using System;

namespace ElectronicsStore.DataTransferObject
{
    public class ProductImageUploadDTO
    {
        public int ProductID { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] ImageData { get; set; } = Array.Empty<byte>();
    }
}
