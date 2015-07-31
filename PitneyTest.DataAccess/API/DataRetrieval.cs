using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyTest.DataAccess.DataObjects;
using PitneyTest.DataAccess.Helpers;
using PitneyTest.DataAccess.Token;

namespace PitneyTest.DataAccess.API
{
    public class DataRetrieval
    {
        private const string AuthTokenHeader = "authToken";
        private const string CsrfTokenHeader = "x-csrf-token";
        private const string UserIdHeader = "QA-IDP-USER-ID";

        private WebRequest GetLoginWebRequest(Uri uri, string userId)
        {
            var request = WebRequestHelper.Create(uri);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers[UserIdHeader] = userId;

            return request;
        }

        private WebRequest GetTransactionsWebRequest(Uri uri, AccessToken token)
        {
            var request = WebRequestHelper.Create(uri);

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers[AuthTokenHeader] = token.AuthToken;
            request.Headers[CsrfTokenHeader] = token.CsrfToken;

            return request;
        }

        private AccessToken GetTokenFromResponse(WebResponse response)
        {
            var authToken = response.Headers[AuthTokenHeader];
            var csrfToken = response.Headers[CsrfTokenHeader];
            return new AccessToken
            {
                AuthToken = authToken,
                CsrfToken = csrfToken
            };
        }

        private Transactions GetTransactionsFromResponse(WebResponse response)
        {
            try
            {
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var transactions = JsonConvert.DeserializeObject<Transactions>(responseString);
                return transactions;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AccessToken> GetTokenAsync(Uri uri, string userId)
        {
            var request = GetLoginWebRequest(uri, userId);
            var response = await request.GetResponseAsync();
            return GetTokenFromResponse(response);
        }

        public async Task<Transactions> GetTransactionsAsync(Uri uri, AccessToken token)
        {
            var request = GetTransactionsWebRequest(uri, token);
            var response = await request.GetResponseAsync();
            return GetTransactionsFromResponse(response);
        }
    }
}