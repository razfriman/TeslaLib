using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TeslaLib
{
    public class RestClient
    {
        private readonly string _baseUrl;
        private readonly HttpClientFactory _httpClientFactory;

        public RestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClientFactory = new HttpClientFactory();
        }

        public void SetToken(string token)
        {
            _httpClientFactory.Token = token;
        }

        public async Task<T> PostLoginToken<T>(string uri, object body = null)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_baseUrl + uri, content).ConfigureAwait(false);
                return await ProcessResponseStream<T>(response).ConfigureAwait(false);
            }
        }

        public async Task<ResponseWrapper<T>> Post<T>(string uri, object body = null)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_baseUrl + uri, content).ConfigureAwait(false);
                return await ProcessResponse<T>(response).ConfigureAwait(false);
            }
        }

        public async Task<ResponseWrapper<T>> Get<T>(string uri)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(_baseUrl + uri).ConfigureAwait(false);
                return await ProcessResponse<T>(response).ConfigureAwait(false);
            }
        }

        private static async Task<ResponseWrapper<T>> ProcessResponse<T>(HttpResponseMessage response)
        {
            var result = new ResponseWrapper<T>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                result.Data = await ProcessResponseStream<T>(response).ConfigureAwait(false);
            }
            else
            {
                result.ErrorMessage = response.ReasonPhrase;
            }

            return result;
        }

        private static async Task<T> ProcessResponseStream<T>(HttpResponseMessage response)
        {
            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var sr = new StreamReader(stream))
            using (var reader = new JsonTextReader(sr))
            {
                var rawJson = await JObject.LoadAsync(reader).ConfigureAwait(false);
                JsonConvert.DeserializeObject<Models.LoginToken>(rawJson.ToString());
                var serializer = JsonSerializer.CreateDefault();
                serializer.Deserialize<T>(reader);
                var result = rawJson.ToObject<T>(serializer);
                return result;
            }
        }
    }
}
