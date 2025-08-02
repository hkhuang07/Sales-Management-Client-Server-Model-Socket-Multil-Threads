using ElectronicsStore.DataTransferObject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
        private async Task<byte[]> ReadExactlyAsync(NetworkStream stream, int bytesToRead, CancellationToken cancellationToken = default)
        {
            byte[] buffer = new byte[bytesToRead];
            int totalBytesRead = 0;

            // Gán một giá trị mặc định cho bytesRead để tránh lỗi "use of unassigned local variable"
            int bytesRead = 0;

            while (totalBytesRead < bytesToRead)
            {
                // Kiểm tra cancellation token trước khi thực hiện thao tác I/O
                cancellationToken.ThrowIfCancellationRequested();

                // Sử dụng ReadAsync với CancellationToken
                bytesRead = await stream.ReadAsync(buffer, totalBytesRead, bytesToRead - totalBytesRead, cancellationToken);

                if (bytesRead == 0)
                {
                    // Kết nối bị đóng đột ngột.
                    throw new EndOfStreamException($"Connection closed prematurely. Expected {bytesToRead} bytes, but only received {totalBytesRead}.");
                }

                totalBytesRead += bytesRead;
            }

            return buffer;
        }
        // Phương thức chung để gửi yêu cầu và nhận phản hồi
        public async Task<TResponseData> SendRequest<TRequestPayload, TResponseData>(string action, TRequestPayload payload)
        {
            Console.WriteLine($"Sending request: MethodName='{action}', Payload='{(payload != null ? JsonConvert.SerializeObject(payload) : "null")}'");

            using (TcpClient client = new TcpClient())
            {
               
                try
                {
                    var connectTask = client.ConnectAsync(_serverIp, _serverPort);
                    if (await Task.WhenAny(connectTask, Task.Delay(5000)) != connectTask)
                    {
                        throw new TimeoutException("Connection attempt timed out.");
                    }
                    if (connectTask.IsFaulted)
                    {
                        throw connectTask.Exception.InnerException ?? new Exception("Failed to connect to server.");
                    }

                    NetworkStream stream = client.GetStream();

                    // Sử dụng ClientRequest<TRequestPayload> cho tất cả các yêu cầu
                    ClientRequest<TRequestPayload> request = new ClientRequest<TRequestPayload>(action, payload);
                    string requestJson = JsonConvert.SerializeObject(request);
                    byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

                    byte[] lengthBytes = BitConverter.GetBytes(requestBytes.Length);
                    await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length);
                    await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                    await stream.FlushAsync();

                    Console.WriteLine($"Sent {requestBytes.Length} bytes for '{action}' request.");

                    //byte[] responseLengthBytes = await ReadExactlyAsync(stream, 4, cts.Token);
                    byte[] responseLengthBytes = await ReadExactlyAsync(stream, 4);
                    int responseLength = BitConverter.ToInt32(responseLengthBytes, 0);

                    if (responseLength <= 0)
                    {
                        throw new InvalidDataException($"Received invalid response length from server: {responseLength}. This might indicate a server error or corrupted data.");
                    }

                    byte[] responseBuffer = await ReadExactlyAsync(stream, responseLength);
                    string responseJson = Encoding.UTF8.GetString(responseBuffer);

                    Console.WriteLine($"Received {responseLength} bytes for '{action}' response: {responseJson}");

                    if (string.IsNullOrEmpty(responseJson))
                    {
                        throw new Exception("Received empty JSON response from server. This indicates an issue with server response or network.");
                    }

                    // Sử dụng ServerResponse<TResponseData> thay cho ServerResponse<object> để deserialize trực tiếp
                    ServerResponse<TResponseData> serverResponse;
                    try
                    {
                        serverResponse = JsonConvert.DeserializeObject<ServerResponse<TResponseData>>(responseJson);
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.Error.WriteLine($"JSON Deserialization Error (raw) for action '{action}': {jsonEx.Message}");
                        Console.Error.WriteLine($"Raw JSON response that failed: {responseJson}");
                        throw new Exception($"Failed to parse raw server response JSON for action '{action}'. Details: {jsonEx.Message}", jsonEx);
                    }

                    if (serverResponse.Success)
                    {
                        if (serverResponse.Data == null && !EqualityComparer<TResponseData>.Default.Equals(default(TResponseData), serverResponse.Data))
                        {
                            // trường hợp data của TResponseData không thể là null.
                            // ví dụ TResponseData là List, int,.. nhưng data trả về là null
                            throw new InvalidDataException("Server response indicates success but data is null.");
                        }

                        // Trả về trực tiếp data đã deserialize
                        return serverResponse.Data;
                    }
                    else // Server indicates an error
                    {
                        string errorMessage = serverResponse.Message ?? "An unknown server error occurred.";
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
                    string friendlyMessage = sockEx.SocketErrorCode switch
                    {
                        SocketError.ConnectionRefused => "Connection refused. The server might not be running or the IP/port is incorrect.",
                        SocketError.HostNotFound => "Server address not found. Check the server IP address.",
                        SocketError.TimedOut => "Connection attempt timed out. The server might be busy or unreachable.",
                        _ => $"A network error occurred (Code: {sockEx.SocketErrorCode}).",
                    };
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
                    throw;
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
            // This sends the keyword as a string payload, which is correct.
            return await SendRequest<string, List<ProductDTO>>("SearchProducts", keyword);
        }
        public async Task<bool> BulkAddProductsAsync(List<ProductDTO> products)
        {
            return await SendRequest<List<ProductDTO>, bool>("BulkAddProducts", products);
        }
        // Phương thức này đã được sửa lỗi và đồng bộ với SendRequest
        public async Task<byte[]?> GetProductImageAsync(string fileName)
        {
            try
            {
                // Gọi phương thức SendRequest đã được định nghĩa
                return await SendRequest<string, byte[]>("GetProductImage", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting product image: {ex.Message}");
                return null;
            }
        }
        // --- New method to upload product image ---
        public async Task<bool> UploadProductImageAsync(int productId, string fileName, byte[] imageData)
        {
            var payload = new ImageUploadRequestDTO
            {
                ProductId = productId,
                FileName = fileName,
                ImageData = imageData
            };
            return await SendRequest<ImageUploadRequestDTO, bool>("UploadProductImage", payload);
        }


        // --- Specific API metods for Categories ---
        public async Task<List<CategoryDTO>> GetCategoriesByNameAsync(string categoryName)
        {
            return await SendRequest<string, List<CategoryDTO>>("GetCategoriesByName", categoryName);
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await SendRequest<object, List<CategoryDTO>>("GetAllCategories", null);
        }


        // --- Specific API metods for Manufacturers ---
        public async Task<List<ManufacturerDTO>> GetAllManufacturersAsync()
        {
            return await SendRequest<object, List<ManufacturerDTO>>("GetAllManufacturers", null);
        }

        // --- Specific API methods for Employees ---
        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            return await SendRequest<object, List<EmployeeDTO>>("GetAllEmployees", null);
        }
        public async Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginRequest)
        {
            return await SendRequest<LoginRequestDTO, LoginResponseDTO>("Authenticate", loginRequest);
        }



        // --- Specific API metods for Customers ---
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            return await SendRequest<object, List<CustomerDTO>>("GetAllCustomers", null);
        }
        public async Task<CustomerDTO> GetCustomerByIdAsync(int customerId)
        {
            return await SendRequest<int, CustomerDTO>("GetCustomerById", customerId);
        }

        public async Task<CustomerDTO> AddCustomerAsync(CustomerDTO customerDto)
        {
            return await SendRequest<CustomerDTO, CustomerDTO>("AddCustomer", customerDto);
        }

        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDto)
        {
            // Server trả về true/false
            return await SendRequest<CustomerDTO, bool>("UpdateCustomer", customerDto);
        }

        public async Task<bool> ConfirmOrderAsync(ConfirmOrderDTO confirmOrderDto)
        {
            // Server trả về true/false
            return await SendRequest<ConfirmOrderDTO, bool>("ConfirmOrder", confirmOrderDto);
        }

        // --- Specific API methods for Order Management ---
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

        public async Task<int> CreateOrderAsync(OrderWithDetailsDTO orderWithDetails)
        {
            return await SendRequest<OrderWithDetailsDTO, int>("CreateOrder", orderWithDetails);
        }
        public async Task<int> CreateTmpOrderAsync(OrderWithDetailsDTO orderWithDetails)
        {
            return await SendRequest<OrderWithDetailsDTO, int>("CreateTmpOrder", orderWithDetails);
        }


        public async Task<bool> UpdateOrderWithDetailsAsync(OrderWithDetailsDTO orderWithDetails)
        {
            return await SendRequest<OrderWithDetailsDTO, bool>("UpdateOrder", orderWithDetails);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await SendRequest<int, bool>("DeleteOrder", orderId);
        }

        public async Task<List<OrderDTO>> SearchOrdersAsync(string id)
        {
            return await SendRequest<string, List<OrderDTO>>("SearchOrder", id);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            return await SendRequest<int, ProductDTO>("GetProductById", productId);
        }

 
    }
}