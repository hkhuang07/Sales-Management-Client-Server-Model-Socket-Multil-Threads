using AutoMapper;
using Azure;
using ElectronicsStore.BusinessLogic;
using ElectronicsStore.DataTransferObject; // Đảm bảo đã có các DTO ở đây
using ElectronicsStore.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    private const int PORT = 301; // Cổng lắng nghe
    private static IMapper _mapper; // Khởi tạo IMapper một lần

    static async Task Main(string[] args)
    {
        // Giả định MapperConfig đã được định nghĩa đúng và có thể Initialize()
        _mapper = MapperConfig.Initialize(); // Khởi tạo mapper một lần duy nhất
        ServerHandler.InitializeMapper(_mapper);

        TcpListener listener = null;
        try
        {
            listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();
            Console.WriteLine($"Server started. Listening on port {PORT}...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

                // Sử dụng _ = để bỏ qua cảnh báo nếu bạn không cần await Task
                // Nên sử dụng ConfigureAwait(false) nếu bạn không cần tiếp tục trên cùng một context
                _ = Task.Run(() => ServerHandler.HandleClientAsync(client));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            listener?.Stop();
        }
    }

    

    // Phương thức trợ giúp để gửi phản hồi
    // Nhận ServerResponseBase để cho phép Newtonsoft.Json serialize Data là object
    private static async Task SendResponse(NetworkStream stream, ServerResponseBase response)
    {
        string responseJson = JsonConvert.SerializeObject(response);
        byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);
        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
        Console.WriteLine($"Sent response: {responseJson}");
    }
}


/*static async Task HandleClientAsync(TcpClient client)
    {
        IPEndPoint remoteEndPoint = null;
        try
        {
            remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;

            // KHỞI TẠO TẤT CẢ CÁC SERVICE TRONG MỖI LUỒNG
            // Các service này cần có constructor nhận IMapper
            var productService = new ProductService(_mapper);
            var categoryService = new CategoryService(_mapper);
            var manufacturerService = new ManufacturerService(_mapper);
            var employeeService = new EmployeeService(_mapper);
            var customerService = new CustomerService(_mapper);
            var orderService = new OrderService(_mapper);
            var orderDetailsService = new OrderDetailsService(_mapper);
            // var userService = new UserService(_mapper); // Uncomment nếu có UserService

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string requestJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received from client: {requestJson}");

                ClientRequestBase requestBase;
                ServerResponseBase responseBase = new ServerResponseBase { Success = false, Message = "Unknown action." }; // Khởi tạo response mặc định

                try
                {
                    // Deserialize thành ClientRequestBase để lấy MethodName và Data thô (object)
                    requestBase = JsonConvert.DeserializeObject<ClientRequestBase>(requestJson);
                }
                catch (JsonException jEx)
                {
                    responseBase.Message = $"Invalid JSON format: {jEx.Message}";
                    await SendResponse(stream, responseBase);
                    continue; // Bỏ qua request lỗi và chờ request tiếp theo
                }
                catch (Exception ex)
                {
                    responseBase.Message = $"Error processing request: {ex.Message}";
                    await SendResponse(stream, responseBase);
                    continue;
                }

                switch (requestBase.MethodName) // Dùng requestBase.MethodName
                {
                    // Các case cho Category
                    case "GetAllCategories":
                        try
                        {
                            var categories = categoryService.GetAll();
                            responseBase.Success = true;
                            responseBase.Message = "Categories retrieved successfully.";
                            responseBase.Data = categories; // Gán trực tiếp list DTO
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error getting categories: {ex.Message}";
                        }
                        break;

                    case "GetCategoriesByName":
                        try
                        {
                            // Cast requestBase.Data sang string rồi deserialize
                            string keyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                            var filteredCategories = categoryService.GetByName(keyword);
                            responseBase.Success = true;
                            responseBase.Message = "Categories filtered successfully.";
                            responseBase.Data = filteredCategories;
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error filtering categories by name: {ex.Message}";
                        }
                        break;

                    case "AddCategory":
                        try
                        {
                            // Deserialize requestBase.Data sang CategoryDTO
                            var categoryToAdd = JsonConvert.DeserializeObject<CategoryDTO>(requestBase.Data.ToString());
                            categoryService.Add(categoryToAdd);
                            responseBase.Success = true;
                            responseBase.Message = "Category added successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error adding category: {ex.Message}";
                        }
                        break;

                    case "UpdateCategory":
                        try
                        {
                            var categoryToUpdate = JsonConvert.DeserializeObject<CategoryDTO>(requestBase.Data.ToString());
                            categoryService.Update(categoryToUpdate.ID, categoryToUpdate);
                            responseBase.Success = true;
                            responseBase.Message = "Category updated successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error updating category: {ex.Message}";
                        }
                        break;

                    case "DeleteCategory":
                        try
                        {
                            int idToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            categoryService.Delete(idToDelete);
                            responseBase.Success = true;
                            responseBase.Message = "Category deleted successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error deleting category: {ex.Message}";
                        }
                        break;

                    // Các case cho Manufacturer
                    case "GetAllManufacturers":
                        try
                        {
                            var list = manufacturerService.GetAll();
                            responseBase.Data = list;
                            responseBase.Success = true;
                            responseBase.Message = "Manufacturers retrieved successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error retrieving manufacturers: {ex.Message}";
                        }
                        break;

                    case "GetManufacturersByName":
                        try
                        {
                            string nameKeyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                            var list = manufacturerService.GetByName(nameKeyword);
                            responseBase.Data = list;
                            responseBase.Success = true;
                            responseBase.Message = "Manufacturers retrieved successfully by name.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error retrieving manufacturers by name: {ex.Message}";
                        }
                        break;

                    case "AddManufacturer":
                        try
                        {
                            var dto = JsonConvert.DeserializeObject<ManufacturerDTO>(requestBase.Data.ToString());
                            manufacturerService.Add(dto);
                            responseBase.Success = true;
                            responseBase.Message = "Manufacturer added successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error adding manufacturer: {ex.Message}";
                        }
                        break;

                    case "UpdateManufacturer":
                        try
                        {
                            var dto = JsonConvert.DeserializeObject<ManufacturerDTO>(requestBase.Data.ToString());
                            manufacturerService.Update(dto.ID, dto);
                            responseBase.Success = true;
                            responseBase.Message = "Manufacturer updated successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error updating manufacturer: {ex.Message}";
                        }
                        break;

                    case "DeleteManufacturer":
                        try
                        {
                            int idToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            manufacturerService.Delete(idToDelete);
                            responseBase.Success = true;
                            responseBase.Message = "Manufacturer deleted successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error deleting manufacturer: {ex.Message}";
                        }
                        break;

                    case "BulkAddManufacturers":
                        try
                        {
                            var manufacturersToImport = JsonConvert.DeserializeObject<List<ManufacturerDTO>>(requestBase.Data.ToString());
                            int successCount = 0;
                            foreach (var dto in manufacturersToImport)
                            {
                                try
                                {
                                    manufacturerService.Add(dto);
                                    successCount++;
                                }
                                catch (Exception exInner)
                                {
                                    Console.WriteLine($"Error adding manufacturer during bulk import: {exInner.Message} - Manufacturer: {dto.ManufacturerName}");
                                }
                            }
                            responseBase.Success = true;
                            responseBase.Message = $"{successCount} manufacturers imported successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error during bulk import of manufacturers: {ex.Message}";
                        }
                        break;

                    // Các case cho Product
                    case "GetAllProducts":
                        try
                        {
                            var list = productService.GetAllList();
                            responseBase.Data = list;
                            responseBase.Success = true;
                            responseBase.Message = "Products retrieved successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error retrieving products: {ex.Message}";
                        }
                        break;

                    case "SearchProducts":
                        try
                        {
                            string keyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                            var list = productService.GetByName(keyword);
                            responseBase.Data = list;
                            responseBase.Success = true;
                            responseBase.Message = "Products searched successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error searching products: {ex.Message}";
                        }
                        break;

                    case "AddProduct":
                        try
                        {
                            var dto = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                            productService.Add(dto);
                            responseBase.Success = true;
                            responseBase.Message = "Product added successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error adding product: {ex.Message}";
                        }
                        break;

                    case "UpdateProduct":
                        try
                        {
                            var dto = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                            productService.Update(dto.ID, dto);
                            responseBase.Success = true;
                            responseBase.Message = "Product updated successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error updating product: {ex.Message}";
                        }
                        break;

                    case "DeleteProduct":
                        try
                        {
                            int idToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            productService.Delete(idToDelete);
                            responseBase.Success = true;
                            responseBase.Message = "Product deleted successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error deleting product: {ex.Message}";
                        }
                        break;

                    case "BulkAddProducts":
                        try
                        {
                            var productsToImport = JsonConvert.DeserializeObject<List<ProductDTO>>(requestBase.Data.ToString());
                            int successCount = 0;
                            foreach (var dto in productsToImport)
                            {
                                try
                                {
                                    productService.Add(dto);
                                    successCount++;
                                }
                                catch (Exception exInner)
                                {
                                    Console.WriteLine($"Error adding product during bulk import: {exInner.Message} - Product: {dto.ProductName}");
                                }
                            }
                            responseBase.Success = true;
                            responseBase.Message = $"{successCount} products imported successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error during bulk import of products: {ex.Message}";
                        }
                        break;

                    case "UpdateProductImage":
                        try
                        {
                            // Giả định ProductDTO có trường Image
                            var updateImageDto = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                            productService.UpdateImage(updateImageDto.ID, updateImageDto.Image);
                            responseBase.Success = true;
                            responseBase.Message = "Product image updated successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error updating product image: {ex.Message}";
                        }
                        break;

                    // Các case cho Employee
                    case "GetAllEmployees":
                        try
                        {
                            var employees = employeeService.GetAll();
                            responseBase.Success = true;
                            responseBase.Message = "Employees retrieved successfully.";
                            responseBase.Data = employees;
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error getting employees: {ex.Message}";
                        }
                        break;

                    case "SearchEmployees":
                        try
                        {
                            string keyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                            var filteredEmployees = employeeService.GetByFullName(keyword);
                            responseBase.Success = true;
                            responseBase.Message = "Employees filtered successfully.";
                            responseBase.Data = filteredEmployees;
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error searching employees: {ex.Message}";
                        }
                        break;

                    case "AddEmployee":
                        try
                        {
                            var employeeToAdd = JsonConvert.DeserializeObject<EmployeeDTO>(requestBase.Data.ToString());
                            employeeService.Add(employeeToAdd);
                            responseBase.Success = true;
                            responseBase.Message = "Employee added successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error adding employee: {ex.Message}";
                        }
                        break;

                    case "UpdateEmployee":
                        try
                        {
                            var employeeToUpdate = JsonConvert.DeserializeObject<EmployeeDTO>(requestBase.Data.ToString());
                            employeeService.Update(employeeToUpdate.ID, employeeToUpdate);
                            responseBase.Success = true;
                            responseBase.Message = "Employee updated successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error updating employee: {ex.Message}";
                        }
                        break;

                    case "DeleteEmployee":
                        try
                        {
                            int idToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            employeeService.Delete(idToDelete);
                            responseBase.Success = true;
                            responseBase.Message = "Employee deleted successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error deleting employee: {ex.Message}";
                        }
                        break;

                    case "ImportEmployees":
                        try
                        {
                            var employeesToImport = JsonConvert.DeserializeObject<List<EmployeeDTO>>(requestBase.Data.ToString());
                            int successCount = 0;
                            foreach (var dto in employeesToImport)
                            {
                                try
                                {
                                    employeeService.Add(dto);
                                    successCount++;
                                }
                                catch (Exception exInner)
                                {
                                    Console.WriteLine($"Error adding employee during import: {exInner.Message} - Employee: {dto.FullName}");
                                }
                            }
                            responseBase.Success = true;
                            responseBase.Message = $"{successCount} employee(s) imported successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error during bulk import of employees: {ex.Message}";
                        }
                        break;

                    // Các case cho Customer
                    case "GetAllCustomer":
                        try
                        {
                            var customers = customerService.GetAll();
                            responseBase.Success = true;
                            responseBase.Message = "Customers retrieved successfully.";
                            responseBase.Data = customers;
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error getting customers: {ex.Message}";
                        }
                        break;

                    case "SearchCustomers":
                        try
                        {
                            string keyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                            var filteredCustomers = customerService.GetByName(keyword);
                            responseBase.Success = true;
                            responseBase.Message = "Customers filtered successfully.";
                            responseBase.Data = filteredCustomers;
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error searching customers: {ex.Message}";
                        }
                        break;

                    case "AddCustomer":
                        try
                        {
                            CustomerDTO customerToAdd = JsonConvert.DeserializeObject<CustomerDTO>(requestBase.Data.ToString());
                            customerService.Add(customerToAdd);
                            responseBase.Success = true;
                            responseBase.Message = "Customer added successfully.";
                        }
                        catch (ArgumentException ex)
                        {
                            responseBase.Message = $"Validation Error: {ex.Message}";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error adding customer: {ex.Message}";
                        }
                        break;

                    case "UpdateCustomer":
                        try
                        {
                            CustomerDTO customerToUpdate = JsonConvert.DeserializeObject<CustomerDTO>(requestBase.Data.ToString());
                            customerService.Update(customerToUpdate.ID, customerToUpdate);
                            responseBase.Success = true;
                            responseBase.Message = "Customer updated successfully.";
                        }
                        catch (ArgumentException ex)
                        {
                            responseBase.Message = $"Validation Error: {ex.Message}";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error updating customer: {ex.Message}";
                        }
                        break;

                    case "DeleteCustomer":
                        try
                        {
                            int customerIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            customerService.Delete(customerIdToDelete);
                            responseBase.Success = true;
                            responseBase.Message = "Customer deleted successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error deleting customer: {ex.Message}";
                        }
                        break;

                    // Các case cho Order
                    case "GetAllOrders":
                        try
                        {
                            var orders = orderService.GetAllList();
                            responseBase.Success = true;
                            responseBase.Message = "Orders retrieved successfully.";
                            responseBase.Data = orders;
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error getting all orders: {ex.Message}";
                        }
                        break;

                    case "GetOrderById":
                        try
                        {
                            int orderId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            var order = orderService.GetById(orderId);
                            if (order != null)
                            {
                                responseBase.Success = true;
                                responseBase.Message = "Order retrieved successfully.";
                                responseBase.Data = order;
                            }
                            else
                            {
                                responseBase.Message = "Order not found.";
                            }
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error getting order by ID: {ex.Message}";
                        }
                        break;

                    case "CreateOrder":
                        try
                        {
                            // OrderWithDetailsDTO cần được định nghĩa để chứa cả OrderDTO và List<OrderDetailDTO>
                            var orderWithDetails = JsonConvert.DeserializeObject<OrderWithDetailsDTO>(requestBase.Data.ToString());

                            // Gọi phương thức CreateOrder đã có trong OrderService
                            int newOrderId = orderService.CreateOrder(orderWithDetails.Order, orderWithDetails.OrderDetails);

                            responseBase.Success = true;
                            responseBase.Message = "Order and details created successfully.";
                            responseBase.Data = newOrderId; // Trả về ID của Order mới tạo
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error creating order: {ex.Message}";
                        }
                        break;

                    case "UpdateOrder":
                        try
                        {
                            var orderWithDetails = JsonConvert.DeserializeObject<OrderWithDetailsDTO>(requestBase.Data.ToString());

                            orderService.UpdateOrder(orderWithDetails.Order, orderWithDetails.OrderDetails);

                            responseBase.Success = true;
                            responseBase.Message = "Order and details updated successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error updating order: {ex.Message}";
                        }
                        break;

                    case "DeleteOrder":
                        try
                        {
                            int orderIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            orderService.DeleteOrderAndDetails(orderIdToDelete);
                            responseBase.Success = true;
                            responseBase.Message = "Order and its details deleted successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error deleting order: {ex.Message}";
                        }
                        break;

                    // ======================================
                    // CÁC CASE CHO ORDER DETAILS
                    // ======================================
                    case "GetOrderDetailsByOrderId":
                        try
                        {
                            int orderId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            var details = orderDetailsService.GetByOrderID(orderId);
                            responseBase.Success = true;
                            responseBase.Message = "Order details retrieved successfully.";
                            responseBase.Data = details;
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error getting order details by order ID: {ex.Message}";
                        }
                        break;

                    case "GetOrderDetailById":
                        try
                        {
                            int detailId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            var detail = orderDetailsService.GetById(detailId);
                            if (detail != null)
                            {
                                responseBase.Success = true;
                                responseBase.Message = "Order detail retrieved successfully.";
                                responseBase.Data = detail;
                            }
                            else
                            {
                                responseBase.Message = "Order detail not found.";
                            }
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error getting order detail by ID: {ex.Message}";
                        }
                        break;

                    case "DeleteOrderDetail":
                        try
                        {
                            int detailIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                            orderDetailsService.Delete(detailIdToDelete); // Giả định OrderDetailsService có phương thức Delete(int id)
                            responseBase.Success = true;
                            responseBase.Message = "Order detail deleted successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error deleting order detail: {ex.Message}";
                        }
                        break;

                    case "ConfirmAccount":
                        try
                        {
                            string confirmationToken = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                            //userService.ConfirmAccount(confirmationToken); // Cần khởi tạo userService nếu sử dụng
                            responseBase.Success = true;
                            responseBase.Message = "Account confirmed successfully.";
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error confirming account: {ex.Message}";
                        }
                        break;

                    case "AuthenticateUser":
                        try
                        {
                            // Deserialize requestBase.Data sang LoginRequestDTO
                            var loginRequest = JsonConvert.DeserializeObject<LoginRequestDTO>(requestBase.Data.ToString());

                            if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
                            {
                                responseBase.Message = "Username and password are required.";
                            }
                            else
                            {
                                // Gọi phương thức Authenticate từ EmployeeService
                                var authenticatedEmployee = employeeService.Authenticate(loginRequest.Username, loginRequest.Password);

                                if (authenticatedEmployee != null)
                                {
                                    // Đăng nhập thành công, tạo LoginResponseDTO để trả về client
                                    var loginResponse = new LoginResponseDTO
                                    {
                                        UserId = authenticatedEmployee.ID,
                                        FullName = authenticatedEmployee.FullName,
                                        Username = authenticatedEmployee.UserName,
                                        Roles = authenticatedEmployee.Role // Map Role (bool) của EmployeeDTO sang IsAdmin của LoginResponseDTO
                                    };
                                    responseBase.Success = true;
                                    responseBase.Message = "Login successful.";
                                    responseBase.Data = loginResponse;
                                }
                                else
                                {
                                    // Đăng nhập thất bại
                                    responseBase.Message = "Invalid username or password.";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            responseBase.Message = $"Error during authentication process: {ex.Message}";
                        }
                        break;

                    default:
                        responseBase.Message = "Unknown method or unsupported action.";
                        responseBase.Success = false;
                        break;
                }

                await SendResponse(stream, responseBase); // Gửi responseBase
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling client {remoteEndPoint}: {ex.Message}");
        }
        finally
        {
            client.Close();
            Console.WriteLine($"Client disconnected: {remoteEndPoint}");
        }
    }*/