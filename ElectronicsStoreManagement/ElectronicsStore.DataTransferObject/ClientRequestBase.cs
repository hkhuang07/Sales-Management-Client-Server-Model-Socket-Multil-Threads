using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ClientRequestBase
    {
        // Data sẽ là JToken hoặc JObject sau khi deserialize ban đầu.
        // Sau đó, nó sẽ được deserialize lại thành kiểu cụ thể trong switch-case.
        public object Data { get; set; }
        public string MethodName { get; set; }  
    }
}
