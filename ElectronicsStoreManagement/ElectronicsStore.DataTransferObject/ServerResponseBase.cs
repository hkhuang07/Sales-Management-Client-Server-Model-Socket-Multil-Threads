using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ServerResponseBase
    {
        // Data có thể là một đối tượng JSON (List<DTO>, DTO, int, string) hoặc null.
        // Newtonsoft.Json sẽ tự động xử lý tuần tự hóa khi gửi.
        public object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        

        // Constructor mặc định (cần thiết cho Json.NET)
        public ServerResponseBase() { }



        // Constructor để tạo phản hồi thành công
        public ServerResponseBase(object data, string message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor để tạo phản hồi lỗi
        public ServerResponseBase(string message, bool success = false)
        {
            Success = success; // Mặc định là false cho lỗi
            Message = message;
            Data = null; // Gán null cho Data khi có lỗi
        }



        // Phương thức tĩnh tiện ích để tạo phản hồi thành công
        public static ServerResponseBase Ok(object data, string message = null)
        {
            return new ServerResponseBase(data, message);
        }

        // Phương thức tĩnh tiện ích để tạo phản hồi lỗi
        public static ServerResponseBase Error(string message)
        {
            return new ServerResponseBase(message);
        }
    }
}
