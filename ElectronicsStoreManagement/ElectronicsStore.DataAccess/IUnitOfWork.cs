using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        // Add all your repositories here as properties
        ICategoryRepository CategoryRepository { get; }
        IManufacturerRepository ManufacturerRepository { get; }
        IProductRepository ProductRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        // Sau khi hoàn thành, bạn có thể tự thêm các repository khác vào.

        Task<int> SaveChangesAsync();
        int SaveChanges();

        // Thêm các phương thức quản lý giao dịch
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}