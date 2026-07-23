using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class OrderDetailsDTO
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }  // Cho hiển thị
        public short Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice => Quantity * Price;

    }


}
