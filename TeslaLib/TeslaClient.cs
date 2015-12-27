using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TeslaLib.Models;
using TeslaLib.Converters;

namespace TeslaLib
{

    public class TeslaClient
    {
        public string Email { get; }
        public string TeslaClientID { get; }
        public string TeslaClientSecret { get; }
        public string AccessToken { get; private set; }

        public RestClient Client { get; set; }

        public static readonly string BASE_URL = "https://owner-api.teslamotors.com/api/1";
        public static readonly string VERSION = "1.1.0";

        public TeslaClient(string email, string teslaClientId, string teslaClientSecret)
        {
            Email = email;
            TeslaClientID = teslaClientId;
            TeslaClientSecret = teslaClientSecret;

            Client = new RestClient(BASE_URL);
            Client.Authenticator = new TeslaAuthenticator();
        }

        public class TeslaAuthenticator : RestSharp.Authenticators.IAuthenticator
        {
            public string Token { get; set; }
            public void Authenticate(IRestClient client, IRestRequest request)
            {
                request.AddHeader("Authorization", $"Bearer {Token}");
            }
        }

        public void LoginUsingCache(string password)
        {
            LoginToken token = LoginTokenCache.GetToken(Email);
            if (token != null)
            {
                SetToken(token);
            }
            else
            {
                token = GetLoginToken(password);
                SetToken(token);
                LoginTokenCache.AddToken(Email, token);
            }
        }

        public void Login(string password)
        {
            LoginToken token = GetLoginToken(password);
            SetToken(token);
        }

        private LoginToken GetLoginToken(string password)
        {
            var loginClient = new RestClient("https://owner-api.teslamotors.com/oauth");
            var request = new RestRequest("token");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                grant_type = "password",
                client_id = TeslaClientID,
                client_secret = TeslaClientSecret,
                email = Email,
                password = password
            });
            var response = loginClient.Post<LoginToken>(request);
            var token = response.Data;
            return token;
        }

        internal void SetToken(LoginToken token)
        {
            var auth = Client.Authenticator as TeslaAuthenticator;
            auth.Token = token.AccessToken;
            AccessToken = token.AccessToken;
        }

        public void ClearLoginTokenCache()
        {
            LoginTokenCache.ClearCache();
        }

        public List<TeslaVehicle> LoadVehicles()
        {
            var request = new RestRequest("vehicles");
            var response = Client.Get(request);

            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<List<TeslaVehicle>>(json.ToString());

            data.ForEach(x => x.Client = Client);

            return data;
        }
    }
}
