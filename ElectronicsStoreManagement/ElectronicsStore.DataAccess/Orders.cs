using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectronicsStore.DataAccess
{
    public class Orders
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public virtual ObservableCollectionListSource<Order_Details> ViewDetails { get; } = new();
        public virtual Customers Customer { get; set; } = null!;
        public virtual Employees Employee { get; set; } = null!;
    }

    [NotMapped]
    public class OrderList
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; } // Thêm
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } // Thêm
        public string CustomeAddress { get; set; } //Thêm
        public string CustomerPhone { get; set; } //Thêm
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public string? ViewDetails { get; set; } // Thêm
        public double? TotalPrice { get; set; } // Thêm
    }
}
