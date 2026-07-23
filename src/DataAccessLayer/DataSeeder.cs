using System;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.DataAccess
{
    public static class DataSeeder
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories { ID = 1, CategoryName = "Linh kiện điện tử" },
                new Categories { ID = 2, CategoryName = "Điện thoại" },
                new Categories { ID = 3, CategoryName = "Laptop" },
                new Categories { ID = 4, CategoryName = "Máy tính" },
                new Categories { ID = 5, CategoryName = "IPAD" },
                new Categories { ID = 6, CategoryName = "Đồng hồ" },
                new Categories { ID = 7, CategoryName = "Phụ kiện" },
                new Categories { ID = 8, CategoryName = "Máy in" },
                new Categories { ID = 9, CategoryName = "Tivi" },
                new Categories { ID = 10, CategoryName = "Máy ảnh" }
            );

            modelBuilder.Entity<Manufacturers>().HasData(
                new Manufacturers { ID = 1, ManufacturerName = "Intel", ManufacturerAddress = "USA", ManufacturerPhone = "0123456789", ManufacturerEmail = "support@intel.com" },
                new Manufacturers { ID = 2, ManufacturerName = "AMD", ManufacturerAddress = "USA", ManufacturerPhone = "0123456790", ManufacturerEmail = "support@amd.com" },
                new Manufacturers { ID = 3, ManufacturerName = "NVIDIA", ManufacturerAddress = "USA", ManufacturerPhone = "0123456791", ManufacturerEmail = "contact@nvidia.com" },
                new Manufacturers { ID = 4, ManufacturerName = "ASUS", ManufacturerAddress = "Taiwan", ManufacturerPhone = "0123456792", ManufacturerEmail = "info@asus.com" },
                new Manufacturers { ID = 5, ManufacturerName = "Gigabyte", ManufacturerAddress = "Taiwan", ManufacturerPhone = "0123456793", ManufacturerEmail = "info@gigabyte.com" },
                new Manufacturers { ID = 6, ManufacturerName = "MSI", ManufacturerAddress = "Taiwan", ManufacturerPhone = "0123456794", ManufacturerEmail = "contact@msi.com" },
                new Manufacturers { ID = 7, ManufacturerName = "Apple", ManufacturerAddress = "USA", ManufacturerPhone = "0134567890", ManufacturerEmail = "support@apple.com" },
                new Manufacturers { ID = 8, ManufacturerName = "Samsung", ManufacturerAddress = "Korea", ManufacturerPhone = "0134567891", ManufacturerEmail = "contact@samsung.com" },
                new Manufacturers { ID = 9, ManufacturerName = "Xiaomi", ManufacturerAddress = "China", ManufacturerPhone = "0134567892", ManufacturerEmail = "info@xiaomi.com" },
                new Manufacturers { ID = 10, ManufacturerName = "Oppo", ManufacturerAddress = "China", ManufacturerPhone = "0134567893", ManufacturerEmail = "info@oppo.com" },
                new Manufacturers { ID = 11, ManufacturerName = "Realme", ManufacturerAddress = "China", ManufacturerPhone = "0134567894", ManufacturerEmail = "contact@realme.com" },
                new Manufacturers { ID = 12, ManufacturerName = "Vivo", ManufacturerAddress = "China", ManufacturerPhone = "0134567895", ManufacturerEmail = "info@vivo.com" },
                new Manufacturers { ID = 13, ManufacturerName = "Dell", ManufacturerAddress = "USA", ManufacturerPhone = "0144567890", ManufacturerEmail = "support@dell.com" },
                new Manufacturers { ID = 14, ManufacturerName = "HP", ManufacturerAddress = "USA", ManufacturerPhone = "0144567891", ManufacturerEmail = "support@hp.com" },
                new Manufacturers { ID = 15, ManufacturerName = "Lenovo", ManufacturerAddress = "China", ManufacturerPhone = "0144567892", ManufacturerEmail = "info@lenovo.com" },
                new Manufacturers { ID = 16, ManufacturerName = "Acer", ManufacturerAddress = "Taiwan", ManufacturerPhone = "0144567893", ManufacturerEmail = "info@acer.com" },
                new Manufacturers { ID = 17, ManufacturerName = "Asus Laptop", ManufacturerAddress = "Taiwan", ManufacturerPhone = "0144567894", ManufacturerEmail = "contact@asus.com" },
                new Manufacturers { ID = 18, ManufacturerName = "MSI Laptop", ManufacturerAddress = "Taiwan", ManufacturerPhone = "0144567895", ManufacturerEmail = "contact@msi.com" },
                new Manufacturers { ID = 19, ManufacturerName = "Sony", ManufacturerAddress = "Japan", ManufacturerPhone = "0204567890", ManufacturerEmail = "info@sony.com" },
                new Manufacturers { ID = 20, ManufacturerName = "LG", ManufacturerAddress = "Korea", ManufacturerPhone = "0204567892", ManufacturerEmail = "support@lg.com" },
                new Manufacturers { ID = 21, ManufacturerName = "Canon", ManufacturerAddress = "Japan", ManufacturerPhone = "0184567890", ManufacturerEmail = "info@canon.com" },
                new Manufacturers { ID = 22, ManufacturerName = "Logitech", ManufacturerAddress = "Switzerland", ManufacturerPhone = "0194567894", ManufacturerEmail = "support@logitech.com" },
                new Manufacturers { ID = 23, ManufacturerName = "Anker", ManufacturerAddress = "China", ManufacturerPhone = "0194567890", ManufacturerEmail = "support@anker.com" },
                new Manufacturers { ID = 24, ManufacturerName = "Epson", ManufacturerAddress = "Japan", ManufacturerPhone = "0184567892", ManufacturerEmail = "support@epson.com" }
            );

            modelBuilder.Entity<Employees>().HasData(
                new Employees
                {
                    ID = 1,
                    FullName = "Huỳnh Quốc Huy",
                    EmployeePhone = "0924202149",
                    EmployeeAddress = "Long Xuyên-An Giang",
                    UserName = "huynhquochuy",
                    Password = BCrypt.Net.BCrypt.HashPassword("0000000000"),
                    Role = false
                },
                new Employees
                {
                    ID = 2,
                    FullName = "Lâm Tư Thụy",
                    EmployeePhone = "0911122334",
                    EmployeeAddress = "Ji Jiang",
                    UserName = "linsirui",
                    Password = BCrypt.Net.BCrypt.HashPassword("1111111111"),
                    Role = true
                }
            );

            modelBuilder.Entity<Customers>().HasData(
                new Customers { ID = 1, CustomerName = "Hoàng Chiêu Ái Sa", CustomerAddress = "Hà Nội", CustomerPhone = "0911222333", CustomerEmail = "k@example.com" },
                new Customers { ID = 2, CustomerName = "Kim Thành Vũ", CustomerAddress = "Hồ Chí Minh", CustomerPhone = "0922333444", CustomerEmail = "l@example.com" },
                new Customers { ID = 3, CustomerName = "Quách Phú Thành", CustomerAddress = "Đà Nẵng", CustomerPhone = "0933444555", CustomerEmail = "m@example.com" },
                new Customers { ID = 4, CustomerName = "Đinh Thuyền", CustomerAddress = "Cần Thơ", CustomerPhone = "0944555666", CustomerEmail = "n@example.com" },
                new Customers { ID = 5, CustomerName = "Lâm Tịnh Khiết", CustomerAddress = "Hải Phòng", CustomerPhone = "0955666777", CustomerEmail = "o@example.com" },
                new Customers { ID = 6, CustomerName = "Vy Hải Xuân", CustomerAddress = "Quảng Ninh", CustomerPhone = "0966777888", CustomerEmail = "p@example.com" },
                new Customers { ID = 7, CustomerName = "Tống Kỳ", CustomerAddress = "Bình Dương", CustomerPhone = "0977888999", CustomerEmail = "q@example.com" },
                new Customers { ID = 8, CustomerName = "Lý Tường Hoa", CustomerAddress = "Lâm Đồng", CustomerPhone = "0988999000", CustomerEmail = "r@example.com" },
                new Customers { ID = 9, CustomerName = "Trương Tín Phàm", CustomerAddress = "Khánh Hòa", CustomerPhone = "0999000111", CustomerEmail = "s@example.com" },
                new Customers { ID = 10, CustomerName = "Triệu Thanh Phong", CustomerAddress = "Bắc Ninh", CustomerPhone = "0900111222", CustomerEmail = "t@example.com" }
            );

            modelBuilder.Entity<Products>().HasData(
                // Linh kiện điện tử (CategoryID = 1)
                new Products { ID = 1, ManufacturerID = 1, CategoryID = 1, ProductName = "Điện trở 1KΩ", Price = 500, Quantity = 200, Image = null, Description = "Điện trở 1KΩ loại thường" },
                new Products { ID = 2, ManufacturerID = 2, CategoryID = 1, ProductName = "Tụ điện 10uF", Price = 700, Quantity = 150, Image = null, Description = "Tụ điện phân cực 10 microfarad" },
                new Products { ID = 3, ManufacturerID = 3, CategoryID = 1, ProductName = "Triac BT136", Price = 2000, Quantity = 100, Image = null, Description = "Linh kiện bán dẫn điều khiển AC" },
                new Products { ID = 4, ManufacturerID = 4, CategoryID = 1, ProductName = "MOSFET IRF540N", Price = 3500, Quantity = 120, Image = null, Description = "MOSFET công suất cao" },
                new Products { ID = 5, ManufacturerID = 5, CategoryID = 1, ProductName = "Relay 5V 10A", Price = 2500, Quantity = 75, Image = null, Description = "Relay điều khiển tải" },
                new Products { ID = 6, ManufacturerID = 6, CategoryID = 1, ProductName = "Cảm biến nhiệt độ LM35", Price = 1500, Quantity = 90, Image = null, Description = "Cảm biến nhiệt tuyến tính" },

                // Điện thoại (CategoryID = 2)
                new Products { ID = 7, ManufacturerID = 7, CategoryID = 2, ProductName = "iPhone 13", Price = 19000000, Quantity = 30, Image = null, Description = "Smartphone Apple, chip A15, 128GB" },
                new Products { ID = 8, ManufacturerID = 8, CategoryID = 2, ProductName = "Samsung Galaxy S21", Price = 15000000, Quantity = 25, Image = null, Description = "Màn hình AMOLED 6.2\", RAM 8GB" },
                new Products { ID = 9, ManufacturerID = 9, CategoryID = 2, ProductName = "Xiaomi Redmi Note 11", Price = 5000000, Quantity = 40, Image = null, Description = "Pin 5000mAh, sạc nhanh 33W" },
                new Products { ID = 10, ManufacturerID = 10, CategoryID = 2, ProductName = "OPPO Reno8", Price = 8000000, Quantity = 35, Image = null, Description = "Mặt lưng kính, camera AI" },
                new Products { ID = 11, ManufacturerID = 11, CategoryID = 2, ProductName = "Vivo Y20", Price = 4000000, Quantity = 50, Image = null, Description = "Pin khủng, giá rẻ" },
                new Products { ID = 12, ManufacturerID = 12, CategoryID = 2, ProductName = "Realme C35", Price = 3500000, Quantity = 45, Image = null, Description = "Thiết kế trẻ trung, màn hình lớn" },
                new Products { ID = 13, ManufacturerID = 7, CategoryID = 2, ProductName = "iPhone 14 Pro", Price = 29000000, Quantity = 12, Image = null, Description = "Dynamic Island, 120Hz" },
                new Products { ID = 14, ManufacturerID = 8, CategoryID = 2, ProductName = "Samsung Galaxy S23", Price = 21000000, Quantity = 18, Image = null, Description = "Snapdragon 8 Gen 2 for Galaxy" },

                // Laptop (CategoryID = 3)
                new Products { ID = 15, ManufacturerID = 13, CategoryID = 3, ProductName = "Dell Inspiron 15", Price = 16000000, Quantity = 25, Image = null, Description = "Core i5, 8GB RAM, SSD 512GB" },
                new Products { ID = 16, ManufacturerID = 14, CategoryID = 3, ProductName = "HP Pavilion x360", Price = 17000000, Quantity = 20, Image = null, Description = "Màn cảm ứng, xoay gập 360 độ" },
                new Products { ID = 17, ManufacturerID = 15, CategoryID = 3, ProductName = "Lenovo Ideapad 3", Price = 14000000, Quantity = 30, Image = null, Description = "Mỏng nhẹ, chip Ryzen 5" },
                new Products { ID = 18, ManufacturerID = 16, CategoryID = 3, ProductName = "Asus Vivobook 15", Price = 15000000, Quantity = 28, Image = null, Description = "Màn hình 15.6 inch Full HD" },
                new Products { ID = 19, ManufacturerID = 13, CategoryID = 3, ProductName = "Dell XPS 13", Price = 28000000, Quantity = 10, Image = null, Description = "Màn 4K cảm ứng, vỏ nhôm" },

                // Máy tính (CategoryID = 4)
                new Products { ID = 20, ManufacturerID = 13, CategoryID = 4, ProductName = "Máy tính bàn Dell Optiplex 3080", Price = 10500000, Quantity = 10, Image = null, Description = "Core i5, RAM 8GB, SSD 256GB" },
                new Products { ID = 21, ManufacturerID = 14, CategoryID = 4, ProductName = "Máy tính HP ProDesk 400 G6", Price = 9900000, Quantity = 12, Image = null, Description = "i3-10100, RAM 8GB" },
                new Products { ID = 22, ManufacturerID = 15, CategoryID = 4, ProductName = "Máy tính ASUS ExpertCenter D500MA", Price = 8900000, Quantity = 8, Image = null, Description = "Intel Pentium, ổ SSD 256GB" },

                // IPAD (CategoryID = 5)
                new Products { ID = 23, ManufacturerID = 7, CategoryID = 5, ProductName = "iPad Gen 9 Wi-Fi 64GB", Price = 9000000, Quantity = 20, Image = null, Description = "Màn 10.2\", chip A13" },
                new Products { ID = 24, ManufacturerID = 7, CategoryID = 5, ProductName = "iPad Pro 11\" M2", Price = 23000000, Quantity = 8, Image = null, Description = "Màn 120Hz, Face ID" },

                // Phụ kiện, TV, Máy in, Máy ảnh (CategoryID = 6..10)
                new Products { ID = 25, ManufacturerID = 22, CategoryID = 7, ProductName = "Chuột Logitech M330", Price = 350000, Quantity = 25, Image = null, Description = "Chuột không dây, êm ái" },
                new Products { ID = 26, ManufacturerID = 21, CategoryID = 8, ProductName = "Canon LBP2900", Price = 3200000, Quantity = 10, Image = null, Description = "Máy in laser đen trắng" },
                new Products { ID = 27, ManufacturerID = 19, CategoryID = 9, ProductName = "Samsung Smart TV 43\" 4K", Price = 7800000, Quantity = 6, Image = null, Description = "Màn 4K, hệ điều hành Tizen" },
                new Products { ID = 28, ManufacturerID = 21, CategoryID = 10, ProductName = "Canon EOS M50 Mark II", Price = 16500000, Quantity = 5, Image = null, Description = "Máy ảnh không gương lật" }
            );

            modelBuilder.Entity<Orders>().HasData(
                new Orders { ID = 1, EmployeeID = 1, CustomerID = 1, Date = new DateTime(2025, 4, 1), Note = "Hóa đơn đầu tiên" },
                new Orders { ID = 2, EmployeeID = 2, CustomerID = 2, Date = new DateTime(2025, 4, 2), Note = "Khách hàng yêu cầu giao hàng gấp" },
                new Orders { ID = 3, EmployeeID = 1, CustomerID = 3, Date = new DateTime(2025, 4, 3), Note = "Sản phẩm theo yêu cầu đặc biệt" }
            );
        }
    }
}
