using Newtonsoft.Json;

namespace PitneyTest.DataAccess.DataObjects
{
    public class TrackingInfo
    {
        [JsonProperty("signedBy")]
        public object SignedBy { get; set; }

        [JsonProperty("eventCountry")]
        public object EventCountry { get; set; }

        [JsonProperty("companyName")]
        public object CompanyName { get; set; }

        [JsonProperty("authorizedAgent")]
        public bool? AuthorizedAgent { get; set; }

        [JsonProperty("estimatedDeliveryDate")]
        public object EstimatedDeliveryDate { get; set; }

        [JsonProperty("eventCode")]
        public string EventCode { get; set; }

        [JsonProperty("eventCity")]
        public object EventCity { get; set; }

        [JsonProperty("eventZip")]
        public object EventZip { get; set; }

        [JsonProperty("eventState")]
        public object EventState { get; set; }

        [JsonProperty("partnerTransactionId")]
        public object PartnerTransactionId { get; set; }

        [JsonProperty("eventDescription")]
        public string EventDescription { get; set; }

        [JsonProperty("trackingStatus")]
        public string TrackingStatus { get; set; }

        [JsonProperty("trackingId")]
        public string TrackingId { get; set; }

        [JsonProperty("eventDate")]
        public long? EventDate { get; set; }
    }
}