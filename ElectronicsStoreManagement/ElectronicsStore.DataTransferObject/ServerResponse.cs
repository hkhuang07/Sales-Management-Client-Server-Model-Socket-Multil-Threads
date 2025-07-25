using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElectronicsStore.DataTransferObject
{
    public class ServerResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        // Data sẽ là kiểu dữ liệu T được chỉ định
        // Newtonsoft.Json sẽ tự động xử lý tuần tự hóa/giải tuần tự hóa T
        public T Data { get; set; }

        // Constructor mặc định (cần thiết cho Json.NET)
        public ServerResponse() { }

        // Constructor để tạo phản hồi thành công
        public ServerResponse(T data, string message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor để tạo phản hồi lỗi
        public ServerResponse(string message, bool success = false)
        {
            Success = success; // Mặc định là false cho lỗi
            Message = message;
            Data = default(T); // Gán default cho Data khi có lỗi
        }

        // Phương thức tĩnh tiện ích để tạo phản hồi thành công
        public static ServerResponse<T> Ok(T data, string message = null)
        {
            return new ServerResponse<T>(data, message);
        }

        // Phương thức tĩnh tiện ích để tạo phản hồi lỗi
        public static ServerResponse<T> Error(string message)
        {
            return new ServerResponse<T>(message);
        }
    }
}
