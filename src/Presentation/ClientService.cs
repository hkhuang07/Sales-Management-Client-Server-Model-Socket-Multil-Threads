using ElectronicsStore.DataTransferObject;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Client
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;
        private HubConnection _hubConnection;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        public string Token { get; set; }

        public event Action<string> OnRealTimeNotificationReceived;

        public ClientService(string serverUrl)
        {
            _httpClient = new HttpClient();
            // Default ASP.NET Core URL is often http://localhost:5000 or https://localhost:5001
            _httpClient.BaseAddress = new Uri(serverUrl);
            
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => (int)r.StatusCode >= 500)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (result, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"Request failed with status {result.Result?.StatusCode}. Retrying in {timeSpan}... (Attempt {retryCount})");
                    });

            InitializeSignalR(serverUrl);
        }

        public ClientService(string host, int port) : this($"http://{host}:{port}")
        {
        }

        private void InitializeSignalR(string serverUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(serverUrl.TrimEnd('/') + "/storeHub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(Token);
                })
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("ReceiveNotification", (message) =>
            {
                OnRealTimeNotificationReceived?.Invoke(message);
            });
        }

        public async Task ConnectSignalRAsync()
        {
            try
            {
                if (_hubConnection.State == HubConnectionState.Disconnected)
                {
                    await _hubConnection.StartAsync();
                    Console.WriteLine("SignalR Connected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignalR Connection Error: {ex.Message}");
            }
        }

        private void PrepareHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
        }

        private async Task<TResponseData> SendHttpRequestAsync<TRequestPayload, TResponseData>(HttpMethod method, string endpoint, TRequestPayload payload)
        {
            if (string.IsNullOrEmpty(Token) && !endpoint.Contains("api/Auth/login", StringComparison.OrdinalIgnoreCase))
            {
                return default;
            }

            PrepareHeaders();

            try
            {
                HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () =>
                {
                    HttpRequestMessage request = new HttpRequestMessage(method, endpoint);
                    if (payload != null)
                    {
                        string jsonPayload = JsonConvert.SerializeObject(payload);
                        request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    }
                    return await _httpClient.SendAsync(request);
                });

                response.EnsureSuccessStatusCode();
                
                string responseJson = await response.Content.ReadAsStringAsync();
                var srvResp = JsonConvert.DeserializeObject<ServerResponse<TResponseData>>(responseJson);
                if (srvResp != null && srvResp.Success)
                {
                    return srvResp.Data;
                }
                else
                {
                    throw new Exception(srvResp?.Message ?? "Server returned error.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"HTTP Request Error for {endpoint}: {ex.Message}");
                throw;
            }
        }

        // --- Auth ---
        public async Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginRequest)
        {
            PrepareHeaders();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/Auth/login");
            request.Content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(responseJson);
                if (loginResponse != null && loginResponse.Success)
                {
                    Token = loginResponse.Token;
                    PrepareHeaders();
                    // Re-connect SignalR with the new token
                    await ConnectSignalRAsync();
                }
                return loginResponse ?? new LoginResponseDTO { Success = false, Message = "Invalid response format" };
            }

            try
            {
                var errResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(responseJson);
                if (errResponse != null && !string.IsNullOrEmpty(errResponse.Message))
                {
                    return errResponse;
                }
            }
            catch { }

            return new LoginResponseDTO { Success = false, Message = string.IsNullOrWhiteSpace(responseJson) ? "Invalid username or password" : responseJson };
        }

        // --- Products ---
        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            return await SendHttpRequestAsync<object, List<ProductDTO>>(HttpMethod.Get, "api/Products", null);
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO product)
        {
            return await SendHttpRequestAsync<ProductDTO, ProductDTO>(HttpMethod.Post, "api/Products", product);
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO product)
        {
            return await SendHttpRequestAsync<ProductDTO, ProductDTO>(HttpMethod.Put, "api/Products", product);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await SendHttpRequestAsync<object, bool>(HttpMethod.Delete, $"api/Products/{productId}", null);
        }

        public async Task<List<ProductDTO>> SearchProductsAsync(string keyword)
        {
            return await SendHttpRequestAsync<object, List<ProductDTO>>(HttpMethod.Get, $"api/Products/search?keyword={Uri.EscapeDataString(keyword)}", null);
        }

        public async Task<byte[]?> GetProductImageAsync(string fileName)
        {
            return null; // For simplicity in this demo, image operations return null
        }

        public async Task<bool> UploadProductImageAsync(int productId, string fileName, byte[] imageData)
        {
            return false;
        }

        // --- Categories ---
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await SendHttpRequestAsync<object, List<CategoryDTO>>(HttpMethod.Get, "api/Categories", null);
        }

        // --- Manufacturers ---
        public async Task<List<ManufacturerDTO>> GetAllManufacturersAsync()
        {
            return await SendHttpRequestAsync<object, List<ManufacturerDTO>>(HttpMethod.Get, "api/Manufacturers", null);
        }

        // --- Employees ---
        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            return await SendHttpRequestAsync<object, List<EmployeeDTO>>(HttpMethod.Get, "api/Employees", null);
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employee)
        {
            return await SendHttpRequestAsync<EmployeeDTO, EmployeeDTO>(HttpMethod.Post, "api/Employees", employee);
        }

        public async Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO employee)
        {
            return await SendHttpRequestAsync<EmployeeDTO, EmployeeDTO>(HttpMethod.Put, "api/Employees", employee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await SendHttpRequestAsync<object, bool>(HttpMethod.Delete, $"api/Employees/{id}", null);
        }

        // --- Customers ---
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            return await SendHttpRequestAsync<object, List<CustomerDTO>>(HttpMethod.Get, "api/Customers", null);
        }
        
        public async Task<CustomerDTO> AddCustomerAsync(CustomerDTO customer)
        {
            return await SendHttpRequestAsync<CustomerDTO, CustomerDTO>(HttpMethod.Post, "api/Customers", customer);
        }
        
        public async Task<bool> UpdateCustomerAsync(CustomerDTO customer)
        {
            await SendHttpRequestAsync<CustomerDTO, CustomerDTO>(HttpMethod.Put, "api/Customers", customer);
            return true;
        }

        // --- Orders ---
        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            return await SendHttpRequestAsync<object, List<OrderDTO>>(HttpMethod.Get, "api/Orders", null);
        }

        public async Task<List<OrderDetailsDTO>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await SendHttpRequestAsync<object, List<OrderDetailsDTO>>(HttpMethod.Get, $"api/Orders/{orderId}/details", null);
        }

        public async Task<int> CreateOrderAsync(OrderWithDetailsDTO orderWithDetails)
        {
            return await SendHttpRequestAsync<OrderWithDetailsDTO, int>(HttpMethod.Post, "api/Orders", orderWithDetails);
        }

        // Extra methods required by Presentation
        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            return await SendHttpRequestAsync<object, ProductDTO>(HttpMethod.Get, $"api/Products/{productId}", null);
        }

        public async Task<bool> BulkAddProductsAsync(List<ProductDTO> products)
        {
            await SendHttpRequestAsync<List<ProductDTO>, object>(HttpMethod.Post, "api/Products/bulk", products);
            return true;
        }

        public async Task<EmployeeDTO> GetEmployeeByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var results = await SendHttpRequestAsync<object, List<EmployeeDTO>>(HttpMethod.Get, $"api/Employees/search?keyword={Uri.EscapeDataString(name)}", null);
            return results?.Count > 0 ? results[0] : null;
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            return await SendHttpRequestAsync<object, EmployeeDTO>(HttpMethod.Get, $"api/Employees/{id}", null);
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            return await SendHttpRequestAsync<object, CustomerDTO>(HttpMethod.Get, $"api/Customers/{id}", null);
        }

        public async Task<bool> UpdateOrderWithDetailsAsync(OrderWithDetailsDTO orderWithDetails)
        {
            await SendHttpRequestAsync<OrderWithDetailsDTO, object>(HttpMethod.Put, "api/Orders", orderWithDetails);
            return true;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            return await SendHttpRequestAsync<object, OrderDTO>(HttpMethod.Get, $"api/Orders/{orderId}", null);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await SendHttpRequestAsync<object, bool>(HttpMethod.Delete, $"api/Orders/{orderId}", null);
        }

        public async Task<List<OrderDTO>> SearchOrdersAsync(string keyword)
        {
            return await SendHttpRequestAsync<object, List<OrderDTO>>(HttpMethod.Get, $"api/Orders/search?keyword={Uri.EscapeDataString(keyword)}", null);
        }

        public async Task<List<OrderDTO>> GetOrdersByStatus(string status = null)
        {
            var allOrders = await SendHttpRequestAsync<object, List<OrderDTO>>(HttpMethod.Get, "api/Orders", null);
            if (string.IsNullOrEmpty(status)) return allOrders;
            // DTO might not have Status string directly or it could be an enum. Just return all for now if no filter applied.
            return allOrders; 
        }

        public async Task<int> CreateTmpOrderAsync(OrderWithDetailsDTO orderWithDetails)
        {
            return await SendHttpRequestAsync<OrderWithDetailsDTO, int>(HttpMethod.Post, "api/Orders", orderWithDetails);
        }

        public async Task<bool> ConfirmOrderAsync(ConfirmOrderDTO dto)
        {
            var order = await GetOrderByIdAsync(dto.OrderID);
            if (order != null)
            {
                order.CustomerID = dto.CustomerID;
                order.EmployeeID = dto.EmployeeID;
                order.Note = dto.Note;
                order.Status = "Confirmed";
                await SendHttpRequestAsync<OrderDTO, object>(HttpMethod.Put, "api/Orders", order);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await SendHttpRequestAsync<OrderDTO, object>(HttpMethod.Put, "api/Orders", order);
                return true;
            }
            return false;
        }

        public async Task<TResponseData> SendRequest<TRequestPayload, TResponseData>(string action, TRequestPayload payload)
        {
            HttpMethod method = HttpMethod.Get;
            string endpoint = "";
            switch (action)
            {
                case "GetAllCategories": method = HttpMethod.Get; endpoint = "api/Categories"; break;
                case "GetCategoriesByName": method = HttpMethod.Get; endpoint = $"api/Categories/search?keyword={Uri.EscapeDataString(payload?.ToString() ?? "")}"; break;
                case "AddCategory": method = HttpMethod.Post; endpoint = "api/Categories"; break;
                case "UpdateCategory": method = HttpMethod.Put; endpoint = "api/Categories"; break;
                case "DeleteCategory": method = HttpMethod.Delete; endpoint = $"api/Categories/{payload}"; break;

                case "GetAllManufacturers": method = HttpMethod.Get; endpoint = "api/Manufacturers"; break;
                case "GetManufacturersByName": method = HttpMethod.Get; endpoint = $"api/Manufacturers/search?keyword={Uri.EscapeDataString(payload?.ToString() ?? "")}"; break;
                case "AddManufacturer": method = HttpMethod.Post; endpoint = "api/Manufacturers"; break;
                case "UpdateManufacturer": method = HttpMethod.Put; endpoint = "api/Manufacturers"; break;
                case "DeleteManufacturer": method = HttpMethod.Delete; endpoint = $"api/Manufacturers/{payload}"; break;

                case "GetAllEmployees": method = HttpMethod.Get; endpoint = "api/Employees"; break;
                case "SearchEmployees": method = HttpMethod.Get; endpoint = $"api/Employees/search?keyword={Uri.EscapeDataString(payload?.ToString() ?? "")}"; break;
                case "AddEmployee": method = HttpMethod.Post; endpoint = "api/Employees"; break;
                case "UpdateEmployee": method = HttpMethod.Put; endpoint = "api/Employees"; break;
                case "DeleteEmployee": method = HttpMethod.Delete; endpoint = $"api/Employees/{payload}"; break;
                case "ChangeEmployeePassword": method = HttpMethod.Post; endpoint = "api/Auth/change-password"; break;

                case "GetAllCustomers": method = HttpMethod.Get; endpoint = "api/Customers"; break;
                case "SearchCustomers": method = HttpMethod.Get; endpoint = $"api/Customers/search?keyword={Uri.EscapeDataString(payload?.ToString() ?? "")}"; break;
                case "AddCustomer": method = HttpMethod.Post; endpoint = "api/Customers"; break;
                case "UpdateCustomer": method = HttpMethod.Put; endpoint = "api/Customers"; break;
                case "DeleteCustomer": method = HttpMethod.Delete; endpoint = $"api/Customers/{payload}"; break;

                case "BackupDatabase": method = HttpMethod.Post; endpoint = "api/Database/backup"; break;
                case "RestoreDatabase": method = HttpMethod.Post; endpoint = "api/Database/restore"; break;

                default: throw new NotSupportedException($"Action {action} is not mapped.");
            }
            object requestPayload = (method == HttpMethod.Post || method == HttpMethod.Put) ? payload : null;
            return await SendHttpRequestAsync<object, TResponseData>(method, endpoint, requestPayload);
        }
    }
}