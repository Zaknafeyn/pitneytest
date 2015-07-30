using System;
using NUnit.Framework;
using PitneyTest.DataAccess.API;

namespace PitneyTest.Tests
{
    [TestFixture]
    public class ApiDataUriBuilderTests
    {
        private const string originalUrl = "http://foundation-qa.horizon.pitneycloud.com/api/v1/";

        public void TestBuilderWithStartDateParameter()
        {
            // init
            var expectedUrl = originalUrl + "transactions?startDate=2015-07-22+10:10:10Z";
            var target = new ApiDataUriBuilder(new Uri(originalUrl), new ApiBuilderConfiguration
            {
                StartDate = new DateTime(2015, 7, 22, 10, 10, 10, 123)
            });
            // act
            var result = target.GetTransactionsUri();
            //assert
            Assert.AreEqual(expectedUrl, result.ToString());
        }

        [Test]
        public void TestBuilder()
        {
            // init
            var expectedUrl = originalUrl + "transactions";
            var target = new ApiDataUriBuilder(new Uri(originalUrl));
            // act
            var result = target.GetTransactionsUri();
            //assert
            Assert.AreEqual(expectedUrl, result.ToString());
        }

        [Test]
        public void TestBuilderWithEndDateParameter()
        {
            // init
            var expectedUrl = originalUrl + "transactions?endDate=2015-07-22T10:10:10Z";
            var target = new ApiDataUriBuilder(new Uri(originalUrl), new ApiBuilderConfiguration
            {
                EndDate = new DateTime(2015, 7, 22, 10, 10, 10, 123)
            });
            // act
            var result = target.GetTransactionsUri();
            //assert
            Assert.AreEqual(expectedUrl, result.ToString());
        }

        [Test]
        public void TestBuilderWithPageParameters()
        {
            // init
            var expectedUrl = originalUrl + "transactions?page=30";
            var target = new ApiDataUriBuilder(new Uri(originalUrl), new ApiBuilderConfiguration
            {
                PageNumber = 30
            });
            // act
            var result = target.GetTransactionsUri();
            //assert
            Assert.AreEqual(expectedUrl, result.ToString());
        }

        [Test]
        public void TestBuilderWithSizeParameters()
        {
            // init
            var expectedUrl = originalUrl + "transactions?size=30";
            var target = new ApiDataUriBuilder(new Uri(originalUrl), new ApiBuilderConfiguration
            {
                PageSize = 30
            });
            // act
            var result = target.GetTransactionsUri();
            //assert
            Assert.AreEqual(expectedUrl, result.ToString());
        }

        [Test]
        [Ignore]
        public void TestBuilderWithSortParameter()
        {
            // init
            var expectedUrl = originalUrl + "transactions?sort=CreateDate%2cDesc";
            var target = new ApiDataUriBuilder(new Uri(originalUrl), new ApiBuilderConfiguration
            {
                SortField = SortField.CreateDate,
                SortOrder = SortOrder.Desc
            });
            // act
            var result = target.GetTransactionsUri();
            //assert
            Assert.AreEqual(expectedUrl, result.ToString());
        }

        [Test]
        public void TestBuilderWithStartDateAndEndDateParameters()
        {
            // init
            var expectedUrl = originalUrl + "transactions?endDate=2015-07-22T10:10:10Z&startDate=2015-07-22T10:10:10Z";
            var target = new ApiDataUriBuilder(new Uri(originalUrl), new ApiBuilderConfiguration
            {
                StartDate = new DateTime(2015, 7, 22, 10, 10, 10, 123),
                EndDate = new DateTime(2015, 7, 22, 10, 10, 10, 123),
            });
            // act
            var result = target.GetTransactionsUri();
            //assert
            Assert.AreEqual(expectedUrl, result.ToString());
        }
    }
}