using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public class Order_Details
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public short Quantity { get; set; }
        public int Price { get; set; }
        public virtual Orders Order { get; set; } = null!;
        public virtual Products Product { get; set; } = null!;
    }

    [NotMapped]
    public class OrderDetailsList
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public short Quantity { get; set; }
        public int Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
