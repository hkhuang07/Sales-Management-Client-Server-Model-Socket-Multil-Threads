using ElectronicsStore.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public interface IOrderRepository
    {
        List<Orders> GetAllWithDetails();
        List<Orders> GetAll();
        Orders? GetById(int id);
        List<Orders> GetByStatus(string status);

        // New methods
        List<Orders> GetByCustomerId(int customerId);
        List<Orders> GetByEmployeeId(int employeeId);

        void Add(Orders entity);
        int Insert(Orders order); // Consider renaming to AddAndReturnId for clarity
        void Update(Orders order);

        void Delete(Orders order);

    }
}