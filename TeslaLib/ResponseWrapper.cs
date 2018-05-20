using System.Net;
using Newtonsoft.Json;

namespace TeslaLib
{
    public class ResponseWrapper<T>
    {
        public bool IsSuccess { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("response")]
        public T Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
