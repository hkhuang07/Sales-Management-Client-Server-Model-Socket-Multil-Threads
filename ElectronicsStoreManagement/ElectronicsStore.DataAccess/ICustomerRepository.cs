using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public interface ICustomerRepository
    {
        List<Customers> GetAll();
        Customers? GetById(int id);
        void Add(Customers customer);
        void Update(Customers customer);
        void Delete(Customers customer);
    }
}
