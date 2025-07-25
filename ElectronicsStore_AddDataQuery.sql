USE ElectronsStore
GO

INSERT INTO Category (CategoryName) VALUES
(N'Linh kiện điện tử'),
(N'Điện thoại'),
(N'Laptop'),
(N'Máy tính'),
(N'IPAD'),
(N'Đồng hồ'),
(N'Máy in'),
(N'Phụ kiện'),
(N'Tivi'),
(N'Máy ảnh');


INSERT INTO Manufacturer (ManufacturerName, ManufacturerAddress, ManufacturerPhone, ManufacturerEmail) VALUES
-- Linh kiện điện tử
('Intel', 'USA', '0123456789', 'support@intel.com'),
('AMD', 'USA', '0123456790', 'support@amd.com'),
('NVIDIA', 'USA', '0123456791', 'contact@nvidia.com'),
('ASUS', 'Taiwan', '0123456792', 'info@asus.com'),
('Gigabyte', 'Taiwan', '0123456793', 'info@gigabyte.com'),
('MSI', 'Taiwan', '0123456794', 'contact@msi.com'),
-- Điện thoại
('Apple', 'USA', '0134567890', 'support@apple.com'),
('Samsung', 'Korea', '0134567891', 'contact@samsung.com'),
('Xiaomi', 'China', '0134567892', 'info@xiaomi.com'),
('Oppo', 'China', '0134567893', 'info@oppo.com'),
('Realme', 'China', '0134567894', 'contact@realme.com'),
('Vivo', 'China', '0134567895', 'info@vivo.com'),
-- Laptop
('Dell', 'USA', '0144567890', 'support@dell.com'),
('HP', 'USA', '0144567891', 'support@hp.com'),
('Lenovo', 'China', '0144567892', 'info@lenovo.com'),
('Acer', 'Taiwan', '0144567893', 'info@acer.com'),
('Asus', 'Taiwan', '0144567894', 'contact@asus.com'),
('MSI', 'Taiwan', '0144567895', 'contact@msi.com'),
-- Máy tính
('Intel PC', 'USA', '0154567890', 'intelpc@intel.com'),
('HP Desktop', 'USA', '0154567891', 'desktop@hp.com'),
('Dell Workstation', 'USA', '0154567892', 'dell@work.com'),
('ASRock', 'Taiwan', '0154567893', 'info@asrock.com'),
('Lenovo PC', 'China', '0154567894', 'contact@lenovo.com'),
('Zotac', 'China', '0154567895', 'support@zotac.com'),
-- iPad
('Apple iPad', 'USA', '0164567890', 'ipad@apple.com'),
('iPad Pro', 'USA', '0164567891', 'pro@apple.com'),
('iPad Air', 'USA', '0164567892', 'air@apple.com'),
('iPad Mini', 'USA', '0164567893', 'mini@apple.com'),
('Apple Care', 'USA', '0164567894', 'care@apple.com'),
('iPad Gen', 'USA', '0164567895', 'gen@apple.com'),
-- Đồng hồ
('Casio', 'Japan', '0174567890', 'info@casio.com'),
('Citizen', 'Japan', '0174567891', 'support@citizen.com'),
('Seiko', 'Japan', '0174567892', 'info@seiko.com'),
('Apple Watch', 'USA', '0174567893', 'watch@apple.com'),
('Samsung Gear', 'Korea', '0174567894', 'gear@samsung.com'),
('Huawei Watch', 'China', '0174567895', 'watch@huawei.com'),
-- Máy in
('Canon', 'Japan', '0184567890', 'info@canon.com'),
('HP Printer', 'USA', '0184567891', 'printer@hp.com'),
('Epson', 'Japan', '0184567892', 'support@epson.com'),
('Brother', 'Japan', '0184567893', 'info@brother.com'),
('Xerox', 'USA', '0184567894', 'info@xerox.com'),
('Ricoh', 'Japan', '0184567895', 'support@ricoh.com'),
-- Phụ kiện
('Anker', 'China', '0194567890', 'support@anker.com'),
('Belkin', 'USA', '0194567891', 'info@belkin.com'),
('Ugreen', 'China', '0194567892', 'support@ugreen.com'),
('Baseus', 'China', '0194567893', 'info@baseus.com'),
('Logitech', 'Switzerland', '0194567894', 'support@logitech.com'),
('Rapoo', 'China', '0194567895', 'info@rapoo.com'),
-- Tivi
('Sony', 'Japan', '0204567890', 'info@sony.com'),
('Samsung', 'Korea', '0204567891', 'tv@samsung.com'),
('LG', 'Korea', '0204567892', 'support@lg.com'),
('TCL', 'China', '0204567893', 'info@tcl.com'),
('Panasonic', 'Japan', '0204567894', 'support@panasonic.com'),
('Sharp', 'Japan', '0204567895', 'info@sharp.com'),
-- Máy ảnh
('Canon', 'Japan', '0214567890', 'photo@canon.com'),
('Nikon', 'Japan', '0214567891', 'support@nikon.com'),
('Sony', 'Japan', '0214567892', 'photo@sony.com'),
('Fujifilm', 'Japan', '0214567893', 'info@fujifilm.com'),
('Olympus', 'Japan', '0214567894', 'support@olympus.com'),
('Leica', 'Germany', '0214567895', 'info@leica.com');

INSERT INTO Customer (CustomerName, CustomerAddress, CustomerPhone, CustomerEmail) VALUES
(N'Hoàng Chiêu Ái Sa', N'Hà Nội', '0911222333', 'k@example.com'),
(N'Kim Thành Vũ', N'Hồ Chí Minh', '0922333444', 'l@example.com'),
(N'Quách Phú Thành', N'Đà Nẵng', '0933444555', 'm@example.com'),
(N'Đinh Thuyền', N'Cần Thơ', '0944555666', 'n@example.com'),
(N'Lâm Tịnh Khiết', N'Hải Phòng', '0955666777', 'o@example.com'),
(N'Vy Hải Xuân', N'Quảng Ninh', '0966777888', 'p@example.com'),
(N'Tống Kỳ', N'Bình Dương', '0977888999', 'q@example.com'),
(N'Lý Tường Hoa', N'Lâm Đồng', '0988999000', 'r@example.com'),
(N'Trương Tín Phàm', N'Khánh Hòa', '0999000111', 's@example.com'),
(N'Triệu Thanh Phong', N'Bắc Ninh', '0900111222', 't@example.com');

INSERT INTO Employee (FullName, EmployeePhone, EmployeeAddress, UserName, Password, Role) VALUES
(N'Huỳnh Quốc Huy', '0924202149', N'Long Xuyên-An Giang', 'huynhquochuy', '0000000000', 0),
(N'Lâm Tư Thụy', '0911122334', N'Ji Jiang', 'linsirui', '1111111111', 1);

--Bảng Product 
--Linh kiện điện tử
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(1, 1, N'Điện trở 1KΩ', 500, 200, NULL, N'Điện trở 1KΩ loại thường'),
(2, 1, N'Tụ điện 10uF', 700, 150, NULL, N'Tụ điện phân cực 10 microfarad'),
(3, 1, N'Triac BT136', 2000, 100, NULL, N'Linh kiện bán dẫn điều khiển AC'),
(4, 1, N'MOSFET IRF540N', 3500, 120, NULL, N'MOSFET công suất cao'),
(5, 1, N'Relay 5V 10A', 2500, 75, NULL, N'Relay điều khiển tải'),
(6, 1, N'Cảm biến nhiệt độ LM35', 1500, 90, NULL, N'Cảm biến nhiệt tuyến tính'),

(1, 1, N'Mạch nguồn 5V', 10000, 60, NULL, N'Mạch hạ áp AC-DC'),
(2, 1, N'Mạch thu phát RF 433MHz', 12000, 50, NULL, N'Mô-đun truyền dữ liệu không dây'),
(3, 1, N'LED RGB 5mm', 1000, 300, NULL, N'LED 3 màu tích hợp'),
(4, 1, N'Mạch sạc pin 18650', 5000, 100, NULL, N'Mạch sạc lithium tích hợp bảo vệ'),
(5, 1, N'IC NE555', 800, 150, NULL, N'IC định thời kinh điển'),
(6, 1, N'LCD 16x2 I2C', 25000, 60, NULL, N'Màn hình LCD giao tiếp I2C'),

(1, 1, N'Mạch Arduino Nano', 70000, 40, NULL, N'Board Arduino mini dạng Nano'),
(2, 1, N'Mạch ESP8266 NodeMCU', 85000, 30, NULL, N'Board điều khiển WiFi'),
(3, 1, N'Bluetooth HC-05', 60000, 45, NULL, N'Mô-đun giao tiếp Bluetooth'),
(4, 1, N'Động cơ servo SG90', 30000, 35, NULL, N'Servo nhỏ gọn cho robot'),
(5, 1, N'Cảm biến khoảng cách HC-SR04', 22000, 50, NULL, N'Ultrasound sensor đo khoảng cách'),
(6, 1, N'Cảm biến gia tốc MPU6050', 40000, 20, NULL, N'Gia tốc kế và con quay hồi chuyển'),

(1, 1, N'Mạch điều tốc ESC 30A', 50000, 25, NULL, N'Mạch điều khiển động cơ brushless'),
(2, 1, N'Quạt tản nhiệt 5V', 10000, 80, NULL, N'Quạt mini dùng cho Raspberry Pi'),
(3, 1, N'Đế Arduino Uno', 25000, 60, NULL, N'Đế nhựa giữ Arduino'),
(4, 1, N'Pin Li-ion 18650 3.7V', 15000, 100, NULL, N'Pin sạc lithium phổ biến'),
(5, 1, N'Biến trở 10K', 1200, 200, NULL, N'Chiết áp xoay dùng cho điều chỉnh tín hiệu'),
(6, 1, N'Breadboard 830 lỗ', 30000, 50, NULL, N'Board cắm mạch tạm thời'),

-- 30 sản phẩm tiếp theo (các tên và mô tả khác nhau)
(1, 1, N'Điốt 1N4007', 400, 500, NULL, N'Điốt chỉnh lưu'),
(2, 1, N'Ống co nhiệt 3mm', 200, 400, NULL, N'Ống cách điện thu nhỏ khi nhiệt độ cao'),
(3, 1, N'Đèn LED đỏ 5mm', 300, 600, NULL, N'LED dùng hiển thị trạng thái'),
(4, 1, N'Pin 9V Alkaline', 10000, 70, NULL, N'Pin khô dùng cho cảm biến'),
(5, 1, N'Chân cắm Dupont đực', 500, 200, NULL, N'Dây nối linh kiện'),
(6, 1, N'Đồng hồ vạn năng DT830D', 90000, 15, NULL, N'Multimeter điện tử'),

(1, 1, N'Raspberry Pi Pico', 120000, 20, NULL, N'Microcontroller mới từ Raspberry'),
(2, 1, N'Camera OV7670', 60000, 25, NULL, N'Module chụp ảnh cho vi điều khiển'),
(3, 1, N'Mạch đọc thẻ RFID RC522', 40000, 40, NULL, N'Dùng để đọc thẻ RFID 13.56MHz'),
(4, 1, N'Buzzer 5V', 2000, 100, NULL, N'Chuông báo dùng cho mạch điều khiển'),
(5, 1, N'Mạch relay 2 kênh', 18000, 35, NULL, N'Relay điều khiển 2 thiết bị'),
(6, 1, N'Pin CR2032', 3000, 200, NULL, N'Pin cúc áo 3V'),

(1, 1, N'Adapter 12V 2A', 50000, 30, NULL, N'Nguồn cung cấp cho mạch'),
(2, 1, N'Cáp USB to TTL', 25000, 25, NULL, N'Dây nạp cho vi điều khiển'),
(3, 1, N'Ốc đồng M3', 100, 300, NULL, N'Ốc lắp ráp module'),
(4, 1, N'Nguồn chuyển mạch 12V 5A', 80000, 10, NULL, N'SMPS dùng cho camera'),
(5, 1, N'Mạch cảm biến ánh sáng', 12000, 50, NULL, N'Light sensor module'),
(6, 1, N'Mạch điều chỉnh tốc độ motor', 20000, 40, NULL, N'Driver động cơ DC');

--Điện thoại
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(7, 2, N'iPhone 13', 19000000, 30, NULL, N'Smartphone Apple, chip A15, 128GB'),
(8, 2, N'Samsung Galaxy S21', 15000000, 25, NULL, N'Màn hình AMOLED 6.2", RAM 8GB'),
(9, 2, N'Xiaomi Redmi Note 11', 5000000, 40, NULL, N'Pin 5000mAh, sạc nhanh 33W'),
(10, 2, N'OPPO Reno8', 8000000, 35, NULL, N'Mặt lưng kính, camera AI'),
(11, 2, N'Vivo Y20', 4000000, 50, NULL, N'Pin khủng, giá rẻ'),
(12, 2, N'Realme C35', 3500000, 45, NULL, N'Thiết kế trẻ trung, màn hình lớn'),

(7, 2, N'iPhone 13 Pro', 24000000, 20, NULL, N'Màn 120Hz, camera tele 3x'),
(8, 2, N'Samsung Galaxy A73', 9500000, 25, NULL, N'Camera 108MP, Snapdragon 778G'),
(9, 2, N'Xiaomi Poco X5', 7000000, 30, NULL, N'Màn AMOLED 120Hz, pin 5000mAh'),
(10, 2, N'OPPO A96', 5500000, 35, NULL, N'RAM 8GB, pin 5000mAh'),
(11, 2, N'Vivo V25', 8500000, 20, NULL, N'Chụp ảnh đẹp, chống rung OIS'),
(12, 2, N'Realme Narzo 50A', 3800000, 50, NULL, N'Pin trâu, màn lớn'),

(7, 2, N'iPhone SE 2022', 11500000, 15, NULL, N'Chip A15, thiết kế cổ điển'),
(8, 2, N'Samsung Galaxy Z Flip3', 23000000, 10, NULL, N'Màn gập độc đáo'),
(9, 2, N'Xiaomi Mi 11 Lite', 6700000, 18, NULL, N'Thiết kế mỏng nhẹ, 90Hz'),
(10, 2, N'OPPO Find X3 Pro', 20000000, 12, NULL, N'Flagship OPPO 2021'),
(11, 2, N'Vivo X80 Pro', 21000000, 10, NULL, N'Camera ZEISS, Snapdragon 8 Gen 1'),
(12, 2, N'Realme GT Neo 3', 9500000, 25, NULL, N'Sạc nhanh 80W, hiệu năng cao'),

(7, 2, N'iPhone 14', 23000000, 22, NULL, N'Chip A15 Bionic, camera mới'),
(8, 2, N'Samsung Galaxy S22', 18000000, 18, NULL, N'Thiết kế cao cấp, Exynos 2200'),
(9, 2, N'Xiaomi 12T', 10500000, 30, NULL, N'Camera 108MP, sạc nhanh 120W'),
(10, 2, N'OPPO A57', 3800000, 40, NULL, N'Pin 5000mAh, loa stereo'),
(11, 2, N'Vivo Y22s', 4200000, 35, NULL, N'RAM 8GB, Snapdragon 680'),
(12, 2, N'Realme 9 Pro+', 7900000, 25, NULL, N'Camera chống rung OIS'),

(7, 2, N'iPhone 14 Pro', 29000000, 12, NULL, N'Dynamic Island, 120Hz'),
(8, 2, N'Samsung Galaxy M54', 8300000, 28, NULL, N'Pin 6000mAh, màn Super AMOLED'),
(9, 2, N'Xiaomi Redmi 12C', 3000000, 60, NULL, N'Giá rẻ, hiệu năng ổn'),
(10, 2, N'OPPO Reno10 5G', 10000000, 15, NULL, N'Sạc nhanh SUPERVOOC'),
(11, 2, N'Vivo V29e', 8900000, 20, NULL, N'Thiết kế cong, selfie tốt'),
(12, 2, N'Realme GT5', 13500000, 18, NULL, N'Snapdragon 8 Gen 2'),

(7, 2, N'iPhone 14 Plus', 25000000, 10, NULL, N'Màn hình lớn 6.7 inch'),
(8, 2, N'Samsung Galaxy Z Fold4', 36000000, 5, NULL, N'Điện thoại màn gập cao cấp'),
(9, 2, N'Xiaomi Black Shark 5', 12000000, 10, NULL, N'Gaming phone mạnh mẽ'),
(10, 2, N'OPPO Find N2 Flip', 24000000, 8, NULL, N'Màn hình gập dọc, camera tốt'),
(11, 2, N'Vivo T1 5G', 6000000, 22, NULL, N'Hỗ trợ 5G, hiệu năng ổn'),
(12, 2, N'Realme C55', 4200000, 30, NULL, N'Camera 64MP, sạc 33W'),

(7, 2, N'iPhone 15', 26000000, 15, NULL, N'Mẫu mới 2023, cổng USB-C'),
(8, 2, N'Samsung Galaxy S23', 21000000, 18, NULL, N'Snapdragon 8 Gen 2 for Galaxy'),
(9, 2, N'Xiaomi Redmi Note 12 Pro', 7500000, 30, NULL, N'Màn AMOLED 120Hz'),
(10, 2, N'OPPO A78', 5500000, 40, NULL, N'Pin 5000mAh, sạc 67W'),
(11, 2, N'Vivo Y17s', 3900000, 38, NULL, N'Camera kép, pin bền'),
(12, 2, N'Realme Narzo N55', 4000000, 35, NULL, N'Hiệu năng tốt, sạc nhanh');

--Laptop
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(13, 3, N'Dell Inspiron 15', 16000000, 25, NULL, N'Core i5, 8GB RAM, SSD 512GB'),
(14, 3, N'HP Pavilion x360', 17000000, 20, NULL, N'Màn cảm ứng, xoay gập 360 độ'),
(15, 3, N'Lenovo Ideapad 3', 14000000, 30, NULL, N'Mỏng nhẹ, chip Ryzen 5'),
(16, 3, N'Asus Vivobook 15', 15000000, 28, NULL, N'Màn hình 15.6 inch Full HD'),
(17, 3, N'MSI Modern 14', 18000000, 15, NULL, N'Thiết kế mỏng, GPU Intel Iris'),
(18, 3, N'Acer Aspire 7', 19000000, 18, NULL, N'GPU GTX 1650, cho game thủ'),

(13, 3, N'Dell XPS 13', 28000000, 10, NULL, N'Màn 4K cảm ứng, vỏ nhôm'),
(14, 3, N'HP Envy 13', 23000000, 12, NULL, N'Core i7, màn mỏng đẹp'),
(15, 3, N'Lenovo ThinkPad E14', 21000000, 14, NULL, N'Bền bỉ, phù hợp doanh nghiệp'),
(16, 3, N'Asus Zenbook 14 OLED', 26000000, 8, NULL, N'Màn OLED sắc nét, pin lâu'),
(17, 3, N'MSI GF63 Thin', 24000000, 10, NULL, N'Card RTX 3050, chơi game tốt'),
(18, 3, N'Acer Swift 3', 17000000, 18, NULL, N'Mỏng nhẹ, thời lượng pin tốt'),

(13, 3, N'Dell Latitude 5420', 22000000, 11, NULL, N'Doanh nghiệp, bảo mật tốt'),
(14, 3, N'HP ProBook 450 G9', 21000000, 13, NULL, N'Core i5 Gen 12, RAM 16GB'),
(15, 3, N'Lenovo Yoga Slim 7', 25000000, 9, NULL, N'AMD Ryzen 7, 1TB SSD'),
(16, 3, N'Asus ROG Strix G15', 32000000, 6, NULL, N'Laptop gaming mạnh mẽ'),
(17, 3, N'MSI Katana GF66', 30000000, 7, NULL, N'RTX 3060, Intel Core i7'),
(18, 3, N'Acer Nitro 5', 28000000, 8, NULL, N'Phù hợp game thủ tầm trung'),

(13, 3, N'Dell Vostro 3510', 15000000, 20, NULL, N'Làm việc văn phòng'),
(14, 3, N'HP EliteBook 840 G8', 27000000, 9, NULL, N'Thiết kế sang trọng, bảo mật'),
(15, 3, N'Lenovo Legion 5', 33000000, 6, NULL, N'RTX 3060, RAM 16GB'),
(16, 3, N'Asus TUF Gaming F15', 29000000, 10, NULL, N'Chơi game tầm trung tốt'),
(17, 3, N'MSI Stealth 15M', 34000000, 5, NULL, N'Mỏng nhẹ, hiệu năng cao'),
(18, 3, N'Acer Chromebook 514', 12000000, 22, NULL, N'Chạy ChromeOS, nhẹ nhàng'),

(13, 3, N'Dell G15 5511', 26000000, 10, NULL, N'Gaming, tản nhiệt tốt'),
(14, 3, N'HP Spectre x360', 35000000, 4, NULL, N'Màn cảm ứng 2K, pin lâu'),
(15, 3, N'Lenovo Flex 5', 18500000, 12, NULL, N'Xoay gập linh hoạt, cảm ứng'),
(16, 3, N'Asus ExpertBook B5', 23000000, 9, NULL, N'Doanh nhân, pin bền'),
(17, 3, N'MSI Creator Z16', 40000000, 3, NULL, N'Đồ họa, dựng phim chuyên nghiệp'),
(18, 3, N'Acer Spin 5', 21000000, 7, NULL, N'2-trong-1, hỗ trợ bút cảm ứng'),

(13, 3, N'Dell Alienware x14', 46000000, 2, NULL, N'Laptop gaming cao cấp'),
(14, 3, N'HP ZBook Firefly 15 G8', 39000000, 4, NULL, N'Máy trạm di động'),
(15, 3, N'Lenovo Yoga 9i', 36000000, 3, NULL, N'Màn OLED, xoay gập'),
(16, 3, N'Asus ROG Zephyrus G14', 37000000, 4, NULL, N'AMD Ryzen 9, RTX 3060'),
(17, 3, N'MSI Summit E14', 33000000, 5, NULL, N'Dành cho doanh nhân'),
(18, 3, N'Acer TravelMate P2', 16000000, 15, NULL, N'Bền, pin ổn định');

-- Máy tính (CategoryID = 4)
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(13, 4, N'Máy tính bàn Dell Optiplex 3080', 10500000, 10, NULL, N'Core i5, RAM 8GB, SSD 256GB'),
(14, 4, N'Máy tính HP ProDesk 400 G6', 9900000, 12, NULL, N'i3-10100, RAM 8GB'),
(15, 4, N'Máy tính ASUS ExpertCenter D500MA', 8900000, 8, NULL, N'Intel Pentium, ổ SSD 256GB'),
(16, 4, N'Máy tính Acer Veriton S', 9600000, 10, NULL, N'RAM 8GB, bộ nhớ SSD'),
(17, 4, N'Máy tính bàn Lenovo V50t', 11200000, 7, NULL, N'i5 thế hệ 10, vỏ nhỏ gọn'),
(18, 4, N'Máy tính mini Intel NUC 11', 11800000, 6, NULL, N'Máy tính nhỏ, hiệu năng cao'),
(13, 4, N'Máy tính HP EliteDesk 800', 12500000, 9, NULL, N'Desktop dùng trong văn phòng'),
(14, 4, N'Máy tính Dell Vostro 3681', 11000000, 10, NULL, N'Máy tính văn phòng nhỏ gọn'),
(15, 4, N'Máy tính Gigabyte BRIX S', 11500000, 5, NULL, N'Máy tính mini, tiết kiệm điện'),
(16, 4, N'Máy tính bàn Asus PN41', 10400000, 7, NULL, N'Máy tính mini, Core i3 thế hệ 11'),
(17, 4, N'Máy tính Lenovo IdeaCentre 3', 9200000, 8, NULL, N'AMD Ryzen 3, 8GB RAM'),
(18, 4, N'Máy tính All-in-One HP 22-df', 13500000, 6, NULL, N'Màn hình 21.5", tích hợp CPU'),
(13, 4, N'Máy tính Dell Inspiron 3880', 9800000, 7, NULL, N'Văn phòng cơ bản'),
(14, 4, N'Máy tính để bàn MSI Cubi N', 8300000, 6, NULL, N'Celeron N4500, máy nhỏ gọn'),
(15, 4, N'Máy tính văn phòng ASUS S500', 8900000, 10, NULL, N'Chip Intel Core i3'),
(16, 4, N'Máy tính HP Pavilion TP01', 12100000, 5, NULL, N'Máy tính giải trí, đồ họa nhẹ'),
(17, 4, N'Máy tính để bàn Acer Aspire TC', 10700000, 9, NULL, N'i5-12400, 8GB RAM'),
(18, 4, N'Máy tính mini Beelink SEi10', 9500000, 8, NULL, N'Máy nhỏ cho lập trình viên'),
(13, 4, N'Máy tính Dell Optiplex 5090', 13600000, 6, NULL, N'Máy doanh nghiệp cao cấp'),
(14, 4, N'Máy tính Lenovo ThinkCentre M70t', 11900000, 7, NULL, N'Bảo mật tốt, nhỏ gọn'),
(15, 4, N'Máy tính HP Desktop M01', 8700000, 11, NULL, N'Chip AMD Ryzen 3'),
(16, 4, N'Máy tính Intel NUC M15', 13900000, 4, NULL, N'Ultrasmall PC mạnh mẽ'),
(17, 4, N'Máy tính ASUS PN62', 12500000, 6, NULL, N'Mini PC dùng học online'),
(18, 4, N'Máy tính MSI Pro DP21', 11100000, 7, NULL, N'Văn phòng, hiệu năng ổn định'),
(13, 4, N'Máy tính HP All-in-One 24', 15200000, 5, NULL, N'Màn hình 24", thiết kế đẹp'),
(14, 4, N'Máy tính Dell XPS Desktop', 17900000, 3, NULL, N'Máy mạnh cho thiết kế'),
(15, 4, N'Máy tính Gigabyte AERO Mini', 14500000, 4, NULL, N'Dành cho đồ họa nhẹ'),
(16, 4, N'Máy tính Acer Aspire Z24', 13200000, 5, NULL, N'All-in-one, tiết kiệm diện tích'),
(17, 4, N'Máy tính ASUS Vivo AiO V222', 12900000, 6, NULL, N'All-in-One, dùng học tập'),
(18, 4, N'Máy tính Lenovo All-in-One 3', 12300000, 5, NULL, N'Màn 21.5", tiết kiệm điện'),
(13, 4, N'Máy tính HP Slim Desktop S01', 9300000, 8, NULL, N'Máy tính học sinh-sinh viên'),
(14, 4, N'Máy tính bàn Acer Veriton N', 10900000, 6, NULL, N'Kích thước nhỏ gọn, hiệu quả'),
(15, 4, N'Máy tính ASUS Mini PC PB60', 12000000, 5, NULL, N'Intel i5, hỗ trợ 4K'),
(16, 4, N'Máy tính Lenovo ThinkCentre M75s', 11400000, 7, NULL, N'AMD Ryzen Pro, doanh nghiệp'),
(17, 4, N'Máy tính HP ProDesk 600 G6', 13800000, 4, NULL, N'Hiệu suất cao, bảo mật tốt'),
(18, 4, N'Máy tính MSI Cubi 5', 9800000, 6, NULL, N'Mini PC tiết kiệm điện'),
(13, 4, N'Máy tính ASUS S500SC', 9500000, 7, NULL, N'Máy cho văn phòng nhỏ'),
(14, 4, N'Máy tính All-in-One Acer C24', 14200000, 4, NULL, N'PC học online cho trẻ em'),
(15, 4, N'Máy tính Lenovo IdeaCentre AIO', 12500000, 5, NULL, N'All-in-One tích hợp webcam'),
(16, 4, N'Máy tính HP ENVY Desktop', 15500000, 3, NULL, N'Phù hợp chỉnh sửa video'),
(17, 4, N'Máy tính Dell Precision 3650', 17800000, 2, NULL, N'Dành cho thiết kế chuyên nghiệp'),
(18, 4, N'Máy tính HP Elite Slice', 13200000, 5, NULL, N'Dạng khối, độc đáo'),
(13, 4, N'Máy tính Acer Aspire XC', 9200000, 9, NULL, N'Máy tính nhỏ, học sinh dùng'),
(14, 4, N'Máy tính Lenovo ThinkStation P340', 16700000, 2, NULL, N'Workstation mini mạnh'),
(15, 4, N'Máy tính ASUS Mini PC PN62S', 11300000, 6, NULL, N'Dùng được cho camera AI'),
(16, 4, N'Máy tính HP Z2 G5 Tower', 18500000, 3, NULL, N'Dành cho dựng phim'),
(17, 4, N'Máy tính Dell Vostro 3910', 11900000, 6, NULL, N'Phù hợp doanh nghiệp nhỏ'),
(18, 4, N'Máy tính Lenovo Legion T5', 17500000, 4, NULL, N'Máy tính gaming mạnh mẽ');


-- iPad (CategoryID = 5)
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(19, 5, N'iPad Gen 9 Wi-Fi 64GB', 9000000, 20, NULL, N'Màn 10.2", chip A13'),
(20, 5, N'iPad Gen 10 Wi-Fi 64GB', 10500000, 15, NULL, N'Chip A14, hỗ trợ Apple Pencil'),
(21, 5, N'iPad Air M1 64GB', 15500000, 10, NULL, N'M1, USB-C, hỗ trợ Magic Keyboard'),
(22, 5, N'iPad Pro 11" M2', 23000000, 8, NULL, N'Màn 120Hz, Face ID'),
(23, 5, N'iPad Mini 6 64GB', 13500000, 12, NULL, N'Nhỏ gọn, cổng USB-C'),
(24, 5, N'iPad Pro 12.9" M2', 30000000, 6, NULL, N'Màn XDR, chip M2 mạnh mẽ'),
(19, 5, N'iPad Gen 10 LTE', 12500000, 10, NULL, N'4G, tiện dụng cho công việc'),
(20, 5, N'iPad Air M1 LTE', 17500000, 8, NULL, N'Hỗ trợ 4G, mỏng nhẹ'),
(21, 5, N'iPad Mini 6 LTE', 15500000, 9, NULL, N'Nhỏ gọn, tiện di chuyển'),
(22, 5, N'iPad Pro 11" 1TB', 40000000, 4, NULL, N'Lưu trữ lớn, hiệu năng cao');

-- Đồng hồ (CategoryID = 6)
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(19, 6, N'Apple Watch Series 9', 12000000, 10, NULL, N'Màn Always On, chip S9'),
(20, 6, N'Samsung Galaxy Watch 5', 7500000, 12, NULL, N'Tích hợp đo SpO2'),
(21, 6, N'Garmin Forerunner 255', 8500000, 8, NULL, N'Đo nhịp tim, GPS'),
(22, 6, N'Xiaomi Watch S1', 5200000, 14, NULL, N'Màn AMOLED, pin lâu'),
(23, 6, N'Huawei Watch GT3', 7900000, 9, NULL, N'Theo dõi sức khỏe toàn diện'),
(24, 6, N'OPPO Watch Free', 1500000, 20, NULL, N'Giá rẻ, pin bền'),
(19, 6, N'Apple Watch SE', 8500000, 11, NULL, N'Giá tốt, đầy đủ tính năng'),
(20, 6, N'Amazfit GTR 3', 3800000, 15, NULL, N'Màn tròn, pin 14 ngày'),
(21, 6, N'Garmin Venu 2', 11000000, 6, NULL, N'Thể thao cao cấp'),
(22, 6, N'Samsung Galaxy Watch 4', 7000000, 13, NULL, N'WearOS, kết nối Android');

-- Phụ kiện (CategoryID = 7)
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(19, 7, N'Chuột Logitech M330', 350000, 25, NULL, N'Chuột không dây, êm ái'),
(20, 7, N'Bàn phím cơ Fuhlen', 790000, 18, NULL, N'Đèn LED, switch Blue'),
(21, 7, N'Tai nghe Sony WH-1000XM5', 8000000, 8, NULL, N'Chống ồn chủ động'),
(22, 7, N'Cáp Lightning Anker', 350000, 30, NULL, N'Dài 1.2m, siêu bền'),
(23, 7, N'Sạc dự phòng Xiaomi 20000mAh', 500000, 20, NULL, N'2 cổng sạc nhanh'),
(24, 7, N'USB Kingston 64GB', 180000, 50, NULL, N'USB 3.2 tốc độ cao'),
(19, 7, N'Giá đỡ điện thoại', 100000, 40, NULL, N'Gập gọn, tiện lợi'),
(20, 7, N'Miếng dán màn hình iPhone', 150000, 35, NULL, N'Chống trầy, dễ dán'),
(21, 7, N'Tai nghe Bluetooth Baseus', 900000, 22, NULL, N'TWS, chống nước IPX5'),
(22, 7, N'Sạc nhanh Samsung 25W', 450000, 16, NULL, N'Chuẩn PD USB-C');

-- Máy in (CategoryID = 8)
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(19, 8, N'Canon LBP2900', 3200000, 10, NULL, N'Máy in laser đen trắng'),
(20, 8, N'Epson L3150', 4800000, 8, NULL, N'Máy in phun màu, wifi'),
(21, 8, N'HP LaserJet Pro M404dn', 5800000, 5, NULL, N'Tốc độ cao, in 2 mặt'),
(22, 8, N'Brother HL-L2321D', 3600000, 7, NULL, N'In laser, tự động 2 mặt'),
(23, 8, N'Canon G3010', 5100000, 6, NULL, N'Máy in đa chức năng'),
(24, 8, N'HP DeskJet Ink 2336', 1500000, 12, NULL, N'Giá rẻ, in màu cơ bản'),
(19, 8, N'Epson L4160', 5900000, 4, NULL, N'In Wifi, scan copy'),
(20, 8, N'Brother DCP-T720DW', 5400000, 5, NULL, N'Máy in màu đa năng'),
(21, 8, N'Canon Pixma E3470', 2500000, 9, NULL, N'Kết nối wifi, in màu'),
(22, 8, N'HP Ink Tank 415', 4400000, 6, NULL, N'In không dây, tiết kiệm mực');

-- TV (CategoryID = 9)
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(19, 9, N'Samsung Smart TV 43" 4K', 7800000, 6, NULL, N'Màn 4K, hệ điều hành Tizen'),
(20, 9, N'LG Smart TV 50"', 9900000, 5, NULL, N'Màn lớn, âm thanh sống động'),
(21, 9, N'Sony Bravia 55" OLED', 23500000, 3, NULL, N'Màn OLED cực đẹp'),
(22, 9, N'Casper TV 43" Full HD', 5500000, 7, NULL, N'Giá rẻ, hình ảnh rõ'),
(23, 9, N'TCL TV 4K 55"', 8900000, 6, NULL, N'4K, hỗ trợ Netflix'),
(24, 9, N'Xiaomi Mi TV P1 43"', 6900000, 8, NULL, N'Android TV, giọng nói'),
(19, 9, N'Samsung Crystal UHD 55"', 10200000, 5, NULL, N'Thích hợp cho gia đình'),
(20, 9, N'LG OLED Evo 65"', 29000000, 2, NULL, N'Màn cực mỏng, đẹp'),
(21, 9, N'Coocaa TV 40"', 4800000, 9, NULL, N'Giá mềm, tivi thông minh'),
(22, 9, N'Sharp 32" HD', 3200000, 10, NULL, N'Phù hợp phòng nhỏ');

-- Máy ảnh (CategoryID = 10)
INSERT INTO Product (ManufacturerID, CategoryID, ProductName, Price, Quantity, Image, Description) VALUES
(19, 10, N'Canon EOS M50 Mark II', 16500000, 5, NULL, N'Máy ảnh không gương lật'),
(20, 10, N'Sony ZV-E10', 18000000, 4, NULL, N'Dành cho vlogger'),
(21, 10, N'Nikon Z30', 17500000, 3, NULL, N'Quay video 4K, cảm biến APS-C'),
(22, 10, N'Fujifilm X-T200', 16000000, 6, NULL, N'Máy ảnh retro đẹp mắt'),
(23, 10, N'Canon EOS 1500D', 12500000, 7, NULL, N'DSLR phổ thông'),
(24, 10, N'Sony Alpha A6400', 21000000, 3, NULL, N'Lấy nét nhanh, quay 4K'),
(19, 10, N'Panasonic Lumix G100', 15000000, 4, NULL, N'Gọn nhẹ, livestream tốt'),
(20, 10, N'Canon EOS R10', 23000000, 2, NULL, N'Máy ảnh không gương lật mới'),
(21, 10, N'Nikon D3500', 11900000, 6, NULL, N'DSLR học sinh-sinh viên'),
(22, 10, N'Fujifilm X-A7', 14500000, 5, NULL, N'Cảm biến lớn, màn xoay cảm ứng');

INSERT INTO [Order] (EmployeeID, CustomerID, [Date], Note) VALUES
(1, 1, '2025-04-01', N'Hóa đơn đầu tiên'),
(2, 2, '2025-04-02', N'Khách hàng yêu cầu giao hàng gấp'),
(1, 3, '2025-04-03', N'Sản phẩm theo yêu cầu đặc biệt'),
(1, 4, '2025-04-04', N'Giảm giá đặc biệt cho khách hàng VIP'),
(2, 5, '2025-04-05', N'Mua số lượng lớn'),
(2, 6, '2025-04-06', N'Khách hàng chọn thanh toán qua chuyển khoản'),
(1, 7, '2025-04-07', N'Đặt mua phụ kiện điện thoại'),
(2, 8, '2025-04-08', N'Mua một sản phẩm theo catalog'),
(2, 9, '2025-04-09', N'Yêu cầu đổi trả sau khi nhận hàng'),
(1, 10, '2025-04-10', N'Khách hàng muốn thanh toán ngay');
SELECT * FROM [Order]
SELECT * FROM Order_Details


