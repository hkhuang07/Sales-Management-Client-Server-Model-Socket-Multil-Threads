using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ServerResponseBase
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        
        public ServerResponseBase() { }

        public ServerResponseBase(object data, string message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }
        public ServerResponseBase(string message, bool success = false)
        {
            Success = success; // Mặc định là false cho lỗi
            Message = message;
            Data = null; // Gán null cho Data khi có lỗi
        }

        public static ServerResponseBase Ok(object data, string message = null)
        {
            return new ServerResponseBase(data, message);
        }

        public static ServerResponseBase Error(string message)
        {
            return new ServerResponseBase(message);
        }
    }

    public class ServerResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }

        public ServerResponse() { }

        public ServerResponse(T data, string message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public ServerResponse(string message, bool success = false)
        {
            Success = success; // Mặc định là false cho lỗi
            Message = message;
            Data = default(T); // Gán default cho Data khi có lỗi
        }

        public static ServerResponse<T> Ok(T data, string message = null)
        {
            return new ServerResponse<T>(data, message);
        }

        public static ServerResponse<T> Error(string message)
        {
            return new ServerResponse<T>(message);
        }
    }
}
