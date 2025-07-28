using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int ManufacturerID { get; set; }
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
    }
}
