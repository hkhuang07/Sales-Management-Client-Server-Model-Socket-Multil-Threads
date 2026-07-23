using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ClientRequestBase
    {
        public object Data { get; set; }
        public string MethodName { get; set; }  
    }
    public class ClientRequest<T>
    {
        public string MethodName { get; set; }
        public T Data { get; set; }

        public ClientRequest() { }

        public ClientRequest(string methodName, T data)
        {
            MethodName = methodName;
            Data = data;
        }
    }
}
