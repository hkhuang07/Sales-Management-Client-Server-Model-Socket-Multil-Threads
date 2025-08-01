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

        // Constructor đã được cập nhật để nhận DbContext qua Dependency Injection
        public CustomerRepository(ElectronicsStoreContext context)
        {
            _context = context;
        }

        // Tra cứu
        public List<Customers> GetAll() => _context.Customer.ToList();

        public Customers? GetById(int id) => _context.Customer.Find(id);

        //Thêm mới
        public void Add(Customers customer)
        {
            _context.Customer.Add(customer);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }

        //Cập nhật
        public void Update(Customers customer)
        {
            var existingCustomer = _context.Customer.Find(customer.ID);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.CustomerAddress = customer.CustomerAddress;
                existingCustomer.CustomerPhone = customer.CustomerPhone;
                existingCustomer.CustomerEmail = customer.CustomerEmail;

                _context.Customer.Update(existingCustomer);
                // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
            }
            else
            {
                throw new Exception($"Customer with ID = {customer.ID} not found.");
            }
        }

        //Xóa
        public void Delete(Customers customer)
        {
            _context.Customer.Remove(customer);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }
    }
}