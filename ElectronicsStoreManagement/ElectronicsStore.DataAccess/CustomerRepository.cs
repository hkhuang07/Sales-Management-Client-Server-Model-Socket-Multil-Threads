using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ElectronicsStoreContext _context;

        public CustomerRepository()
        {
            _context = new ElectronicsStoreContext();
        }

        // Tra cứu
        public List<Customers> GetAll() => _context.Customer.ToList();

        public Customers? GetById(int id) => _context.Customer.Find(id);

        //Thêm mới
        public void Add(Customers customer)
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
        }

        //Cập nhật
        public void Update(Customers customer)
        {
            var existingCustomer = _context.Customer.Find(customer.ID);
            if (existingCustomer != null)
            {
                // Cập nhật các thuộc tính cần thiết
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.CustomerAddress = customer.CustomerAddress;
                existingCustomer.CustomerPhone = customer.CustomerPhone;
                existingCustomer.CustomerEmail = customer.CustomerEmail;

                _context.Customer.Update(existingCustomer);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Customer with ID = {customer.ID} not found.");
            }
            _context.Customer.Update(customer);
            _context.SaveChanges();
        }

        //Xóa
        public void Delete(Customers customer)
        {
            _context.Customer.Remove(customer);
            _context.SaveChanges();
        }
    }
}
