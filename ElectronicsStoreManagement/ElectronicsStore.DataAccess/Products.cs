using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectronicsStore.DataAccess
{
    public class Products
    {
        public int ID { get; set; }
        public int ManufacturerID { get; set; }
        public int CategoryID { get; set; }                                                     
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public virtual ObservableCollectionListSource<Order_Details> Order_Details { get; } = new();
        public virtual Categories Category { get; set; } = null!;
        public virtual Manufacturers Manufacturer { get; set; } = null!;
    }
    [NotMapped]
    public class ProductList
    {
        public int ID { get; set; }
        public int ManufacturerID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string ManufacturerName { get; set; }
        public string CategoryName { get; set; }
    }

}
