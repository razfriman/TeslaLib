using System.Net.Http;
using System.Net.Http.Headers;

namespace TeslaLib
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public string Token { get; set; }

        public HttpClient CreateClient(string name)
        {
            var client = new HttpClient();
            AddDefaultHeaders(client);
            return client;
        }

        private void AddDefaultHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "TeslaLib C# Library");

            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            }
        }
    }
}
