using System;
using System.Collections.Generic;
using System.Linq;

namespace PitneyTest.DataAccess.Helpers
{
    public sealed class HttpUtility
    {
        public static Dictionary<string, string> ParseQueryString(string query)
        {
            var pairs = query.Split('&');
            return pairs
                .Select(o => o.Split('='))
                .Where(items => items.Count() == 2)
                .ToDictionary(pair => Uri.UnescapeDataString(pair[0]),
                    pair => Uri.UnescapeDataString(pair[1]));
        }
    }
}