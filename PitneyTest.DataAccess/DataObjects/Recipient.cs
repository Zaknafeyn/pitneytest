﻿using Newtonsoft.Json;

namespace PitneyTest.DataAccess.DataObjects
{
    public class Recipient
    {
        [JsonProperty("streetLine2")]
        public string StreetLine2 { get; set; }

        [JsonProperty("streetLine1")]
        public string StreetLine1 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("streetLine4")]
        public object StreetLine4 { get; set; }

        [JsonProperty("streetLine3")]
        public string StreetLine3 { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("lastLineOnly")]
        public bool LastLineOnly { get; set; }

        [JsonProperty("isoCountry")]
        public string IsoCountry { get; set; }
    }
}