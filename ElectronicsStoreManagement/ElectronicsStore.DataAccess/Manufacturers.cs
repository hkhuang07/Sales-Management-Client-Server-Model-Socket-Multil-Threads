using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectronicsStore.DataAccess
{
    public class Manufacturers
    {
        public int ID { get; set; }
        public string ManufacturerName { get; set; }
        public string? ManufacturerAddress { get; set; }
        public string? ManufacturerPhone { get; set; }
        public string? ManufacturerEmail { get; set; }
        public virtual ObservableCollectionListSource<Products> Product { get; } = new();

    }
}