using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ManufacturerDTO
    {
        public int ID { get; set; }
        public string ManufacturerName { get; set; }
        public string? ManufacturerAddress { get; set; }
        public string? ManufacturerPhone { get; set; }
        public string? ManufacturerEmail { get; set; }
    }
}
