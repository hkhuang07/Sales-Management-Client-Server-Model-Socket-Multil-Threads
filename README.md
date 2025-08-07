# 🚀 Sales Management System - Client-Server Model (4 Layer Client-Server-Business Logic-Data Access), Unit of Work Model, NewtonJSON - TCP/Socket/Mutil Threads Process 📈

## Project Overview

Welcome to the **Sales Management System**! This repository presents an exceptionally powerful and meticulously engineered solution, specifically crafted to meet the demanding needs of modern electronics stores. Far beyond a simple application, this system is a testament to sophisticated software design, built on a robust **Client-Server architecture** that ensures reliability, scalability, and high performance.

<p align="center">
  <h3>Sales Process </h3>
  <img src="demo/video/process.gif" alt="Server-Client Sales Process">
  <p><em> Server Console & Client Sales Process Interface.</em></p>
</p>

### Server & Client Overview
A look at the core interfaces for both the server console and the client application.

<p align="center">
  <h3>Server & Client Overview </h3>
  <img src="demo/server00.jpg" alt="Server Console">
  <img src="demo/server.jpg" alt="Server Console">
  <img src="demo/client.jpg" alt="Client Interface">
  <p><em> A quick glimpse of the Server Console and the Client's Main Interface.</em></p>
</p>

Our core design principle focuses on handling concurrent operations with unmatched efficiency, made possible through advanced **multi-threading**. The system adheres strictly to a clean and maintainable **4-Layer Architecture (Client -> Server -> Business Logic -> Data Access)**, which provides a clear separation of concerns, simplifies maintenance, and guarantees rock-solid data integrity. For data persistence, we leverage the power of **.NET Entity Framework Core** and **SQL Server**, forming a solid foundation for all data operations.

This introduction provides a comprehensive and detailed look into the technologies and architectural patterns that make this project a premier example of modern application development.
### Sales Process Flow
This GIF illustrates the seamless flow of data from the client's sales interface to the server console, showcasing the real-time processing of sales transactions.

---
## 📸 Media Gallery & Feature Showcase

This section provides a dynamic visual walkthrough of the system's key functionalities.

### Secure Authentication
This demonstrates the robust authentication process for both staff and administrators, highlighting the server's role in validating credentials.

<p align="center">
  <h3>Server & Client Authentication </h3>
  <img src="demo/video/auth_user.gif" alt="Server-Client Authentication">
  <p><em> Authentication process for standard users/employees.</em></p>
  <img src="demo/video/auth_admin.gif" alt="Server-Client Authentication">
  <p><em> Authentication process for administrators, showcasing elevated access rights.</em></p>
</p>

### Inventory Management
A series of visual demonstrations highlighting the CRUD operations for key data entities.

<p align="center">
  <h3>Server & Client Categories </h3>
  <img src="demo/video/categories.gif" alt="Server-Client Categories">
  <p><em> Managing product categories in real-time.</em></p>
</p>
<p align="center">
  <h3>Server & Client Manufacturer </h3>
  <img src="demo/video/manufacturers.gif" alt="Server-Client Manufacturer">
  <p><em> Handling manufacturer information across the system.</em></p>
</p>
<p align="center">
  <h3>Server & Client Products </h3>
  <img src="demo/video/products.gif" alt="Server-Client Products">
  <p><em> A detailed view of product management functionalities.</em></p>
</p>

### Customer & Employee Records
Managing the core stakeholders of the business.

<p align="center">
  <h3>Server & Client Customers </h3>
  <img src="demo/video/customers.gif" alt="Server-Client Customers">
  <p><em> The customer management interface.</em></p>
</p>
<p align="center">
  <h3>Server & Client Employees </h3>
  <img src="demo/video/employees.gif" alt="Server-Client Employees">
  <p><em> The employee management interface for admins.</em></p>
</p>

### Order Processing
A look at the comprehensive order and order details management.

<p align="center">
  <h3>Server & Client Order and Details </h3>
  <img src="demo/video/order_orderdetails.gif" alt="Server-Client Order and Details">
  <p><em> Processing and viewing order and order detail information.</em></p>
</p>

### In-depth Statistics & Reporting
Gain valuable insights with powerful statistical analysis tools.

<p align="center">
  <h3>Server & Client Product Statistics </h3>
  <img src="demo/video/productstatistic.gif" alt="Server-Client Product Statistics">
  <p><em> Detailed statistics on product sales and performance.</em></p>
</p>
<p align="center">
  <h3>Server & Client Revenue Statistics </h3>
  <img src="demo/video/revenuestatistic.gif" alt="Server-Client Revenue Statistics">
  <p><em> Visualizing revenue trends and data.</em></p>
</p>

### Supplemental Information
Supporting documentation and software details.

<p align="center">
  <h3>Help Center </h3>
  <img src="demo/video/helpcenter.gif" alt="Server-Client Help Center">
  <p><em> Accessing the integrated Help Center website for assistance.</em></p>
</p>
<p align="center">
  <h3>Software Information </h3>
  <img src="demo/video/softwareinfor.gif" alt="Server-Client Software Information">
  <p><em> A look at the software information and version details.</em></p>
</p>
---

## ✨ Key Architectural Highlights & Advanced Technologies ✨

This project is engineered with a powerful stack of technologies and a thoughtful architectural design, ensuring a high-performance, scalable, and secure application.

### 🌐 The Client-Server Model with Multi-threaded Processing
At the heart of this system lies a sophisticated **Client-Server model** built on standard TCP/IP sockets. This architecture allows the client application (the user interface) to be completely decoupled from the server, where all the heavy lifting and data processing occur.

* **Server Core & Listening**: Our server application, initiated in `Program.cs`, acts as the central command center. It uses a `TcpListener` to continuously and asynchronously **listen for incoming client connections** on a dedicated port, which is configured to be `301`. This listening loop is the entry point for all client communication.
    
    ```csharp
    listener = new TcpListener(IPAddress.Any, PORT);
    listener.Start();
    Console.WriteLine($"Server started. Listening on port {PORT}...");
    while (true)
    {
        TcpClient client = await listener.AcceptTcpClientAsync();
        // ...
    }
    ```
    
* **⚡ Multi-threaded Processing**: To prevent a single client from monopolizing server resources, each new client connection is handled with an elegant **asynchronous multi-threaded approach**. Once a client connects, the server immediately spawns a new task using `Task.Run(() => ServerHandler.HandleClientAsync(client, ...))`. This ensures that each client request is processed on a separate thread, allowing the server to handle **countless simultaneous connections** without any blocking, which is critical for a responsive and high-throughput system.
    
* **🔌 Socket Communication**: The foundation of our communication is a direct, highly efficient, and low-latency protocol built on raw **TCP sockets**. This choice of protocol over higher-level abstractions like HTTP ensures minimal overhead and maximum speed, which is crucial for real-time data exchange in a busy sales environment.
    
* **📦 Robust Message Framing with Length-Prefixed Protocol**: To overcome the inherent challenges of stream-based socket communication (where messages can be split or combined), we implemented a custom, highly reliable **length-prefixed messaging protocol**. Before sending the actual JSON payload, the client first sends a 4-byte integer representing the total length of the message. The server (and vice versa) then reads exactly that many bytes, guaranteeing the reception of the complete and untruncated message. This is a powerful feature implemented meticulously in both `ClientService.cs` and `ServerHandler.cs`.
    
    ```csharp
    // Client side:
    byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
    byte[] lengthBytes = BitConverter.GetBytes(requestBytes.Length);
    await stream.WriteAsync(lengthBytes, 0, lengthBytes.Length);
    await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
    ```
    

---

### 🎨 4-Layer Architectural Pattern (Client -> Server -> Business Logic Layer -> Data Access Layer) Data is transported using Data Transfer Objects (DTOs)
The entire system is a masterclass in clean architecture, meticulously organized into four distinct layers. This clear separation of responsibilities promotes modularity, testability, and long-term maintainability.

1.  **🖥️ Client Layer (`ElectronicsStore.Presentation`, `ElectronicsStore.Client`)**: This is the user's primary interface, built as a rich **Windows Forms UI**. It's responsible for all user interaction, displaying data, and translating user actions into concrete requests. The `ClientService` component within this layer handles all communication with the server, abstracting away the networking complexities from the UI.
2.  **⚙️ Server Layer (`ElectronicsStore.Server`)**: Acting as the intelligent intermediary, the server layer is the control center for client requests. The `ServerHandler` is a key component here, responsible for:
    * Receiving and deserializing incoming JSON requests using **Newtonsoft.Json**.
    * Dynamically invoking the appropriate business logic based on the `MethodName` property in the request.
    * Meticulously crafting and serializing the response object before sending it back to the client.
3.  **🧠 Business Logic Layer (BLL - `ElectronicsStore.BusinessLogic`)**: This is the "brain" of the application, containing all the core business rules and operations (e.g., `EmployeeService`, `OrderService`). This layer is blissfully unaware of the client or the database. It handles all data validation, complex calculations, and workflow orchestration. A critical design choice here is the use of **Data Transfer Objects (DTOs)**, which are plain objects used for clean and efficient data exchange between layers, preventing direct exposure of internal domain models.
4.  **🗄️ Data Access Layer (DAL - `ElectronicsStore.DataAccess`)**: The data guardian. This layer is solely dedicated to seamless and secure interaction with the database. It contains robust repositories (e.g., `IOrderRepository`, `OrderRepository`) that abstract away the complexities of the underlying data storage mechanism.

---

### 💾 Robust Data Management & Transaction Integrity

* **.NET Entity Framework Core & SQL Server**: We harness the power of **.NET Entity Framework Core**, a cutting-edge Object-Relational Mapper (ORM), to effortlessly interact with our **SQL Server** database. This magical tool simplifies all data operations (CRUD), enforces strong type-safety, and dramatically reduces boilerplate code for database interactions.
    
* **🛡️ Unit of Work Pattern**: To ensure absolute data consistency and integrity, we employ the **Unit of Work pattern**. This pattern guarantees that all database operations within a single business transaction (e.g., creating a new order with its details) are treated as a single, atomic unit. The `UnitOfWork` class manages the `DbContext` and ensures that all changes are either successfully committed together or rolled back completely if any part of the transaction fails, preventing partial or inconsistent data states.
    
    ```csharp
    // Inside ServerHandler, a new UnitOfWork is created for each request
    using var context = new ElectronicsStoreContext(dbContextOptions);
    var unitOfWork = new UnitOfWork(context);
    // ...
    // Inside a service method:
    // Perform multiple repository operations
    await _unitOfWork.CommitAsync(); // All changes are committed as a single transaction
    ```
    
* **Data Transfer Objects (DTOs)**: As part of the multi-layered architecture, DTOs are extensively used to transfer data between the client, server, and business logic. They represent a simplified view of the domain models and are optimized for serialization and deserialization, which is handled efficiently using **Newtonsoft.Json** for all network communication.
    
    ```csharp
    // Client sending a request with a DTO payload
    ClientRequest<LoginRequestDto> request = new ClientRequest<LoginRequestDto>("Login", loginDto);
    string requestJson = JsonConvert.SerializeObject(request);
    
    // Server receiving and deserializing the request
    ClientRequestBase requestBase = JsonConvert.DeserializeObject<ClientRequestBase>(requestJson);
    ```
    
---

## Project Structure (Conceptual)
```
├── ElectronicsStore.sln
├── ElectronicsStore.Client (Presentation) (Windows Forms UI, ClientService for server communication)
│   ├── ClientService.cs
│   ├── frmMain.cs
│   └── ... (Other UI forms)
├── ElectronicsStore.Server (Server application for handling client requests)
│   ├── Program.cs (Main server entry point)
│   └── ServerHandler.cs (Handles client requests, invokes BLL)
├── ElectronicsStore.BusinessLogic (Business Logic Layer)
│   ├── OrderService.cs
│   ├── EmployeeService.cs
│   ├── ProductService.cs
│   ├── CustomerService.cs
│   ├── MappingProfile.cs (AutoMapper configuration)
│   ├── MappingConfig.cs (FluentValidation configuration)
│   └── ... (Other service classes)
├── ElectronicsStore.DataAccess (Data Access Layer)
│   ├── IOrderRepository.cs
│   ├── OrderRepository.cs
│   ├── ElectronicsStoreContext.cs (EF Core DbContext)
│   └── ... (Other repository interfaces and implementations)
├── ElectronicsStore.DataTransferObject (DTOs for data exchange)
│   ├── OrderDTO.cs
│   ├── ProductDTO.cs
│   ├── LoginRequestDTO.cs
│   ├── LoginResponseDTO.cs
│   └── ... (All other DTOs)
└── ElectronicsStore.Models (Entity Framework Core models/entities)
├── Orders.cs
├── Employees.cs
├── Customers.cs
├── Products.cs
├── Order_Details.cs
└── ... (All other database entities)
```

## 🚀 How to Run (General Steps) 🚀

Getting this system up and running is straightforward:

1.  **🗃️ Database Setup:**
    * Ensure **SQL Server** is installed and purring. 🐢
    * Restore the database backup (if provided) or simply run **Entity Framework migrations** to automatically sculpt the database schema. 🛠️
    * **Crucially**, update the connection strings in `appsettings.json` (or `App.config` for older projects) within both the Server and potentially Client projects to point directly to your SQL Server instance. 🔗

2.  **💻 Server Application:**
    * Open the entire solution in **Visual Studio**. 📂
    * Build the `ElectronicsStore.Server` project. 🏗️
    * Execute the `ElectronicsStore.Server` application. Watch as it gracefully starts listening on the configured port (defaulting to `301`). 👂

3.  **🖥️ Client Application:**
    * Build the `ElectronicsStore.Client` project. 🏗️
    * Launch the `ElectronicsStore.Client` application (e.g., `frmMain`). 🏁
    * Double-check that the `ServerIp` and `ServerPort` configured in the client perfectly match the server's listening address and port. Seamless connection guaranteed! 🤝

## 👤 Author 👤

**Huynh Quoc Huy**

* **GitHub Profile:** [https://github.com/hkhuang07](https://github.com/hkhuang07) 🌟
* **Project Repository:** [https://github.com/hkhuang07/Sales-Management-Client-Server-Model-Socket-Multil-Threads](https://github.com/hkhuang07/Sales-Management-Client-Server-Model-Socket-Multil-Threads) 🔗
