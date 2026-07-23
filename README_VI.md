# 🛒 Hệ Thống Quản Lý Cửa Hàng Điện Tử - Kiến Trúc Client-Server ⚡

> **Hệ thống Đa luồng WinForms C# .NET & ASP.NET Core Web API Hiệu năng cao**  
> Xây dựng theo Kiến trúc Clean Architecture 4 lớp, Mẫu thiết kế Unit of Work & Repository, EF Core & SQL Server.

---

## 📌 Tổng Quan Dự Án

Hệ thống Quản lý Cửa hàng Điện tử là giải pháp phần mềm doanh nghiệp cấp sản xuất được thiết kế riêng cho quản lý bán hàng POS và kho hàng linh kiện/thiết bị điện tử.

Hệ thống triển khai kiến trúc **Client-Server** giao tiếp qua **RESTful Web API và SignalR Real-Time**, đảm bảo thời gian phản hồi tức thì, xử lý đồng thời đa người dùng, toàn vẹn dữ liệu và bảo mật cao với **BCrypt Password Hashing & JWT Authentication**.

---

## 🔐 Bảo Mật & Xác Thực Nâng Cao

* **BCrypt Hashing**: Mật khẩu được mã hóa an toàn bằng thuật toán BCrypt trước khi lưu trữ vào cơ sở dữ liệu.
* **JWT Bearer Token**: Xác thực và phân quyền giữa Client và Server sử dụng JSON Web Token.
* **Tài khoản mặc định (Seed Data)**:
  * **Admin**: Username `linsirui` | Mật khẩu `1111111111`
  * **Staff**: Username `huynhquochuy` | Mật khẩu `0000000000`

---

## 🛠️ Hướng Dẫn Cài Đặt & Khởi Chạy

### 1. 📋 Yêu Cầu Tiền Đề
* **.NET 8.0 SDK** trở lên.
* **Visual Studio 2022** (với workload .NET Desktop Development).
* **SQL Server** hoặc **SQL Server LocalDB**.

### 2. 🗄️ Cấu Hình Cơ Sở Dữ Liệu
Cập nhật chuỗi kết nối trong `src/Server/appsettings.json` và chạy lệnh cập nhật database:
```bash
dotnet ef database update --project src/DataAccessLayer --startup-project src/Server
```

### 3. 🚀 Chạy Ứng Dụng

1. **Khởi chạy Server Backend**:
   ```bash
   dotnet run --project src/Server/ElectronicsStore.Server.csproj
   ```

2. **Khởi chạy Client WinForms POS**:
   ```bash
   dotnet run --project src/Presentation/ElectronicsStore.Client.csproj
   ```

---

## 👤 Tác Giả & Bản Quyền

**Huỳnh Quốc Huy**
* **GitHub**: [hkhuang07](https://github.com/hkhuang07)
* **Repository**: [Electronic-Store-NET-Winform-Socket-MultilThreads](https://github.com/hkhuang07/Electronic-Store-NET-Winform-Socket-MultilThreads)

*Copyright © 2025 Huỳnh Quốc Huy. All Rights Reserved.*
