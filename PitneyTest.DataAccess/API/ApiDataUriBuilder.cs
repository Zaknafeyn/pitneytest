using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyTest.DataAccess.Helpers;

namespace PitneyTest.DataAccess.API
{
    public class ApiDataUriBuilder
    {
        public ApiDataUriBuilder(ApiBuilderConfiguration configuration = null)
        {
            var dataApiUrlTemplate = DataAccess.Configuration.DataApiUrlTemplate;
            var dataServer = DataAccess.Configuration.DataServer;
            DataApiUri = new Uri(string.Format(dataApiUrlTemplate.TrimEnd('\\') + "\\", dataServer));
            Configuration = configuration;
        }

        public ApiDataUriBuilder(Uri dataApuUri, ApiBuilderConfiguration configuration = null)
        {
            DataApiUri = dataApuUri;
            Configuration = configuration;
        }

        public ApiBuilderConfiguration Configuration { get; set; }
        public Uri DataApiUri { get; }

        public Uri GetTransactionsUri()
        {
            var uriBuilder = new UriBuilder(DataApiUri);

            uriBuilder.Query = GetQueryParams(uriBuilder);
            uriBuilder.Path += "transactions";

            return uriBuilder.Uri;
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
            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter {CamelCaseText = true});
                return settings;
            });

            if (!sortOrder.HasValue && !sortField.HasValue)
                return null;

            if (sortOrder.HasValue && sortField.HasValue)
                return new KeyValuePair<string, string>("sort", string.Format("{0},{1}",
                    sortField.Value.ToRequestParameter(),
                    sortOrder.Value.ToRequestParameter()));

            if (sortOrder.HasValue)
                return new KeyValuePair<string, string>("sort", string.Format("{0}",
                    sortOrder.Value.ToRequestParameter()));

            return new KeyValuePair<string, string>("sort", string.Format("{0}",
                sortField.Value.ToRequestParameter()));
        }

        #endregion
    }
}