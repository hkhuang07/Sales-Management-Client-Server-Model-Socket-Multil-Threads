using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.DataAccess
{
    public class ElectronicsStoreContextFactory : IDesignTimeDbContextFactory<ElectronicsStoreContext>
    {
        public ElectronicsStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ElectronicsStoreContext>();
            optionsBuilder.UseSqlServer(
                "Data Source=.;Database=ElectronsStore;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
            );

            return new ElectronicsStoreContext(optionsBuilder.Options);
        }
    }
}
