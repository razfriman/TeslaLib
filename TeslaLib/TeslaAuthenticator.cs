using RestSharp;
using RestSharp.Authenticators;

namespace TeslaLib
{
    public class TeslaAuthenticator : IAuthenticator
    {
        public string Token { get; set; }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("Authorization", $"Bearer {Token}");
        }
    }
}
