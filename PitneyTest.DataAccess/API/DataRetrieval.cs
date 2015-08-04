using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyTest.DataAccess.DataObjects;
using PitneyTest.DataAccess.Token;

namespace PitneyTest.DataAccess.API
{
    public class DataRetrieval
    {
        private const string AuthTokenHeader = "authToken";
        private const string CsrfTokenHeader = "x-csrf-token";
        private const string UserIdHeader = "QA-IDP-USER-ID";

        private HttpClient GetLoginHttpClient(string userId)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add(UserIdHeader, userId);

            return client;
        }

        private HttpClient GetLoginHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");

            return client;
        }

        private HttpClient GetTransactionsHttpClient(AccessToken token)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add(AuthTokenHeader, token.AuthToken);
            client.DefaultRequestHeaders.Add(CsrfTokenHeader, token.CsrfToken);

            return client;
        }

        private async Task<HttpResponseMessage> GetLoginResponseAsync(Uri uri, string userId)
        {
            var client = GetLoginHttpClient(userId);
            var response = await client.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<HttpResponseMessage> GetLoginResponseAsync(Uri uri, string userId, string password, string clientId)
        {
            var client = GetLoginHttpClient();
            var requestMessage = GetRequestMessage(uri, userId, password, clientId);
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private HttpRequestMessage GetRequestMessage(Uri uri, string userId, string password, string clientId)
        {
            var uriBuilder = new UriBuilder(uri) {Query = string.Format("clientid={0}", clientId)};
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
            var body = new {username = userId, password};
            var bodyJson = JsonConvert.SerializeObject(body);
            requestMessage.Content = new StringContent(bodyJson);
            return requestMessage;
        }

        private async Task<HttpResponseMessage> GetTransactionsResponseAsync(Uri uri, AccessToken token)
        {
            var client = GetTransactionsHttpClient(token);
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<AccessToken> GetTokenAsync(Uri uri, string userId)
        {
            try
            {
                var response = await GetLoginResponseAsync(uri, userId);

                var token = new AccessToken
                {
                    AuthToken = response.Headers.First(x => x.Key == AuthTokenHeader).Value.First(),
                    CsrfToken = response.Headers.First(x => x.Key == CsrfTokenHeader).Value.First()
                };

                return token;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AccessToken> GetTokenAsync(Uri uri, string userId, string password, string clientId)
        {
            try
            {
                var response = await GetLoginResponseAsync(uri, userId, password, clientId);

                var token = new AccessToken
                {
                    AuthToken = response.Headers.First(x => x.Key == AuthTokenHeader).Value.First(),
                    CsrfToken = response.Headers.First(x => x.Key == CsrfTokenHeader).Value.First()
                };

                return token;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Transactions> GetTransactionsAsync(Uri uri, AccessToken token)
        {
            try
            {
                var response = await GetTransactionsResponseAsync(uri, token);

                var responseString = await response.Content.ReadAsStringAsync();
                var transactions = JsonConvert.DeserializeObject<Transactions>(responseString);

                return transactions;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}