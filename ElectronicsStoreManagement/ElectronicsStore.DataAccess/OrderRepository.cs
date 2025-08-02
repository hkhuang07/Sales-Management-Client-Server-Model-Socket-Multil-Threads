using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicsStore.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ElectronicsStoreContext _context;

        public OrderRepository(ElectronicsStoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Tra cứu với chi tiết.
        // Giả định tên navigation property là 'Order_Details'
        public List<Orders> GetAllWithDetails()
        {
            return _context.Order
                .Include(o => o.Employee)
                .Include(o => o.Customer)
                .Include(o => o.ViewDetails)
                .ToList();
        }

        // Tra cứu không có chi tiết
        public List<Orders> GetAll() => _context.Order.ToList();

        // Tra cứu theo ID với chi tiết
        public Orders? GetById(int id) => _context.Order
            .Include(o => o.ViewDetails)
            .FirstOrDefault(o => o.ID == id);

        public List<Orders> GetByCustomerId(int customerId)
        {
            return _context.Order
                .Where(o => o.CustomerID == customerId)
                .Include(o => o.Employee)
                .Include(o => o.Customer)
                .Include(o => o.ViewDetails)
                .ToList();
        }
        public List<Orders> GetByStatus(string status)
        {
            return _context.Order
                .Where(o => o.Status == status)
                .Include(o => o.Employee)
                .Include(o => o.Customer)
                .Include(o => o.ViewDetails)
                .ToList();
        }

        public List<Orders> GetByEmployeeId(int employeeId)
        {
            return _context.Order
                .Where(o => o.EmployeeID == employeeId)
                .Include(o => o.Employee)
                .Include(o => o.Customer)
                .Include(o => o.ViewDetails)
                .ToList();
        }

        public void Add(Orders entity)
        {
            _context.Order.Add(entity);
        }

        public int Insert(Orders order)
        {
            _context.Order.Add(order);
            // Vẫn trả về ID, việc lưu sẽ do service đảm nhiệm
            return order.ID;
        }

        public void Update(Orders order)
        {
            _context.Order.Update(order);
        }
    

        public void Delete(Orders order)
        {
            _context.Order.Remove(order);
        }
    }
}