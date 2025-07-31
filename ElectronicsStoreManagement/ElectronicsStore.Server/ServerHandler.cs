using AutoMapper;
using ElectronicsStore.BusinessLogic;
using ElectronicsStore.DataTransferObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Server
{
    public class ServerHandler
    {
        private static IMapper _mapper; // Cần được khởi tạo, ví dụ trong hàm Main hoặc Startup

        // Phương thức khởi tạo cho _mapper
        public static void InitializeMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static async Task HandleClientAsync(TcpClient client)
        {
            IPEndPoint remoteEndPoint = null;
            try
            {
                remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine($"Client connected: {remoteEndPoint}");

                if (_mapper == null)
                {
                    throw new InvalidOperationException("IMapper has not been initialized. Call ServerHandler.InitializeMapper() first.");
                }

                // Khởi tạo các service
                var productService = new ProductService(_mapper);
                var categoryService = new CategoryService(_mapper);
                var manufacturerService = new ManufacturerService(_mapper);
                var employeeService = new EmployeeService(_mapper);
                var customerService = new CustomerService(_mapper);
                var orderService = new OrderService(_mapper);
                var orderDetailsService = new OrderDetailsService(_mapper);

                NetworkStream stream = client.GetStream();

                // Buffer để đọc độ dài (4 bytes)
                byte[] lengthBuffer = new byte[4];

                while (true) // Vòng lặp liên tục để nhận và xử lý request
                {
                    ClientRequestBase requestBase = null;
                    ServerResponseBase responseBase = new ServerResponseBase { Success = false, Message = "Unknown action or server error." }; // Khởi tạo response mặc định
                    string requestJson = null;
                    try
                    {
                        // 1. Đọc 4 bytes độ dài của request JSON
                        int bytesReadLength = await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length);
                        if (bytesReadLength == 0)
                        {
                            // Client disconnected gracefully
                            Console.WriteLine($"Client {remoteEndPoint} disconnected.");
                            break; // Thoát vòng lặp xử lý client này
                        }
                        if (bytesReadLength < 4)
                        {
                            // Lỗi: Không nhận đủ 4 byte độ dài
                            Console.WriteLine($"Error: Did not receive full length prefix (expected 4 bytes, got {bytesReadLength}) from client {remoteEndPoint}. Disconnecting.");
                            responseBase.Message = "Protocol error: Incomplete length prefix.";
                            await SendResponse(stream, responseBase); // Gửi lỗi về client
                            break;
                        }

                        int requestLength = BitConverter.ToInt32(lengthBuffer, 0);

                        if (requestLength <= 0 || requestLength > 10 * 1024 * 1024) // Giới hạn kích thước tin nhắn (ví dụ 10MB)
                        {
                            Console.WriteLine($"Error: Invalid request length ({requestLength}) from client {remoteEndPoint}. Disconnecting.");
                            responseBase.Message = "Protocol error: Invalid request length.";
                            await SendResponse(stream, responseBase);
                            break;
                        }

                        // 2. Đọc dữ liệu JSON dựa trên độ dài đã nhận
                        byte[] requestDataBuffer = new byte[requestLength];
                        int totalBytesReadData = 0;
                        int bytesToReadData = requestLength;

                        while (bytesToReadData > 0)
                        {
                            int currentRead = await stream.ReadAsync(requestDataBuffer, totalBytesReadData, bytesToReadData);
                            if (currentRead == 0)
                            {
                                // Client disconnected while sending data
                                Console.WriteLine($"Client {remoteEndPoint} disconnected unexpectedly while sending data.");
                                break; // Thoát vòng lặp xử lý client này
                            }
                            totalBytesReadData += currentRead;
                            bytesToReadData -= currentRead;
                        }

                        if (totalBytesReadData < requestLength)
                        {
                            // Lỗi: Không nhận đủ dữ liệu JSON
                            Console.WriteLine($"Error: Did not receive full data for request (expected {requestLength}, got {totalBytesReadData}) from client {remoteEndPoint}. Disconnecting.");
                            responseBase.Message = "Protocol error: Incomplete request data.";
                            await SendResponse(stream, responseBase);
                            break;
                        }

                        requestJson = Encoding.UTF8.GetString(requestDataBuffer, 0, totalBytesReadData);
                        Console.WriteLine($"Received from client {remoteEndPoint}: {requestJson}");

                        // Deserialize request
                        requestBase = JsonConvert.DeserializeObject<ClientRequestBase>(requestJson);

                        if (requestBase == null || string.IsNullOrWhiteSpace(requestBase.MethodName))
                        {
                            responseBase.Message = "Invalid request: MethodName is missing.";
                        }
                        else
                        {
                            // Xử lý logic theo MethodName
                            switch (requestBase.MethodName)
                            {
                                // ======================================
                                // CÁC CASE CHO CATEGORY
                                // ======================================
                                case "GetAllCategories":
                                var categories = categoryService.GetAll();
                                responseBase.Success = true;
                                responseBase.Message = "Categories retrieved successfully.";
                                responseBase.Data = categories; // Gán trực tiếp list DTO
                                break;

                                case "GetCategoryById": // Thêm case lấy theo ID
                                    int categoryId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var category = categoryService.GetById(categoryId);
                                    if (category != null)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Category retrieved successfully.";
                                        responseBase.Data = category;
                                    }
                                    else
                                    {
                                        responseBase.Message = "Category not found.";
                                    }
                                    break;

                                case "GetCategoriesByName":
                                    string categoryKeyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                                    var filteredCategories = categoryService.GetByName(categoryKeyword);
                                    responseBase.Success = true;
                                    responseBase.Message = "Categories filtered successfully.";
                                    responseBase.Data = filteredCategories;
                                    break;

                                case "AddCategory":
                                    var categoryToAdd = JsonConvert.DeserializeObject<CategoryDTO>(requestBase.Data.ToString());
                                    categoryService.Add(categoryToAdd);
                                    responseBase.Success = true;
                                    responseBase.Message = "Category added successfully.";
                                    break;

                                case "UpdateCategory":
                                    var categoryToUpdate = JsonConvert.DeserializeObject<CategoryDTO>(requestBase.Data.ToString());
                                    categoryService.Update(categoryToUpdate.ID, categoryToUpdate);
                                    responseBase.Success = true;
                                    responseBase.Message = "Category updated successfully.";
                                    break;

                                case "DeleteCategory":
                                    int categoryIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    categoryService.Delete(categoryIdToDelete);
                                    responseBase.Success = true;
                                    responseBase.Message = "Category deleted successfully.";
                                    break;

                                // ======================================
                                // CÁC CASE CHO MANUFACTURER
                                // ======================================
                                case "GetAllManufacturers":
                                    var manufacturers = manufacturerService.GetAll();
                                    responseBase.Data = manufacturers;
                                    responseBase.Success = true;
                                    responseBase.Message = "Manufacturers retrieved successfully.";
                                    break;

                                case "GetManufacturerById": // Thêm case lấy theo ID
                                    int manufacturerId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var manufacturer = manufacturerService.GetById(manufacturerId);
                                    if (manufacturer != null)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Manufacturer retrieved successfully.";
                                        responseBase.Data = manufacturer;
                                    }
                                    else
                                    {
                                        responseBase.Message = "Manufacturer not found.";
                                    }
                                    break;

                                case "GetManufacturersByName":
                                    string manufacturerNameKeyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                                    var filteredManufacturers = manufacturerService.GetByName(manufacturerNameKeyword);
                                    responseBase.Data = filteredManufacturers;
                                    responseBase.Success = true;
                                    responseBase.Message = "Manufacturers retrieved successfully by name.";
                                    break;

                                case "AddManufacturer":
                                    var manufacturerToAdd = JsonConvert.DeserializeObject<ManufacturerDTO>(requestBase.Data.ToString());
                                    manufacturerService.Add(manufacturerToAdd);
                                    responseBase.Success = true;
                                    responseBase.Message = "Manufacturer added successfully.";
                                    break;

                                case "UpdateManufacturer":
                                    var manufacturerToUpdate = JsonConvert.DeserializeObject<ManufacturerDTO>(requestBase.Data.ToString());
                                    manufacturerService.Update(manufacturerToUpdate.ID, manufacturerToUpdate);
                                    responseBase.Success = true;
                                    responseBase.Message = "Manufacturer updated successfully.";
                                    break;

                                case "DeleteManufacturer":
                                    int manufacturerIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    manufacturerService.Delete(manufacturerIdToDelete);
                                    responseBase.Success = true;
                                    responseBase.Message = "Manufacturer deleted successfully.";
                                    break;

                                case "BulkAddManufacturers":
                                    var manufacturersToImport = JsonConvert.DeserializeObject<List<ManufacturerDTO>>(requestBase.Data.ToString());
                                    int mfgSuccessCount = 0;
                                    foreach (var dto in manufacturersToImport)
                                    {
                                        try
                                        {
                                            manufacturerService.Add(dto);
                                            mfgSuccessCount++;
                                        }
                                        catch (Exception exInner)
                                        {
                                            Console.WriteLine($"Error adding manufacturer during bulk import: {exInner.Message} - Manufacturer: {dto.ManufacturerName}");
                                        }
                                    }
                                    responseBase.Success = true;
                                    responseBase.Message = $"{mfgSuccessCount} manufacturers imported successfully.";
                                    break;

                                // ======================================
                                // CÁC CASE CHO PRODUCT
                                // ======================================
                                case "GetAllProducts":
                                    var products = productService.GetAllList();
                                    responseBase.Data = products;
                                    responseBase.Success = true;
                                    responseBase.Message = "Products retrieved successfully.";
                                    break;

                                case "GetProductById": // Thêm case lấy theo ID
                                    int productId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var product = productService.GetById(productId);
                                    if (product != null)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Product retrieved successfully.";
                                        responseBase.Data = product;
                                    }
                                    else
                                    {
                                        responseBase.Message = "Product not found.";
                                    }
                                    break;

                                case "SearchProducts":
                                    string productKeyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                                    var filteredProducts = productService.GetByName(productKeyword);
                                    responseBase.Data = filteredProducts;
                                    responseBase.Success = true;
                                    responseBase.Message = "Products searched successfully.";
                                    break;

                                case "AddProduct":
                                    var productToAdd = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                                    productService.Add(productToAdd);
                                    responseBase.Success = true;
                                    responseBase.Message = "Product added successfully.";
                                    break;

                                case "UpdateProduct":
                                    var productToUpdate = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                                    productService.Update(productToUpdate.ID, productToUpdate);
                                    responseBase.Success = true;
                                    responseBase.Message = "Product updated successfully.";
                                    break;

                                case "DeleteProduct":
                                    int productIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    productService.Delete(productIdToDelete);
                                    responseBase.Success = true;
                                    responseBase.Message = "Product deleted successfully.";
                                    break;

                                case "BulkAddProducts":
                                    var productsToImport = JsonConvert.DeserializeObject<List<ProductDTO>>(requestBase.Data.ToString());
                                    int productSuccessCount = 0;
                                    foreach (var dto in productsToImport)
                                    {
                                        try
                                        {
                                            productService.Add(dto);
                                            productSuccessCount++;
                                        }
                                        catch (Exception exInner)
                                        {
                                            Console.WriteLine($"Error adding product during bulk import: {exInner.Message} - Product: {dto.ProductName}");
                                        }
                                    }
                                    responseBase.Success = true;
                                    responseBase.Message = $"{productSuccessCount} products imported successfully.";
                                    break;

                                /*case "UpdateProductImage":
                                    // Giả định ProductDTO có trường Image
                                    var updateImageDto = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                                    productService.UpdateImage(updateImageDto.ID, updateImageDto.Image);
                                    responseBase.Success = true;
                                    responseBase.Message = "Product image updated successfully.";
                                    break;*/
                                


                                // ======================================
                                // CÁC CASE CHO EMPLOYEE                |
                                // ======================================
                                case "GetAllEmployees":
                                    var employees = employeeService.GetAll();
                                    responseBase.Success = true;
                                    responseBase.Message = "Employees retrieved successfully.";
                                    responseBase.Data = employees;
                                    break;

                                case "GetEmployeeById": // Thêm case lấy theo ID    
                                    int employeeId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var employee = employeeService.GetById(employeeId);
                                    if (employee != null)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Employee retrieved successfully.";
                                        responseBase.Data = employee;
                                    }
                                    else
                                    {
                                        responseBase.Message = "Employee not found.";
                                    }
                                    break;

                                case "SearchEmployees":
                                    string employeeKeyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                                    var filteredEmployees = employeeService.GetByFullName(employeeKeyword);
                                    responseBase.Success = true;
                                    responseBase.Message = "Employees filtered successfully.";
                                    responseBase.Data = filteredEmployees;
                                    break;

                                case "AddEmployee":
                                    var employeeToAdd = JsonConvert.DeserializeObject<EmployeeDTO>(requestBase.Data.ToString());
                                    employeeService.Add(employeeToAdd);
                                    responseBase.Success = true;
                                    responseBase.Message = "Employee added successfully.";
                                    break;

                                case "UpdateEmployee":
                                    var employeeToUpdate = JsonConvert.DeserializeObject<EmployeeDTO>(requestBase.Data.ToString());
                                    employeeService.Update(employeeToUpdate.ID, employeeToUpdate);
                                    responseBase.Success = true;
                                    responseBase.Message = "Employee updated successfully.";
                                    break;

                                case "DeleteEmployee":
                                    int employeeIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    employeeService.Delete(employeeIdToDelete);
                                    responseBase.Success = true;
                                    responseBase.Message = "Employee deleted successfully.";
                                    break;

                                case "ImportEmployees":
                                    var employeesToImport = JsonConvert.DeserializeObject<List<EmployeeDTO>>(requestBase.Data.ToString());
                                    int employeeSuccessCount = 0;
                                    foreach (var dto in employeesToImport)
                                    {
                                        try
                                        {
                                            employeeService.Add(dto);
                                            employeeSuccessCount++;
                                        }
                                        catch (Exception exInner)
                                        {
                                            Console.WriteLine($"Error adding employee during import: {exInner.Message} - Employee: {dto.FullName}");
                                        }
                                    }
                                    responseBase.Success = true;
                                    responseBase.Message = $"{employeeSuccessCount} employee(s) imported successfully.";
                                    break;

                                case "AuthenticateEmployee": // Đối với nhân viên đăng nhập
                                    try
                                    {
                                        var employeeLoginRequest = JsonConvert.DeserializeObject<LoginRequestDTO>(requestBase.Data.ToString());
                                        if (employeeLoginRequest == null || string.IsNullOrWhiteSpace(employeeLoginRequest.Username) || string.IsNullOrWhiteSpace(employeeLoginRequest.Password))
                                        {
                                            responseBase.Message = "Username and password are required.";
                                        }
                                        else
                                        {
                                            var authenticatedEmployee = employeeService.Authentication(employeeLoginRequest.Username, employeeLoginRequest.Password);
                                        
                                            if (authenticatedEmployee != null)
                                            {
                                                responseBase.Success = true;
                                                responseBase.Message = "Employee authenticated successfully.";
                                                responseBase.Data = authenticatedEmployee; // Trả về LoginResponseDTO
                                            }
                                            else
                                            {
                                                // Trường hợp Authentication trả về null nhưng không ném exception
                                                responseBase.Message = "Invalid username or password.";
                                            }
                                        }
                                    }
                                    catch (ArgumentException argEx)
                                    {
                                        // Bắt lỗi ArgumentException từ EmployeeService.Authentication
                                        responseBase.Message = argEx.Message;
                                        Console.Error.WriteLine($"Authentication Argument Error: {argEx.Message}");
                                    }
                                    catch (UnauthorizedAccessException unauthEx)
                                    {
                                        // Bắt lỗi UnauthorizedAccessException từ EmployeeService.Authentication
                                        responseBase.Message = unauthEx.Message;
                                        Console.Error.WriteLine($"Authentication Unauthorized Error: {unauthEx.Message}");
                                    }
                                    catch (Exception authEx)
                                    {
                                        // Bắt các lỗi khác có thể xảy ra trong quá trình Authentication
                                        responseBase.Message = $"An unexpected error occurred during authentication: {authEx.Message}";
                                        Console.Error.WriteLine($"Authentication unexpected Error: {authEx}");
                                    }
                                    break;

                                /*case "RegisterEmployee":
                                    var registerEmployeeRequest = JsonConvert.DeserializeObject<RegisterRequestDTO>(requestBase.Data.ToString());
                                    employeeService.RegisterEmployee(registerEmployeeRequest);
                                    responseBase.Success = true;
                                    responseBase.Message = "Employee registered successfully.";
                                    break;
                                */
                                /*case "ChangeEmployeePassword":
                                    var changeEmployeePassRequest = JsonConvert.DeserializeObject<ChangePasswordRequestDTO>(requestBase.Data.ToString());
                                    bool changed = employeeService.ChangePassword(changeEmployeePassRequest.EmployeeId, changeEmployeePassRequest.OldPassword, changeEmployeePassRequest.NewPassword);
                                    if (changed)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Employee password changed successfully.";
                                    }
                                    else
                                    {
                                        responseBase.Message = "Failed to change employee password. Check old password.";
                                    }
                                    break;

                                case "RequestEmployeePasswordReset":
                                    var employeeEmailRequest = JsonConvert.DeserializeObject<EmailRequestDTO>(requestBase.Data.ToString());
                                    bool resetRequested = employeeService.RequestPasswordReset(employeeEmailRequest.Email);
                                    if (resetRequested)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Password reset link sent to employee email.";
                                    }
                                    else
                                    {
                                        responseBase.Message = "Failed to send password reset link.";
                                    }
                                    break;

                                case "ResetEmployeePassword":
                                    var resetEmployeePassRequest = JsonConvert.DeserializeObject<ResetPasswordRequestDTO>(requestBase.Data.ToString());
                                    bool passwordReset = employeeService.UpdatePassword(resetEmployeePassRequest.Email, resetEmployeePassRequest.Token, resetEmployeePassRequest.NewPassword);
                                    if (passwordReset)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Employee password reset successfully.";
                                    }
                                    else
                                    {
                                        responseBase.Message = "Failed to reset employee password. Invalid token or email.";
                                    }
                                    break;*/

                                // ======================================
                                // CÁC CASE CHO CUSTOMER
                                // ======================================
                                case "GetAllCustomers":
                                    var customers = customerService.GetAll();
                                    responseBase.Success = true;
                                    responseBase.Message = "Customers retrieved successfully.";
                                    responseBase.Data = customers;
                                    break;

                                case "GetCustomerById": // Thêm case lấy theo ID
                                    int customerId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var customer = customerService.GetById(customerId);
                                    if (customer != null)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Customer retrieved successfully.";
                                        responseBase.Data = customer;
                                    }
                                    else
                                    {
                                        responseBase.Message = "Customer not found.";
                                    }
                                    break;

                                case "SearchCustomers":
                                    string customerKeyword = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());
                                    var filteredCustomers = customerService.GetByName(customerKeyword);
                                    responseBase.Success = true;
                                    responseBase.Message = "Customers filtered successfully.";
                                    responseBase.Data = filteredCustomers;
                                    break;

                                case "AddCustomer":
                                    CustomerDTO customerToAdd = JsonConvert.DeserializeObject<CustomerDTO>(requestBase.Data.ToString());
                                    customerService.Add(customerToAdd);
                                    responseBase.Success = true;
                                    responseBase.Message = "Customer added successfully.";
                                    break;

                                case "UpdateCustomer":
                                    CustomerDTO customerToUpdate = JsonConvert.DeserializeObject<CustomerDTO>(requestBase.Data.ToString());
                                    customerService.Update(customerToUpdate.ID, customerToUpdate);
                                    responseBase.Success = true;
                                    responseBase.Message = "Customer updated successfully.";
                                    break;

                                case "DeleteCustomer":
                                    int customerIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    customerService.Delete(customerIdToDelete);
                                    responseBase.Success = true;
                                    responseBase.Message = "Customer deleted successfully.";
                                    break;

                                // ======================================
                                // CÁC CASE CHO ORDER
                                // ======================================
                                case "GetAllOrders":
                                    var orders = orderService.GetAllList();
                                    responseBase.Success = true;
                                    responseBase.Message = "Orders retrieved successfully.";
                                    responseBase.Data = orders;
                                    break;

                                case "GetOrderById":
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
                                    break;

                                case "GetOrdersByCustomerId": // Thêm case lấy đơn hàng theo CustomerID
                                    int custId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var customerOrders = orderService.GetOrdersByCustomerId(custId);
                                    responseBase.Success = true;
                                    responseBase.Message = "Orders retrieved successfully by customer ID.";
                                    responseBase.Data = customerOrders;
                                    break;

                                case "GetOrdersByEmployeeId": // Thêm case lấy đơn hàng theo EmployeeID
                                    int empId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var employeeOrders = orderService.GetOrdersByEmployeeId(empId);
                                    responseBase.Success = true;
                                    responseBase.Message = "Orders retrieved successfully by employee ID.";
                                    responseBase.Data = employeeOrders;
                                    break;

                                case "CreateOrder":
                                    var orderWithDetailsCreate = JsonConvert.DeserializeObject<OrderWithDetailsDTO>(requestBase.Data.ToString());
                                    int newOrderId = orderService.CreateOrder(orderWithDetailsCreate.Order, orderWithDetailsCreate.OrderDetails);

                                    responseBase.Success = true;
                                    responseBase.Message = "Order and details created successfully.";
                                    responseBase.Data = newOrderId; // Trả về ID của Order mới tạo
                                    break;

                                case "UpdateOrder":
                                    var orderWithDetailsUpdate = JsonConvert.DeserializeObject<OrderWithDetailsDTO>(requestBase.Data.ToString());
                                    orderService.UpdateOrder(orderWithDetailsUpdate.Order, orderWithDetailsUpdate.OrderDetails);

                                    responseBase.Success = true;
                                    responseBase.Message = "Order and details updated successfully.";
                                    responseBase.Data = null; // Đảm bảo Data là null khi Success là true và client mong đợi bool
                                    break;

                                case "DeleteOrder":
                                    int orderIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    orderService.DeleteOrderAndDetails(orderIdToDelete);
                                    responseBase.Success = true;
                                    responseBase.Message = "Order and its details deleted successfully.";
                                    break;

                                // ======================================
                                // CÁC CASE CHO ORDER DETAILS
                                // ======================================
                                case "GetOrderDetailsByOrderId":
                                    int orderIdForDetails = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    var details = orderDetailsService.GetByOrderID(orderIdForDetails);
                                    responseBase.Success = true;
                                    responseBase.Message = "Order details retrieved successfully.";
                                    responseBase.Data = details;
                                    break;

                                case "GetOrderDetailById":
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
                                    break;

                                case "AddOrderDetail": // Thêm case Add OrderDetail
                                    var orderDetailToAdd = JsonConvert.DeserializeObject<OrderDetailsDTO>(requestBase.Data.ToString());
                                    orderDetailsService.Add(orderDetailToAdd);
                                    responseBase.Success = true;
                                    responseBase.Message = "Order detail added successfully.";
                                    break;

                                case "UpdateOrderDetail": // Thêm case Update OrderDetail
                                    var orderDetailToUpdate = JsonConvert.DeserializeObject<OrderDetailsDTO>(requestBase.Data.ToString());
                                    orderDetailsService.Update(orderDetailToUpdate.ID, orderDetailToUpdate);
                                    responseBase.Success = true;
                                    responseBase.Message = "Order detail updated successfully.";
                                    break;

                                case "DeleteOrderDetail":
                                    int detailIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                    orderDetailsService.Delete(detailIdToDelete);
                                    responseBase.Success = true;
                                    responseBase.Message = "Order detail deleted successfully.";
                                    break;

                                case "BulkAddOrderDetails": // Thêm case Bulk Add OrderDetails
                                    var orderDetailsToAdd = JsonConvert.DeserializeObject<List<OrderDetailsDTO>>(requestBase.Data.ToString());
                                    orderDetailsService.AddOrderDetails(orderDetailsToAdd);
                                    responseBase.Success = true;
                                    responseBase.Message = $"{orderDetailsToAdd.Count} order details added successfully.";
                                    break;

                                /*case "BulkUpdateOrderDetails": // Thêm case Bulk Update OrderDetails
                                    var orderDetailsToUpdate = JsonConvert.DeserializeObject<List<OrderDetailsDTO>>(requestBase.Data.ToString());
                                    orderDetailToUpdate = JsonConvert.DeserializeObject<OrderDetailsDTO>(requestBase.Data.ToString());

                                    orderDetailsService.UpdateOrderDetails(orderDetailToUpdate.ID,orderDetailsToUpdate);
                                    responseBase.Success = true;
                                    responseBase.Message = $"{orderDetailsToUpdate.Count} order details updated successfully.";
                                    break;*/

                                // ======================================
                                // CÁC CASE CHO USER (Nếu có User Service riêng)
                                // ======================================
                                /*case "RegisterUser":
                                    var registerRequest = JsonConvert.DeserializeObject<RegisterRequestDTO>(requestBase.Data.ToString());
                                    userService.Register(registerRequest);
                                    responseBase.Success = true;
                                    responseBase.Message = "User registered successfully. Please confirm your account.";
                                    break;

                                case "ConfirmAccount":
                                    var confirmAccountRequest = JsonConvert.DeserializeObject<ConfirmAccountRequestDTO>(requestBase.Data.ToString());
                                    userService.ConfirmAccount(confirmAccountRequest);
                                    responseBase.Success = true;
                                    responseBase.Message = "Account confirmed successfully.";
                                    break;

                                case "ChangeUserPassword":
                                    var changePassRequest = JsonConvert.DeserializeObject<ChangePasswordRequestDTO>(requestBase.Data.ToString());
                                    bool userPassChanged = userService.ChangePassword(changePassRequest);
                                    if (userPassChanged)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "User password changed successfully.";
                                    }
                                    else
                                    {
                                        responseBase.Message = "Failed to change user password. Check old password or user ID.";
                                    }
                                    break;

                                case "ForgotPassword":
                                    var forgotPassEmailRequest = JsonConvert.DeserializeObject<EmailRequestDTO>(requestBase.Data.ToString());
                                    bool forgotPassSuccess = userService.ForgotPassword(forgotPassEmailRequest);
                                    if (forgotPassSuccess)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Password reset link sent to your email.";
                                    }
                                    else
                                    {
                                        responseBase.Message = "Failed to send password reset link. Email not found or other error.";
                                    }
                                    break;

                                case "ResetPassword":
                                    var resetPassRequest = JsonConvert.DeserializeObject<ResetPasswordRequestDTO>(requestBase.Data.ToString());
                                    bool resetSuccess = userService.ResetPassword(resetPassRequest);
                                    if (resetSuccess)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = "Password reset successfully.";
                                    }
                                    else
                                    {
                                        responseBase.Message = "Failed to reset password. Invalid token or email.";
                                    }
                                    break;

                                case "AuthenticateUser": // Thường dành cho khách hàng đăng nhập
                                    var loginRequest = JsonConvert.DeserializeObject<LoginRequestDTO>(requestBase.Data.ToString());

                                    if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
                                    {
                                        responseBase.Message = "Username and password are required.";
                                    }
                                    else
                                    {
                                        // Giả sử CustomerService hoặc UserService có phương thức Authenticate cho khách hàng
                                        // Tùy vào cách bạn quản lý tài khoản người dùng (khách hàng, nhân viên riêng hay chung)
                                        // Ví dụ: var authenticatedCustomer = customerService.Authenticate(loginRequest.Username, loginRequest.Password);
                                        // Hoặc: var authenticatedUser = userService.Authenticate(loginRequest.Username, loginRequest.Password);
                                        // Hiện tại, chúng ta đã có Authenticate trong EmployeeService. Nếu có thêm cho Customer, bạn có thể gọi ở đây.
                                        // Để đơn giản, nếu EmployeeService xử lý cả user chung, có thể dùng nó.
                                        var authenticatedUser = employeeService.Authenticate(loginRequest.Username, loginRequest.Password); // Sử dụng tạm employeeService nếu chưa có UserService.Authenticate

                                        if (authenticatedUser != null)
                                        {
                                            responseBase.Success = true;
                                            responseBase.Message = "Authentication successful.";
                                            responseBase.Data = authenticatedUser; // Trả về thông tin user hoặc token
                                        }
                                        else
                                        {
                                            responseBase.Message = "Invalid username or password.";
                                        }
                                    }
                                    break;*/

                                default:
                                    responseBase.Message = $"Unknown method: {requestBase.MethodName}";
                                    break;
                                }
                        }
                    }
                    catch (JsonException jEx)
                    {
                        responseBase.Message = $"Invalid JSON format received: {jEx.Message}. Request: '{requestJson}'";
                        Console.Error.WriteLine($"JSON Deserialization Error: {jEx.Message} | Raw request: '{requestJson}'");
                    }
                    catch (Exception ex)
                    {
                        responseBase.Message = $"Server error processing request: {ex.Message}";
                        Console.Error.WriteLine($"Error in HandleClientAsync (outer try-catch): {ex}");
                    }
                    finally
                    {
                        // Luôn gửi phản hồi về client sau mỗi yêu cầu
                        await SendResponse(stream, responseBase);
                    }
                } // End of while loop
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Client disconnected unexpectedly or network error: {ioEx.Message} from {remoteEndPoint}");
            }
            catch (SocketException sockEx)
            {
                Console.WriteLine($"Socket error with client {remoteEndPoint}: {sockEx.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unhandled error in HandleClientAsync for client {remoteEndPoint}: {ex}");
            }
            finally
            {
                client.Close();
                Console.WriteLine($"Client disconnected: {remoteEndPoint}");
            }
        }

        // Phương thức trợ giúp để gửi phản hồi
        private static async Task SendResponse(NetworkStream stream, ServerResponseBase response)
        {
            try
            {
                string responseJson = JsonConvert.SerializeObject(response);
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);

                // QUAN TRỌNG: Gửi 4 bytes độ dài của dữ liệu trước
                byte[] lengthBytes = BitConverter.GetBytes(responseBytes.Length);
                await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length); // Gửi 4 bytes độ dài

                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                await stream.FlushAsync();
                Console.WriteLine($"Sent response to {stream.Socket.RemoteEndPoint}: {responseJson}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error sending response to client: {ex.Message}");
            }
        }
    }
}