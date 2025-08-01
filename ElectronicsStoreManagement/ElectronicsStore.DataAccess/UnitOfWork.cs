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

        public ICategoryRepository CategoryRepository { get; private set; }
        public IManufacturerRepository ManufacturerRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IOrderDetailsRepository OrderDetailsRepository { get; private set; }
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