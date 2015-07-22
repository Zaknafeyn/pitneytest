using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PitneyTest.API;

namespace PitneyTest.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestDataRetrieval()
        {
            var url = new Uri("http://foundation-qa.horizon.pitneycloud.com/api/v1/user/auth/guam");
            var userId = "950f9e6a-2ce9-44f2-9f25-31e5caad60a0@pb";
            var target = new DataRetrieval();
            var result = target.GetToken(url, userId);
        }

        [Test]
        public void TestDataRetrievalGetTransactions()
        {
            var transactionsUrl = new Uri("http://shipping-qa.horizon.pitneycloud.com/api/v1/transactions");
            var tokenUrl = new Uri("http://foundation-qa.horizon.pitneycloud.com/api/v1/user/auth/guam");
            var userId = "950f9e6a-2ce9-44f2-9f25-31e5caad60a0@pb";
            var target = new DataRetrieval();
            var token = target.GetToken(tokenUrl, userId);
            var transactions = target.GetTransactions(transactionsUrl, token);
        }
    }
}
