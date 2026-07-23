using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using System.Collections.Generic; // Dòng này bị trùng, có thể xóa

namespace ElectronicsStore.DataAccess
{
    public interface IOrderDetailsRepository
    {
        List<Order_Details> GetByOrderID(int orderId);
        void DeleteByOrderID(int orderId);
        void AddRange(List<Order_Details> details);
        void Insert(Order_Details detail);
        void Update(Order_Details detail); // Phương thức này đã có trong interface

        Order_Details? GetById(int id);   // Phương thức này đã có trong interface
        void Delete(Order_Details detail); // Phương thức này đã có trong interface
    }
}