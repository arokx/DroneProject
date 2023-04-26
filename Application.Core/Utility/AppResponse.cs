using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Utility
{
    public class AppResponse<T> where T : class
    {
        public T Data { get; set; }
        public Meta Meta { get; set; }
        public override string ToString()
        {
            // return JsonSerializer.Serialize(this);
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }

    public class Meta
    {
        public bool IsSucceeded { get; set; }
        public int HttpErrorCode { get; set; }
        public string Message { get; set; }
    }
}
