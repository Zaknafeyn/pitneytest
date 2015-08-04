using System;
using NUnit.Framework;
using PitneyTest.DataAccess.API;

namespace PitneyTest.Tests
{
    [TestFixture]
    public class DataRetrievalTest
    {
        [Test]
        [Ignore]
        public async void TestDataRetrieval()
        {
            var url = new Uri("http://foundation-qa.horizon.pitneycloud.com/api/v1/user/auth/guam");
            var userId = "950f9e6a-2ce9-44f2-9f25-31e5caad60a0@pb";
            var target = new DataRetrieval();
            var result = await target.GetTokenAsync(url, userId);
            Assert.AreNotEqual(result, null);
        }

        [Test]
        [Ignore]
        public async void TestDataRetrievalGetTokenByLoginPassword()
        {
            var url = new Uri("http://foundation-qa.horizon.pitneycloud.com/api/v1/user/auth/guam");
            var userId = "craig.j1@horizon.com";
            var password = "Testing123";
            var clientId = "2856109e-ca67-4aa8-bf34-8877bc0502e9";
            var target = new DataRetrieval();
            var result = await target.GetTokenAsync(url, userId, password, clientId);
            Assert.AreNotEqual(result, null);
        }

        [Test]
        [Ignore]
        public async void TestDataRetrievalGetTransactions()
        {
            var transactionsUrl = new Uri("http://shipping-qa.horizon.pitneycloud.com/api/v1/transactions");
            var tokenUrl = new Uri("http://foundation-qa.horizon.pitneycloud.com/api/v1/user/auth/guam");
            var userId = "950f9e6a-2ce9-44f2-9f25-31e5caad60a0@pb";
            var target = new DataRetrieval();
            var token = await target.GetTokenAsync(tokenUrl, userId);
            var transactions = await target.GetTransactionsAsync(transactionsUrl, token);
            Assert.AreNotEqual(transactions, null);
        }


    }
}