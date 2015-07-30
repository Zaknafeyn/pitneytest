using System;
using System.IO;
using System.Net;
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

        private HttpWebRequest GetLoginWebRequest(Uri uri, string userId)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;
            request.Headers.Add(UserIdHeader, userId);

            return request;
        }

        private HttpWebRequest GetTransactionsWebRequest(Uri uri, AccessToken token)
        {
            var uriString = uri.ToString();
            var request = (HttpWebRequest)WebRequest.Create(uriString);

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;
            request.Headers.Add(AuthTokenHeader, token.AuthToken);
            request.Headers.Add(CsrfTokenHeader, token.CsrfToken);

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

        public AccessToken GetToken(Uri uri, string userId)
        {
            var request = GetLoginWebRequest(uri, userId);

            var response = (HttpWebResponse)request.GetResponse();

            return GetTokenFromResponse(response);
        }

        public Transactions GetTransactions(Uri uri, AccessToken token)
        {
            var request = GetTransactionsWebRequest(uri, token);

            var response = (HttpWebResponse)request.GetResponse();

            return GetTransactionsFromResponse(response);
        }
    }
}