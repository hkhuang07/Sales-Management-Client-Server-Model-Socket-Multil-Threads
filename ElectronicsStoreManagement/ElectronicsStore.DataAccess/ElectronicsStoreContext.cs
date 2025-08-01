using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.DataAccess
{
    public class ElectronicsStoreContext : DbContext
    {
        public DbSet<Categories> Category { get; set; }
        public DbSet<Manufacturers> Manufacturer { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<Employees> Employee { get; set; }
        public DbSet<Customers> Customer { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<Order_Details> Order_Details { get; set; }

        public ElectronicsStoreContext() { }

        public ElectronicsStoreContext(DbContextOptions<ElectronicsStoreContext> options)
            : base(options)
        {
        }

        // Xóa hoặc comment phương thức OnConfiguring để chỉ sử dụng chuỗi kết nối từ Program.cs
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlServer("Data Source=.;Database=ElectronsStore;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
        //     }
        // }
    }
}