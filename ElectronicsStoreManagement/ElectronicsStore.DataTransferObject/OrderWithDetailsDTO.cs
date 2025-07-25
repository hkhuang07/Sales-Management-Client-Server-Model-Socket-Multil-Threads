using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class OrderWithDetailsDTO
    {
        public OrderDTO Order { get; set; }
        public List<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
