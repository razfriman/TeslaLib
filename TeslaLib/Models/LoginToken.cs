using Newtonsoft.Json;

namespace TeslaLib.Models
{
    public class LoginToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

    }
}
