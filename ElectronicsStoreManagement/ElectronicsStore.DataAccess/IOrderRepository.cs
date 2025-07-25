using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ElectronicsStore.DataAccess
{
    public interface IOrderRepository
    {
        List<Orders> GetAllWithDetails();
        List<Orders> GetAll();
        Orders? GetById(int id);

        // New methods
        List<Orders> GetByCustomerId(int customerId);
        List<Orders> GetByEmployeeId(int employeeId);

        void Add(Orders entity);
        int Insert(Orders order); // Consider renaming to AddAndReturnId for clarity
        void Update(Orders order);
        void Delete(Orders order);

        /*void DeleteOrderDetails(int orderId);
        void AddOrderDetails(List<Order_Details> details);
        void Save();    */
    }
}