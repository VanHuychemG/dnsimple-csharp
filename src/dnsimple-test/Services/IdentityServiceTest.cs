using System;
using System.Globalization;
using NUnit.Framework;

namespace dnsimple_test.Services
{
    [TestFixture]
    public class IdentityServiceTest
    {
        private DateTime CreatedAt { get; } = DateTime.ParseExact(
            "2015-09-18T23:04:37Z", "yyyy-MM-ddTHH:mm:ssZ",
            CultureInfo.CurrentCulture);

        private DateTime UpdatedAt { get; } = DateTime.ParseExact(
            "2016-06-09T20:03:39Z", "yyyy-MM-ddTHH:mm:ssZ",
            CultureInfo.CurrentCulture);

        [Test]
        [TestCase("https://api.sandbox.dnsimple.com/v2/whoami")]
        public void WhoamiAccountSuccessTest(string expectedUrl)
        {
            var client = new MockDnsimpleClient(
                "whoami/success-account.http");

            var response = client.Identity.Whoami();
            var account = response.Data.Account;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, account.Id);
                Assert.AreEqual("example-account@example.com", account.Email);
                Assert.AreEqual("dnsimple-professional",
                    account.PlanIdentifier);
                Assert.AreEqual(CreatedAt, account.CreatedAt);
                Assert.AreEqual(UpdatedAt, account.UpdatedAt);
                
                Assert.AreEqual(expectedUrl, client.RequestSentTo());
            });
        }

        [Test]
        public void WhoamiUserSuccessTest()
        {
            var client = new MockDnsimpleClient("whoami/success-user.http");

            var response = client.Identity.Whoami();
            var user = response.Data.User;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, user.Id);
                Assert.AreEqual("example-user@example.com", user.Email);
                Assert.AreEqual(CreatedAt, user.CreatedAt);
                Assert.AreEqual(UpdatedAt, user.UpdatedAt);
            });
        }
    }
}