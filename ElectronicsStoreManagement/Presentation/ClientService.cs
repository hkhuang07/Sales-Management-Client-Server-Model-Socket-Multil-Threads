// ElectronicsStore.Client/ClientService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.Client
{
    public class ClientService
    {
        private readonly string _serverIp;
        private readonly int _serverPort;

        public ClientService(string serverIp, int serverPort)
        {
            _serverIp = serverIp;
            _serverPort = serverPort;
        }

        // Helper method to read a specific number of bytes from the stream
        private async Task<byte[]> ReadExactlyAsync(NetworkStream stream, int bytesToRead)
        {
            byte[] buffer = new byte[bytesToRead];
            int totalBytesRead = 0;
            int bytesRead;

            while (totalBytesRead < bytesToRead &&
                   (bytesRead = await stream.ReadAsync(buffer, totalBytesRead, bytesToRead - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;
            }

            if (totalBytesRead < bytesToRead)
            {
                // This means the stream ended prematurely
                throw new EndOfStreamException($"Expected {bytesToRead} bytes but only read {totalBytesRead} bytes.");
            }
            return buffer;
        }

        public async Task<TResponseData> SendRequest<TRequestPayload, TResponseData>(string action, TRequestPayload payload)
        {
            // Logging request details for debugging
            Console.WriteLine($"Sending request: MethodName='{action}', Payload='{JsonConvert.SerializeObject(payload)}'");

            using (TcpClient client = new TcpClient())
            {
                try
                {
                    // Set a timeout for connection
                    var connectTask = client.ConnectAsync(_serverIp, _serverPort);
                    if (await Task.WhenAny(connectTask, Task.Delay(5000)) != connectTask) // 5 second timeout
                    {
                        throw new TimeoutException("Connection attempt timed out.");
                    }
                    if (connectTask.IsFaulted) // Check if connection failed
                    {
                        throw connectTask.Exception.InnerException ?? new Exception("Failed to connect to server.");
                    }

                    NetworkStream stream = client.GetStream();

                    ClientRequest<TRequestPayload> request = new ClientRequest<TRequestPayload>(action, payload);
                    string requestJson = JsonConvert.SerializeObject(request);
                    byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

                    // --- Implement Length Prefix Protocol (Client Side) ---
                    // 1. Send message length first (4 bytes)
                    byte[] lengthBytes = BitConverter.GetBytes(requestBytes.Length);
                    await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length);

                    // 2. Send JSON data
                    await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                    await stream.FlushAsync(); // Ensure all data is sent immediately

                    // Logging sent request details
                    Console.WriteLine($"Sent {requestBytes.Length} bytes for '{action}' request.");

                    // --- Read response from server (Client Side) ---
                    // 1. Read the first 4 bytes to get the response length
                    byte[] responseLengthBytes = await ReadExactlyAsync(stream, 4);
                    int responseLength = BitConverter.ToInt32(responseLengthBytes, 0);

                    if (responseLength <= 0) // Validate length
                    {
                        throw new InvalidDataException($"Received invalid response length from server: {responseLength}. This might indicate a server error or corrupted data.");
                    }

                    // 2. Read all response data based on the received length
                    byte[] responseBuffer = await ReadExactlyAsync(stream, responseLength);
                    string responseJson = Encoding.UTF8.GetString(responseBuffer);

                    // Logging received response details
                    Console.WriteLine($"Received {responseLength} bytes for '{action}' response: {responseJson}");

                    if (string.IsNullOrEmpty(responseJson))
                    {
                        throw new Exception("Received empty JSON response from server. This indicates an issue with server response or network.");
                    }

                    // Deserialize response to ServerResponse<TResponseData>
                    ServerResponse<TResponseData> serverResponse;
                    try
                    {
                        serverResponse = JsonConvert.DeserializeObject<ServerResponse<TResponseData>>(responseJson);
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.Error.WriteLine($"JSON Deserialization Error for action '{action}': {jsonEx.Message}");
                        Console.Error.WriteLine($"Raw JSON response that failed: {responseJson}");
                        throw new Exception($"Failed to parse server response JSON for action '{action}'. Mismatch in DTOs or corrupted data. Details: {jsonEx.Message}", jsonEx);
                    }


                    if (serverResponse.Success)
                    {
                        // If successful, return the Data directly
                        return serverResponse.Data;
                    }
                    else // Server indicates an error (response.Success == false)
                    {
                        string errorMessage = serverResponse.Message ?? "An unknown server error occurred.";
                        // Check if Data is not null and can be converted to string, useful for error details
                        if (serverResponse.Data != null)
                        {
                            errorMessage += $"\nServer Details: {JsonConvert.SerializeObject(serverResponse.Data)}";
                        }
                        Console.Error.WriteLine($"Server reported error for action '{action}': {errorMessage}");
                        throw new Exception($"Server error for action '{action}': {errorMessage}");
                    }
                }
                catch (SocketException sockEx)
                {
                    // Provide more user-friendly messages for common socket errors
                    string friendlyMessage;
                    switch (sockEx.SocketErrorCode)
                    {
                        case SocketError.ConnectionRefused:
                            friendlyMessage = "Connection refused. The server might not be running or the IP/port is incorrect.";
                            break;
                        case SocketError.HostNotFound:
                            friendlyMessage = "Server address not found. Check the server IP address.";
                            break;
                        case SocketError.TimedOut:
                            friendlyMessage = "Connection attempt timed out. The server might be busy or unreachable.";
                            break;
                        default:
                            friendlyMessage = $"A network error occurred (Code: {sockEx.SocketErrorCode}).";
                            break;
                    }
                    Console.Error.WriteLine($"Socket Error for action '{action}': {sockEx.Message} (Code: {sockEx.ErrorCode})");
                    throw new Exception($"Connection to server failed. {friendlyMessage} Details: {sockEx.Message}", sockEx);
                }
                catch (TimeoutException timeEx)
                {
                    Console.Error.WriteLine($"Timeout Error for action '{action}': {timeEx.Message}");
                    throw new Exception($"Operation timed out: {timeEx.Message}", timeEx);
                }
                catch (EndOfStreamException eosEx)
                {
                    Console.Error.WriteLine($"Network Stream Error for action '{action}': {eosEx.Message}");
                    throw new Exception($"Incomplete data received from server. This might indicate a network issue or server error. Details: {eosEx.Message}", eosEx);
                }
                catch (InvalidDataException idEx)
                {
                    Console.Error.WriteLine($"Invalid Data Error for action '{action}': {idEx.Message}");
                    throw new Exception($"Received invalid data from server. This indicates a protocol mismatch or corrupted data. Details: {idEx.Message}", idEx);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Client Service unexpected error for action '{action}': {ex.Message}");
                    throw; // Re-throw for the caller to handle
                }
            }
        }

        // --- Specific API methods for Product Management ---
        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            return await SendRequest<object, List<ProductDTO>>("GetAllProducts", null);
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO product)
        {
            return await SendRequest<ProductDTO, ProductDTO>("AddProduct", product);
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO product)
        {
            return await SendRequest<ProductDTO, ProductDTO>("UpdateProduct", product);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await SendRequest<int, bool>("DeleteProduct", productId);
        }

        public async Task<List<ProductDTO>> SearchProductsAsync(string keyword)
        {
            return await SendRequest<string, List<ProductDTO>>("SearchProducts", keyword);
        }

        public async Task<bool> UpdateProductImageAsync(ProductDTO imageUpdateDto)
        {
            return await SendRequest<ProductDTO, bool>("UpdateProductImage", imageUpdateDto);
        }

        public async Task<bool> BulkAddProductsAsync(List<ProductDTO> products)
        {
            return await SendRequest<List<ProductDTO>, bool>("BulkAddProducts", products);
        }

        // --- Specific API methods for Categories and Manufacturers ---
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await SendRequest<object, List<CategoryDTO>>("GetAllCategories", null);
        }

        public async Task<List<ManufacturerDTO>> GetAllManufacturersAsync()
        {
            return await SendRequest<object, List<ManufacturerDTO>>("GetAllManufacturers", null);
        }

        // --- Specific API methods for Employees ---
        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            return await SendRequest<object, List<EmployeeDTO>>("GetAllEmployees", null);
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            return await SendRequest<object, List<CustomerDTO>>("GetAllCustomers", null);
        }

        // --- Specific API methods for Order Management (Updated) ---

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            return await SendRequest<object, List<OrderDTO>>("GetAllOrders", null);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            return await SendRequest<int, OrderDTO>("GetOrderById", orderId);
        }

        public async Task<List<OrderDetailsDTO>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await SendRequest<int, List<OrderDetailsDTO>>("GetOrderDetailsByOrderId", orderId);
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderWithDetailsDTO orderWithDetails)
        {
            return await SendRequest<OrderWithDetailsDTO, OrderDTO>("CreateOrder", orderWithDetails);
        }

        public async Task<bool> UpdateOrderWithDetailsAsync(OrderWithDetailsDTO orderWithDetails)
        {
            return await SendRequest<OrderWithDetailsDTO, bool>("UpdateOrderWithDetails", orderWithDetails);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {   
            return await SendRequest<int, bool>("DeleteOrder", orderId);
        }

        public async Task<List<OrderDTO>> SearchOrdersAsync(string keyword)
        {
            return await SendRequest<string, List<OrderDTO>>("SearchOrders", keyword);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            return await SendRequest<int, ProductDTO>("GetProductById", productId);
        }

        // --- Authentication method ---
        public async Task<LoginResponseDTO> AuthenticateUser(LoginRequestDTO loginRequest)
        {
            // The SendRequest method already returns TResponseData, which is LoginResponseDTO in this case.
            // So, you just need to call it directly.
            return await SendRequest<LoginRequestDTO, LoginResponseDTO>("AuthenticateUser", loginRequest);
        }
    }
}