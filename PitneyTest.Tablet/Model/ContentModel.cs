using System.Collections.Generic;
using PitneyTest.DataAccess.DataObjects;

namespace PitneyTest.Tablet.Model
{
    internal class ContentModel
    {
        public ContentModel(Content source, string groupKey)
        {
            Source = source;
            GroupKey = groupKey;
        }

        public string GroupKey { get; set; }
        public Content Source { get; private set; }

        public string UserFullName
        {
            get { return string.Format("{0} ({1})", Source.UserDisplayName, UserEmail); }
        }

        public string UserDisplayName
        {
            get { return Source.UserDisplayName; }
        }

        public string UserEmail
        {
            get { return Source.UserEmail; }
        }

        public object ShipmentDate
        {
            get { return Source.ShipmentDate; }
        }

        public string DebitOrCredit
        {
            get { return Source.DebitOrCredit; }
        }

        public double MailServiceCost
        {
            get { return Source.MailServiceCost; }
        }

        public string Currency
        {
            get { return Source.Currency; }
        }

        public string Description
        {
            get { return Source.Description; }
        }

        public IList<string> RecipientEmails
        {
            get { return Source.RecipientEmails; }
        }

        public IList<string> SenderEmails
        {
            get { return Source.SenderEmails; }
        }

        public IList<string> ExtraServices
        {
            get { return Source.ExtraServices; }
        }

        public string RefundStatus
        {
            get { return Source.RefundStatus; }
        }

        public string MailClass
        {
            get { return Source.MailClass; }
        }

        public double PrepayBalance
        {
            get { return Source.PrepayBalance; }
        }

        public double WidthMeters
        {
            get { return Source.WidthMeters; }
        }

        public double HeightMeters
        {
            get { return Source.HeightMeters; }
        }

        public double WeightKilos
        {
            get { return Source.WeightKilos; }
        }

        public string AmountFull
        {
            get { return string.Format("{0} ({1})", Source.Amount, Currency); }
        }
    }
}