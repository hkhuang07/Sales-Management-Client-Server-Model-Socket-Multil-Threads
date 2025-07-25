using System;
using System.Collections.Generic;
// using System.ComponentModel; // Không cần thiết nếu không dùng
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Đảm bảo đã có using này

namespace ElectronicsStore.DataAccess
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ElectronicsStoreContext _context;

        public OrderDetailsRepository()
        {
            _context = new ElectronicsStoreContext();
        }

        // Tra cứu
        public List<Order_Details> GetByOrderID(int orderId)
        {
            return _context.Order_Details.Where(x => x.OrderID == orderId).ToList();
        }

        // Lấy chi tiết đơn hàng theo ID (Primary Key)
        // Đây là phương thức bạn cần thêm để khắc phục lỗi 'GetById(int)'
        public Order_Details? GetById(int id)
        {
            return _context.Order_Details.Find(id); // Sử dụng Find cho Primary Key
        }

        // Phương thức GetAll() không có trong interface, bạn có thể giữ lại hoặc xóa tùy mục đích sử dụng
        public List<Order_Details> GetAll()
        {
            return _context.Order_Details.ToList();
        }

        // Thêm mới
        public void Insert(Order_Details detail)
        {
            _context.Order_Details.Add(detail);
            _context.SaveChanges();
        }

        public void AddRange(List<Order_Details> details)
        {
            _context.Order_Details.AddRange(details);
            _context.SaveChanges();
        }

        // Cập nhật
        // Đây là phương thức bạn cần thêm để khắc phục lỗi 'Update(Order_Details)'
        public void Update(Order_Details detail)
        {
            var existingDetail = _context.Order_Details.Find(detail.ID); // Giả sử DetailID là Primary Key
            if (existingDetail != null)
            {
                _context.Entry(existingDetail).CurrentValues.SetValues(detail);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Order_Details with ID {detail.ID} not found for update.");
            }
        }

        // Xóa theo OrderID
        public void DeleteByOrderID(int orderId)
        {
            var oldDetails = _context.Order_Details
               .Where(x => x.OrderID == orderId)
               .ToList(); // Cần ToList() để thực hiện truy vấn trước khi RemoveRange

            if (oldDetails.Any())
            {
                _context.Order_Details.RemoveRange(oldDetails);
                _context.SaveChanges();
            }
        }

        // Xóa một chi tiết đơn hàng cụ thể
        // Đây là phương thức bạn cần thêm để khắc phục lỗi 'Delete(Order_Details)'
        public void Delete(Order_Details detail)
        {
            var existingDetail = _context.Order_Details.Find(detail.ID); // Giả sử DetailID là Primary Key
            if (existingDetail != null)
            {
                _context.Order_Details.Remove(existingDetail);
                _context.SaveChanges();
            }
            else
            {
                // Tùy chọn: ném ngoại lệ hoặc ghi log nếu đối tượng không tồn tại
                throw new InvalidOperationException($"Order_Details with ID {detail.ID} not found for deletion.");
            }
        }
    }
}