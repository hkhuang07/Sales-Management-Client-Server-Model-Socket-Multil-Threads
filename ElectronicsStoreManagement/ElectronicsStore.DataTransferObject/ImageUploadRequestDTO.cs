using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ImageUploadRequestDTO
    {
        public int ProductId { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
    }
}
