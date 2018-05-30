using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using TeslaLib.Models;

namespace TeslaLib
{
    public class TeslaClient
    {
        public string Email { get; }
        public string TeslaClientId { get; }
        public string TeslaClientSecret { get; }
        public RestClient Client { get; set; }

        public const string LoginUrl = "https://owner-api.teslamotors.com/oauth/";
        public const string BaseUrl = "https://owner-api.teslamotors.com/api/1/";
        public const string Version = "1.1.0";

        public TeslaClient(string email, string teslaClientId, string teslaClientSecret)
        {
            Email = email;
            TeslaClientId = teslaClientId;
            TeslaClientSecret = teslaClientSecret;
            Client = new RestClient(BaseUrl);
        }

        public async Task LoginUsingCache(string password)
        {
            var token = LoginTokenCache.GetToken(Email);
            if (token != null)
            {
                SetToken(token);
            }
            else
            {
                token = await GetLoginToken(password).ConfigureAwait(false);
                SetToken(token);
                LoginTokenCache.AddToken(Email, token);
            }
        }

        public async Task Login(string password) => SetToken(await GetLoginToken(password).ConfigureAwait(false));

        private async Task<LoginToken> GetLoginToken(string password)
        {
            var loginClient = new RestClient(LoginUrl);

            var response = await loginClient.PostLoginToken<LoginToken>("token", new
            {
                grant_type = "password",
                client_id = TeslaClientId,
                client_secret = TeslaClientSecret,
                email = Email,
                password
            }).ConfigureAwait(false);

            return response;
        }

        internal void SetToken(LoginToken token)
        {
            if (token == null)
            {
                throw new AuthenticationException("Invalid token. Email or password is incorrect");
            }

            Client.SetToken(token.AccessToken);
        }

        public void ClearLoginTokenCache() => LoginTokenCache.ClearCache();

        public async Task<ResponseWrapper<List<TeslaVehicle>>> LoadVehicles()
        {
            var response = await Client.Get<List<TeslaVehicle>>("vehicles").ConfigureAwait(false);

            response.Data?.ForEach(x => x.Client = Client);

            return response;
        }
    }
}
