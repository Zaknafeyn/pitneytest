using System.Collections.Generic;
using Newtonsoft.Json;

namespace PitneyTest.DataAccess.DataObjects
{
    public class Content
    {
        [JsonProperty("extraServicesCost")]
        public double ExtraServicesCost { get; set; }

        [JsonProperty("extraServices")]
        public IList<string> ExtraServices { get; set; }

        [JsonProperty("debitOrCredit")]
        public string DebitOrCredit { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("refundStatus")]
        public string RefundStatus { get; set; }

        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("externalReferenceId")]
        public object ExternalReferenceId { get; set; }

        [JsonProperty("mailClass")]
        public string MailClass { get; set; }

        [JsonProperty("prepayBalance")]
        public double PrepayBalance { get; set; }

        [JsonProperty("widthMeters")]
        public double WidthMeters { get; set; }

        [JsonProperty("heightMeters")]
        public double HeightMeters { get; set; }

        [JsonProperty("notifyRecipientOnDelivery")]
        public bool NotifyRecipientOnDelivery { get; set; }

        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonProperty("shipmentDate")]
        public object ShipmentDate { get; set; }

        [JsonProperty("notifySenderOnDelivery")]
        public bool NotifySenderOnDelivery { get; set; }

        [JsonProperty("createDate")]
        public object CreateDate { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("weightKilos")]
        public double WeightKilos { get; set; }

        [JsonProperty("userDisplayName")]
        public string UserDisplayName { get; set; }

        [JsonProperty("costAccountName")]
        public string CostAccountName { get; set; }

        [JsonProperty("lengthMeters")]
        public double LengthMeters { get; set; }

        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }

        [JsonProperty("insuranceValue")]
        public int InsuranceValue { get; set; }

        [JsonProperty("insuranceCost")]
        public double InsuranceCost { get; set; }

        [JsonProperty("parcelType")]
        public string ParcelType { get; set; }

        [JsonProperty("insuranceProvider")]
        public string InsuranceProvider { get; set; }

        [JsonProperty("trackingInfo")]
        public TrackingInfo TrackingInfo { get; set; }

        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        [JsonProperty("shipmentId")]
        public string ShipmentId { get; set; }

        [JsonProperty("senderEmails")]
        public IList<string> SenderEmails { get; set; }

        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("trackingStatusUpdateTime")]
        public object TrackingStatusUpdateTime { get; set; }

        [JsonProperty("girthMeters")]
        public float GirthMeters { get; set; }

        [JsonProperty("recipientEmails")]
        public IList<string> RecipientEmails { get; set; }

        [JsonProperty("customerPartnerId")]
        public string CustomerPartnerId { get; set; }

        [JsonProperty("mailServiceCost")]
        public double MailServiceCost { get; set; }
    }
}