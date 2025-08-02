using AutoMapper;
using Azure;
using Azure.Core;
using ElectronicsStore.BusinessLogic;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public static async Task HandleClientAsync(TcpClient client, IMapper mapper, DbContextOptions<ElectronicsStoreContext> dbContextOptions)
        {
            IPEndPoint remoteEndPoint = null;
            
            try
            {
                remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                //Console.WriteLine($"Received from client {remoteEndPoint}");
                client.SendBufferSize = 1024 * 1024;
                client.ReceiveBufferSize = 1024 * 1024;
                // Tạo DbContext mới cho mỗi request và đảm bảo nó được giải phóng
                using var context = new ElectronicsStoreContext(dbContextOptions);
                // Khởi tạo Unit of Work, truyền vào DbContext đã tạo
                var unitOfWork = new UnitOfWork(context);

                // Khởi tạo các repository
                var orderRepository = new OrderRepository(context);
                var orderDetailsRepository = new OrderDetailsRepository(context);
                var productRepository = new ProductRepository(context);
                var categoryRepository = new CategoryRepository(context);
                var manufacturerRepository = new ManufacturerRepository(context);
                var employeeRepository = new EmployeeRepository(context);
                var customerRepository = new CustomerRepository(context);

                // Khởi tạo các service, truyền các repository, mapper và Unit of Work
                // Cập nhật lại các constructor của service để phù hợp
                var categoryService = new CategoryService(categoryRepository, mapper, unitOfWork);
                var manufacturerService = new ManufacturerService(manufacturerRepository, mapper, unitOfWork);
                var productService = new ProductService(productRepository, mapper, unitOfWork);
                var customerService = new CustomerService(customerRepository, mapper, unitOfWork);
                var employeeService = new EmployeeService(employeeRepository, mapper, unitOfWork);
                var orderService = new OrderService(orderRepository, orderDetailsRepository, mapper, unitOfWork);
                var orderDetailsService = new OrderDetailsService(orderDetailsRepository, mapper, unitOfWork);

                NetworkStream stream = client.GetStream();
                byte[] lengthBuffer = new byte[4];

                while (true)
                {
                    ClientRequestBase requestBase = null;
                    ServerResponseBase responseBase = new ServerResponseBase { Success = false, Message = "Unknown action or server error." };
                    string requestJson = null;

                    try
                    {
                        int bytesReadLength = await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length);
                        if (bytesReadLength == 0)
                        {
                            Console.WriteLine($"Client {remoteEndPoint} disconnected.");
                            break;
                        }
                        if (bytesReadLength < 4)
                        {
                            Console.WriteLine($"Error: Did not receive full length prefix from client {remoteEndPoint}.");
                            break;
                        }
                        int requestLength = BitConverter.ToInt32(lengthBuffer, 0);
                        if (requestLength <= 0 || requestLength > 10 * 1024 * 1024)
                        {
                            Console.WriteLine($"Error: Invalid request length ({requestLength}) from client {remoteEndPoint}.");
                            break;
                        }
                        byte[] requestDataBuffer = new byte[requestLength];
                        int totalBytesReadData = 0;
                        int bytesToReadData = requestLength;
                        while (bytesToReadData > 0)
                        {
                            int currentRead = await stream.ReadAsync(requestDataBuffer, totalBytesReadData, bytesToReadData);
                            if (currentRead == 0)
                            {
                                Console.WriteLine($"Client {remoteEndPoint} disconnected unexpectedly while sending data.");
                                break;
                            }
                            totalBytesReadData += currentRead;
                            bytesToReadData -= currentRead;
                        }
                        if (totalBytesReadData < requestLength)
                        {
                            Console.WriteLine($"Error: Did not receive full data for request from client {remoteEndPoint}.");
                            break;
                        }

                        requestJson = Encoding.UTF8.GetString(requestDataBuffer, 0, totalBytesReadData);
                        Console.WriteLine($"Received from client {remoteEndPoint}: {requestJson}");
                        requestBase = JsonConvert.DeserializeObject<ClientRequestBase>(requestJson);
                        if (requestBase == null || string.IsNullOrWhiteSpace(requestBase.MethodName))
                        {
                            responseBase.Message = "Invalid request: MethodName is missing.";
                        }
                        else
                        {
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
                                    string categoryKeyword = requestBase.Data.ToString();
                                    var filteredCategories = categoryService.GetByName(categoryKeyword);
                                    responseBase.Success = true;
                                    responseBase.Message = "Categories filtered successfully.";
                                    responseBase.Data = filteredCategories;
                                    break;
                               /* case "GetCategoriesByName":
                                    // Lấy đối tượng JToken từ requestBase.Data
                                    var dataToken = (Newtonsoft.Json.Linq.JToken)requestBase.Data;
                                    string categoryKeyword = dataToken.ToObject<string>();
                                    var filteredCategories = categoryService.GetByName(categoryKeyword);
                                    responseBase.Success = true;
                                    responseBase.Message = "Categories filtered successfully.";
                                    responseBase.Data = filteredCategories;
                                    break;*/

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
                                    string manufacturerNameKeyword = requestBase.Data.ToString();
                                    var filteredManufacturers = manufacturerService.GetByName(manufacturerNameKeyword);
                                    responseBase.Success = true;
                                    responseBase.Message = "Manufacturers retrieved successfully by name.";
                                    responseBase.Data = filteredManufacturers;
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
                                    try
                                    {
                                        var products = productService.GetAllList();
                                        responseBase.Data = products;
                                        responseBase.Success = true;
                                        responseBase.Message = "Products retrieved successfully.";
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving all products: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

                                case "GetProductById":
                                    try
                                    {
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
                                            responseBase.Success = false;
                                            responseBase.Message = "Product not found.";
                                            responseBase.Data = null;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving product by ID: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

                                case "GetProductImage":
                                    try
                                    {
                                        string fileName = requestBase.Data.ToString();
                                        string serverImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", fileName);

                                        if (File.Exists(serverImagePath))
                                        {
                                            byte[] imageData = await File.ReadAllBytesAsync(serverImagePath);
                                            // Chuyển đổi mảng byte thành chuỗi Base64 để gửi qua mạng
                                            string base64Image = Convert.ToBase64String(imageData);

                                            responseBase.Success = true;
                                            responseBase.Message = "Image data sent successfully.";
                                            responseBase.Data = base64Image;
                                        }
                                        else
                                        {
                                            responseBase.Success = false;
                                            responseBase.Message = "Image not found on server.";
                                            responseBase.Data = null;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving image: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

                                /*Thêm case mới để xử lý yêu cầu lấy ảnh
                               case "GetProductImage":
                                   {
                                   // Dữ liệu được gửi lên là tên file ảnh (string)
                                   // Sử dụng JsonConvert.DeserializeObject để lấy tên file từ trường Data
                                   string? fileName = JsonConvert.DeserializeObject<string>(requestBase.Data.ToString());

                                   if (string.IsNullOrEmpty(fileName))
                                   {
                                       responseBase = new ServerResponseBase { Success = false, Message = "File name is required." };
                                   }
                                   else
                                   {
                                       // Gọi ProductService để lấy mảng byte của ảnh
                                       byte[]? imageData = productService.GetProductImage(fileName);

                                       if (imageData != null)
                                       {
                                           // Trả về mảng byte của ảnh
                                           responseBase = new ServerResponseBase { Data = imageData, Success = true };
                                       }
                                       else
                                       {
                                           // Trả về lỗi nếu không tìm thấy file
                                           responseBase = new ServerResponseBase { Success = false, Message = "Image not found." };
                                       }
                                   }
                                   }
                                   break;*/

                                case "SearchProducts":
                                    try
                                    {
                                        string productKeyword;
        
                                        // Handle cases where Data is a string or a JToken.
                                        // This is a more robust way to extract the keyword.
                                        if (requestBase.Data is string dataString)
                                        {
                                            productKeyword = dataString;
                                        }
                                        else
                                        {
                                            // If it's not a string, assume it's a JToken and try to convert it.
                                            // This handles cases where the payload is sent as a JSON object, even if it just contains one value.
                                            productKeyword = (requestBase.Data as Newtonsoft.Json.Linq.JToken)?.ToObject<string>();
                                        }

                                        // Add a null check before calling GetByName to prevent the "Value cannot be null" error.
                                        var filteredProducts = productService.GetByName(productKeyword ?? string.Empty);

                                        responseBase.Success = true;
                                        responseBase.Message = "Products retrieved successfully by name.";
                                        responseBase.Data = filteredProducts;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error processing SearchProducts: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

                                case "AddProduct":
                                    try
                                    {
                                        var productToAdd = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                                        var addedProduct = productService.Add(productToAdd);
                                        responseBase.Success = true;
                                        responseBase.Message = "Product added successfully.";
                                        responseBase.Data = addedProduct; // Gán đối tượng đã thêm vào Data
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error adding product: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

                                case "UpdateProduct":
                                    try
                                    {
                                        var productToUpdate = JsonConvert.DeserializeObject<ProductDTO>(requestBase.Data.ToString());
                                        var updatedProduct = productService.Update(productToUpdate.ID, productToUpdate);
                                        responseBase.Success = true;
                                        responseBase.Message = "Product updated successfully.";
                                        responseBase.Data = updatedProduct; // Gán đối tượng đã cập nhật vào Data
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error updating product: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;
                                case "DeleteProduct":
                                    try
                                    {
                                        int productIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        bool success = productService.Delete(productIdToDelete);

                                        responseBase.Success = success;
                                        responseBase.Message = success ? "Product deleted successfully." : "Failed to delete product or product not found.";
                                        responseBase.Data = success; // Có thể trả về true/false để client dễ xử lý
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error deleting product: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

                                case "UploadProductImage":
                                    try
                                    {
                                        // Deserialize đối tượng ImageUploadRequestDTO từ request
                                        var requestData = JsonConvert.DeserializeObject<ImageUploadRequestDTO>(requestBase.Data.ToString());

                                        // Kiểm tra xem dữ liệu có hợp lệ không
                                        if (requestData == null || string.IsNullOrEmpty(requestData.FileName) || requestData.ImageData == null)
                                        {
                                            responseBase.Success = false;
                                            responseBase.Message = "Invalid image upload data.";
                                            responseBase.Data = null;
                                            break;
                                        }

                                        // Tạo đường dẫn đầy đủ để lưu file ảnh trên server
                                        // (Kiểm tra lại đường dẫn này cho chính xác, ví dụ: "Images/Products")
                                        string serverImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", requestData.FileName);

                                        // Ghi mảng byte của ảnh vào file
                                        File.WriteAllBytes(serverImagePath, requestData.ImageData);

                                        // Trả về phản hồi thành công
                                        responseBase.Success = true;
                                        responseBase.Message = "Image uploaded successfully.";
                                        responseBase.Data = true; // Trả về true để client xác nhận thành công
                                    }
                                    catch (Exception ex)
                                    {
                                        // Xử lý lỗi nếu có trong quá trình lưu file
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error uploading image: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

                                /* case "DeleteProduct":
                                     try
                                     {
                                         int productIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                         bool success = productService.Delete(productIdToDelete);

                                         responseBase.Success = success;
                                         responseBase.Message = success ? "Product deleted successfully." : "Failed to delete product or product not found.";
                                         responseBase.Data = null; // Không cần trả về dữ liệu sau khi xóa
                                     }
                                     catch (Exception ex)
                                     {
                                         responseBase.Success = false;
                                         responseBase.Message = $"Error deleting product: {ex.Message}";
                                         responseBase.Data = null;
                                     }
                                     break;*/

                                case "BulkAddProducts":
                                    // Giữ nguyên logic của bạn, nhưng thêm try-catch ở mức cao hơn để đảm bảo không crash
                                    try
                                    {
                                        var productsToImport = JsonConvert.DeserializeObject<List<ProductDTO>>(requestBase.Data.ToString());
                                        int productSuccessCount = 0;
                                        foreach (var dto in productsToImport)
                                        {
                                            try
                                            {
                                                // Gọi phương thức Add đã được sửa để lấy ID mới nếu cần
                                                productService.Add(dto);
                                                productSuccessCount++;
                                            }
                                            catch (Exception exInner)
                                            {
                                                // Log lỗi chi tiết của từng sản phẩm
                                                Console.WriteLine($"Error adding product during bulk import: {exInner.Message} - Product: {dto.ProductName}");
                                            }
                                        }
                                        responseBase.Success = true;
                                        responseBase.Message = $"{productSuccessCount} products imported successfully.";
                                        responseBase.Data = null;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error during bulk product import: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;

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
                                    string employeeKeyword = requestBase.Data.ToString();
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

                                case "Authenticate": // Đối với nhân viên đăng nhập
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
                                    string customerKeyword = requestBase.Data.ToString();
                                    var filteredCustomers = customerService.GetByName(customerKeyword);
                                    responseBase.Success = true;
                                    responseBase.Message = "Customers filtered successfully.";
                                    responseBase.Data = filteredCustomers;
                                    break;

                                case "AddReturnCustomer":
                                    try
                                    {
                                        var customerToAdd = JsonConvert.DeserializeObject<CustomerDTO>(requestBase.Data.ToString());
                                        // Gọi hàm Add đã được sửa đổi và lấy kết quả trả về
                                        var addedCustomer = customerService.AddReturn(customerToAdd);

                                        responseBase.Success = true;
                                        responseBase.Message = "Customer added successfully.";
                                        // Gán đối tượng đã được thêm thành công (có ID) vào Data
                                        responseBase.Data = addedCustomer;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error adding customer: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;
                                case "AddCustomer":
                                    try
                                    {
                                        var customerToAdd = JsonConvert.DeserializeObject<CustomerDTO>(requestBase.Data.ToString());
                                        customerService.Add(customerToAdd);
                                        responseBase.Success = true;
                                        responseBase.Message = "Customer added successfully.";
                                        responseBase.Data = customerToAdd; 
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error adding customer: {ex.Message}";
                                        responseBase.Data = null; // Hoặc một đối tượng CustomerDTO rỗng
                                    }
                                    break;

                                case "UpdateCustomer":
                                    try
                                    {
                                        CustomerDTO customerToUpdate = JsonConvert.DeserializeObject<CustomerDTO>(requestBase.Data.ToString());
                                        customerService.Update(customerToUpdate.ID, customerToUpdate);
                                        responseBase.Success = true;
                                        responseBase.Message = "Customer updated successfully.";
                                        responseBase.Data = true; 
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error updating customer: {ex.Message}";
                                        responseBase.Data = false; // <--- Sửa ở đây để trả về false khi có lỗi
                                    }
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
                                    try
                                    {
                                        int id = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        var order = orderService.GetById(id);
                                        if (order != null)
                                        {
                                            responseBase.Success = true;
                                            responseBase.Message = "Order retrieved successfully.";
                                            responseBase.Data = order;
                                        }
                                        else
                                        {
                                            responseBase.Success = false;
                                            responseBase.Message = $"Order with ID {id} not found.";
                                        }
                                    }
                                    catch (Exception ex)        
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving order: {ex.Message}";
                                    }
                                    break;
                                case "SearchOrder":
                                    try
                                    {
                                        int searchorderId = requestBase.Data is long idLong ? (int)idLong : JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        var order = orderService.GetById(searchorderId);
                                        List<OrderDTO> orderList = new List<OrderDTO>();

                                        if (order != null)
                                        {
                                            orderList.Add(order);
                                            responseBase.Success = true;
                                            responseBase.Message = "Order retrieved successfully.";
                                            //responseBase.Data = order;
                                        }
                                        else
                                        {
                                            responseBase.Success = false;
                                            responseBase.Message = $"Order with ID {searchorderId} not found.";
                                        }
                                        responseBase.Data = orderList; // Luôn trả về một danh sách
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving order: {ex.Message}";
                                        responseBase.Data = null;
                                    }
                                    break;
                                case "GetOrdersByCustomerId":
                                    try
                                    {
                                        int custId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        var customerOrders = orderService.GetOrdersByCustomerId(custId);
                                        responseBase.Success = true;
                                        responseBase.Message = "Orders retrieved successfully by customer ID.";
                                        responseBase.Data = customerOrders;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving orders by customer ID: {ex.Message}";
                                    }
                                    break;
                                case "GetOrdersByEmployeeId":
                                    try
                                    {
                                        int empId = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        var employeeOrders = orderService.GetOrdersByEmployeeId(empId);
                                        responseBase.Success = true;
                                        responseBase.Message = "Orders retrieved successfully by employee ID.";
                                        responseBase.Data = employeeOrders;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving orders by employee ID: {ex.Message}";
                                    }
                                    break;
                                case "GetOrdersByStatus":
                                    var status = requestBase.Data.ToString();
                                    var filteredOrders = orderService.GetByStatus(status); // Cần thêm phương thức này vào OrderService
                                    responseBase.Success = true;
                                    responseBase.Message = $"Orders with status '{status}' retrieved successfully.";
                                    responseBase.Data = filteredOrders;
                                    break;

                                case "CreateOrder":
                                    try
                                    {
                                        var orderWithDetailsCreate = JsonConvert.DeserializeObject<OrderWithDetailsDTO>(requestBase.Data.ToString());
                                        int newOrderId = orderService.CreateOrder(orderWithDetailsCreate.Order, orderWithDetailsCreate.OrderDetails);

                                        responseBase.Success = true;
                                        responseBase.Message = "Order and details created successfully.";
                                        responseBase.Data = newOrderId;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error creating order: {ex.Message}";
                                        responseBase.Data = -1;
                                    }
                                    break;
                                case "CreateTmpOrder":
                                    try
                                    {
                                        var orderWithDetailsCreate = JsonConvert.DeserializeObject<OrderWithDetailsDTO>(requestBase.Data.ToString());
                                        int newOrderId = orderService.CreateTmpOrder(orderWithDetailsCreate.Order, orderWithDetailsCreate.OrderDetails);

                                        responseBase.Success = true;
                                        responseBase.Message = "Order and details created successfully.";
                                        responseBase.Data = newOrderId;
                                    }
                                    catch (Exception ex)
                                    {
                                        // Log lỗi chi tiết ở server để debug dễ hơn
                                        Console.WriteLine($"Error creating order: {ex.Message}");
                                        if (ex.InnerException != null)
                                        {
                                            Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                                        }

                                        responseBase.Success = false;
                                        responseBase.Message = $"Server error for action 'CreateOrder': {ex.Message}";
                                        responseBase.Data = -1; // Trả về -1 để báo hiệu lỗi
                                    }
                                    break;

                                case "UpdateOrderStatus":
                                    var orderStatusData = JObject.Parse(requestBase.Data.ToString());
                                    int orderId = (int)orderStatusData["ID"];
                                    string newStatus = (string)orderStatusData["Status"];

                                    bool updated = orderService.UpdateStatus(orderId, newStatus); // Cần thêm phương thức này vào OrderService
                                    if (updated)
                                    {
                                        responseBase.Success = true;
                                        responseBase.Message = $"Order {orderId} status updated to '{newStatus}' successfully.";
                                        responseBase.Data = true;
                                    }
                                    else
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Order {orderId} not found or status update failed.";
                                        responseBase.Data = false;
                                    }
                                    break;

                                case "UpdateOrder":
                                    try
                                    {
                                        var orderWithDetailsUpdate = JsonConvert.DeserializeObject<OrderWithDetailsDTO>(requestBase.Data.ToString());
                                        orderService.UpdateOrder(orderWithDetailsUpdate.Order, orderWithDetailsUpdate.OrderDetails);

                                        responseBase.Success = true;
                                        responseBase.Message = "Order and details updated successfully.";
                                        responseBase.Data = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error updating order: {ex.Message}";
                                        responseBase.Data = false;
                                    }
                                    break;

                                case "DeleteOrder":
                                    try
                                    {
                                        int orderIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        orderService.DeleteOrderAndDetails(orderIdToDelete);
                                        responseBase.Success = true;
                                        responseBase.Message = "Order and its details deleted successfully.";
                                        responseBase.Data = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error deleting order: {ex.Message}";
                                        responseBase.Data = false;
                                    }
                                    break;
                                case "ConfirmOrder":
                                    var confirmOrderDto = JsonConvert.DeserializeObject<ConfirmOrderDTO>(requestBase.Data.ToString());
                                    orderService.ConfirmOrder(confirmOrderDto);
                                    responseBase.Success = true;
                                    responseBase.Data = true; 
                                    break;


                                // ======================================
                                // CÁC CASE CHO ORDER DETAILS
                                // ======================================

                                case "GetOrderDetailsByOrderId":
                                    try
                                    {
                                        int orderIdForDetails = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        var details = orderDetailsService.GetByOrderID(orderIdForDetails);
                                        responseBase.Success = true;
                                        responseBase.Message = "Order details retrieved successfully.";
                                        responseBase.Data = details;
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving order details: {ex.Message}";
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
                                            responseBase.Success = false;
                                            responseBase.Message = $"Order detail with ID {detailId} not found.";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error retrieving order detail: {ex.Message}";
                                    }
                                    break;

                                case "AddOrderDetail":
                                    try
                                    {
                                        var orderDetailToAdd = JsonConvert.DeserializeObject<OrderDetailsDTO>(requestBase.Data.ToString());
                                        orderDetailsService.Add(orderDetailToAdd);
                                        responseBase.Success = true;
                                        responseBase.Message = "Order detail added successfully.";
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error adding order detail: {ex.Message}";
                                    }
                                    break;

                                case "UpdateOrderDetail":
                                    try
                                    {
                                        var orderDetailToUpdate = JsonConvert.DeserializeObject<OrderDetailsDTO>(requestBase.Data.ToString());
                                        orderDetailsService.Update(orderDetailToUpdate.ID, orderDetailToUpdate);
                                        responseBase.Success = true;
                                        responseBase.Message = "Order detail updated successfully.";
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error updating order detail: {ex.Message}";
                                    }
                                    break;

                                case "DeleteOrderDetail":
                                    try
                                    {
                                        int detailIdToDelete = JsonConvert.DeserializeObject<int>(requestBase.Data.ToString());
                                        orderDetailsService.Delete(detailIdToDelete);
                                        responseBase.Success = true;
                                        responseBase.Message = "Order detail deleted successfully.";
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error deleting order detail: {ex.Message}";
                                    }
                                    break;

                                case "BulkAddOrderDetails":
                                    try
                                    {
                                        var orderDetailsToAdd = JsonConvert.DeserializeObject<List<OrderDetailsDTO>>(requestBase.Data.ToString());
                                        orderDetailsService.AddOrderDetails(orderDetailsToAdd);
                                        responseBase.Success = true;
                                        responseBase.Message = $"{orderDetailsToAdd.Count} order details added successfully.";
                                    }
                                    catch (Exception ex)
                                    {
                                        responseBase.Success = false;
                                        responseBase.Message = $"Error adding multiple order details: {ex.Message}";
                                    }
                                    break;
                                default:
                                    responseBase.Message = $"Unknown method: {requestBase.MethodName}";
                                    break;
                            }
                        }
                    }
                    catch (JsonException jEx)
                    {
                        responseBase.Success = false;
                        responseBase.Message = $"Invalid JSON format received: {jEx.Message}. Request: '{requestJson}'";
                        Console.Error.WriteLine($"JSON Deserialization Error: {jEx.Message} | Raw request: '{requestJson}'");
                    }
                    catch (Exception ex)
                    {
                        responseBase.Success = false;
                        responseBase.Message = $"Server error processing request: {ex.Message}";
                     
                        Console.Error.WriteLine($"Error in HandleClientAsync (inner try-catch): {ex}");
                    }
                    finally
                    {
                        await SendResponse(stream, responseBase);
                    }
                }
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

        private static async Task SendResponse(NetworkStream stream, ServerResponseBase response)
        {
            try
            {
                string responseJson = JsonConvert.SerializeObject(response);
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);
                byte[] lengthBytes = BitConverter.GetBytes(responseBytes.Length);
                await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length);
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