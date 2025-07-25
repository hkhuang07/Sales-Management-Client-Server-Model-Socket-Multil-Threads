using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectronicsStore.DataAccess
{
    public class Customers
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public virtual ObservableCollectionListSource<Orders> Order { get; } = new();
    }
}
