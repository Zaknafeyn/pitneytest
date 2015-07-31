using System;
using System.Net;

namespace PitneyTest.DataAccess.Helpers
{
    internal static class WebRequestHelper
    {
        public static WebRequest Create(Uri uri)
        {
            var request = WebRequest.Create(uri);

            if (!string.IsNullOrEmpty(Configuration.ProxyUrl))
            {
                request.Proxy = new Proxy(new Uri(Configuration.ProxyUrl))
                {
                    Credentials = new NetworkCredential(Configuration.ProxyUser, Configuration.ProxyPassword)
                };
            }
            return request;
        }
    }

    internal class Proxy : IWebProxy
    {
        private readonly Uri _proxyUri;

        public Proxy(Uri proxyUri)
        {
            _proxyUri = proxyUri;
        }

        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination)
        {
            return _proxyUri;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
}