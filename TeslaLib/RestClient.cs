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

        public string Token { get; set; }

        public RestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<T> PostLoginToken<T>(string uri, object body = null)
        {
            using (var client = new HttpClient())
            {
                AddDefaultHeaders(client);
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_baseUrl + uri, content).ConfigureAwait(false);
                return await ProcessResponseStream<T>(response).ConfigureAwait(false);
            }
        }

        public async Task<ResponseWrapper<T>> Post<T>(string uri, object body = null)
        {
            using (var client = new HttpClient())
            {
                AddDefaultHeaders(client);
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_baseUrl + uri, content).ConfigureAwait(false);
                return await ProcessResponse<T>(response).ConfigureAwait(false);
            }
        }

        public async Task<ResponseWrapper<T>> Get<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                AddDefaultHeaders(client);
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

        private void AddDefaultHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "TeslaLib C# Library");

            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            }
        }

        private static async Task<T> ProcessResponseStream<T>(HttpResponseMessage response)
        {
            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var sr = new StreamReader(stream))
            using (var reader = new JsonTextReader(sr))
            {
                var rawJson = await JObject.LoadAsync(reader).ConfigureAwait(false);
                rawJson.ToString();
                JsonConvert.DeserializeObject<Models.LoginToken>(rawJson.ToString());
                var serializer = JsonSerializer.CreateDefault();
                serializer.Deserialize<T>(reader);
                var result = rawJson.ToObject<T>(serializer);
                return result;
            }
        }
    }
}
