using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace PitneyTest.API
{
    public class ApiDataUriBuilder
    {
        public ApiDataUriBuilder(ApiBuilderConfiguration configuration = null)
        {
            var dataApiUrlTemplate = ConfigurationManager.AppSettings[Constants.DataApiUrlTemplate];
            var dataServer = ConfigurationManager.AppSettings[Constants.DataServer];
            DataApiUri = new Uri(string.Format(dataApiUrlTemplate.TrimEnd('\\') + "\\", dataServer));
            Configuration = configuration;
        }

        public ApiDataUriBuilder(Uri dataApuUri, ApiBuilderConfiguration configuration = null)
        {
            DataApiUri = dataApuUri;
            Configuration = configuration;
        }

        #region Private members

        private string GetQueryParams(UriBuilder uriBuilder)
        {
            var paramDictionary = GetUrlParameters();
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var param in paramDictionary)
            {
                query[param.Key] = param.Value;
            }
            return query.ToString();
        }

        private Dictionary<string, string> GetUrlParameters()
        {
            var parameters = new Dictionary<string, string>();

            if (Configuration == null)
                return parameters;

            if (Configuration.EndDate.HasValue)
                parameters.Add("endDate", string.Format("{0:s}Z", Configuration.EndDate));

            if (Configuration.StartDate.HasValue)
                parameters.Add("startDate", string.Format("{0:s}Z", Configuration.StartDate));

            if (Configuration.PageNumber.HasValue)
                parameters.Add("page", Configuration.PageNumber.Value.ToString());

            if (Configuration.PageSize.HasValue)
                parameters.Add("size", Configuration.PageSize.Value.ToString());

            var sortParameter = GetSortParameter(Configuration.SortField, Configuration.SortOrder);
            if (sortParameter.HasValue)
                parameters.Add(sortParameter.Value.Key, sortParameter.Value.Value);

            if (Configuration.CustomSettings != null)
                foreach (var customSetting in Configuration.CustomSettings)
                {
                    parameters.Add(customSetting.Key, customSetting.Value);
                }

            return parameters;
        }

        private KeyValuePair<string, string>? GetSortParameter(SortField? sortField, SortOrder? sortOrder)
        {
            if (!sortOrder.HasValue && !sortField.HasValue)
                return null;

            if (sortOrder.HasValue && sortField.HasValue)
                return new KeyValuePair<string, string>("sort", string.Format("{0},{1}",
                    Enum.GetName(typeof(SortField), sortField.Value),
                    Enum.GetName(typeof(SortOrder), sortOrder.Value)));

            if (sortOrder.HasValue)
                return new KeyValuePair<string, string>("sort", string.Format("{0}",
                    Enum.GetName(typeof(SortField), sortField.Value)));

            return new KeyValuePair<string, string>("sort", string.Format("{0}",
                Enum.GetName(typeof(SortOrder), sortOrder.Value)));
        }

        #endregion

        public ApiBuilderConfiguration Configuration { get; set; }

        public Uri DataApiUri { get; private set; }

        public Uri GetTransactionsUri()
        {
            var uriBuilder = new UriBuilder(DataApiUri);

            uriBuilder.Query = GetQueryParams(uriBuilder);
            uriBuilder.Path += "transactions";

            return uriBuilder.Uri;
        }
    }
}