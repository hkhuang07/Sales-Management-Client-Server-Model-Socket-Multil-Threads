using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElectronicsStore.DataTransferObject
{
    public class ClientRequest<T>
    {
        public string MethodName { get; set; }

        // Data sẽ là kiểu dữ liệu T được chỉ định
        // Newtonsoft.Json sẽ tự động xử lý tuần tự hóa/giải tuần tự hóa T
        public T Data { get; set; }

        // Constructor mặc định (cần thiết cho Json.NET)
        public ClientRequest() { }

        public ClientRequest(string methodName, T data)
        {
            MethodName = methodName;
            Data = data;
        }
    }

}
