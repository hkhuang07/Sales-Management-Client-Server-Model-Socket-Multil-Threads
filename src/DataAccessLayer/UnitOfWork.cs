// File: ElectronicsStore.DataAccess/UnitOfWork.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElectronicsStoreContext _context;
        private ICategoryRepository? _categoryRepository;
        private IManufacturerRepository? _manufacturerRepository;
        private IProductRepository? _productRepository;
        private IEmployeeRepository? _employeeRepository;
        private ICustomerRepository? _customerRepository;
        private IOrderRepository? _orderRepository;
        private IOrderDetailsRepository? _orderDetailsRepository;

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
        public IManufacturerRepository ManufacturerRepository => _manufacturerRepository ??= new ManufacturerRepository(_context);
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(_context);
        public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_context);
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);
        public IOrderDetailsRepository OrderDetailsRepository => _orderDetailsRepository ??= new OrderDetailsRepository(_context);

        public UnitOfWork(ElectronicsStoreContext context)
        {
            _context = context;
        }

        // Tự động giải phóng DbContext
        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Triển khai các phương thức giao dịch
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }
    }
}