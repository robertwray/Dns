using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dns.Test.DnsQueryHeaderTests
{
    [TestFixture]
    public sealed class Constructor_ByteArray
    {
        private readonly static byte[] ultraasp_dot_net_A_Record = new byte[] { 0, 2, 129, 128, 0, 1, 0, 1, 0, 0, 0, 0, 8, 117, 108, 116, 114, 97, 97, 115, 112, 3, 110, 101, 116, 0, 0, 1, 0, 1, 192, 12, 0, 1, 0, 1, 0, 0, 111, 69, 0, 4, 91, 209, 187, 19 };
        private readonly static byte[] test_dot_cocktail_dot_local_A_Record = new byte[] { 0, 1, 133, 128, 0, 1, 0, 2, 0, 0, 0, 0, 4, 116, 101, 115, 116, 8, 99, 111, 99, 107, 116, 97, 105, 108, 5, 108, 111, 99, 97, 108, 0, 0, 1, 0, 1, 192, 12, 0, 5, 0, 1, 0, 0, 14, 16, 0, 9, 6, 118, 111, 105, 112, 45, 49, 192, 17, 192, 49, 0, 1, 0, 1, 0, 0, 14, 16, 0, 4, 10, 1, 1, 231 };

        [Test]
        public void TransactionId_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.TransactionId, Is.EqualTo(1));
        }

        [Test]
        public void QuestionCount_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.QuestionCount, Is.EqualTo(1));
        }

        [Test]
        public void AnswerCount_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.AnswerCount, Is.EqualTo(2));
        }

        [Test]
        public void NameServerCount_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.NameServerCount, Is.EqualTo(0));
        }

        [Test]
        public void AdditionalRecordCount_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.AdditionalRecordCount, Is.EqualTo(0));
        }

        [Test]
        public void QueryType_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.QueryType, Is.EqualTo(DnsQueryType.Response));
        }

        [Test]
        public void RecursionDesired_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.RecursionDesired, Is.EqualTo(true));
        }

        [Test]
        public void RecursionAvailable_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.RecursionAvailable, Is.EqualTo(true));
        }

        [Test]
        public void Authoritative_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.Authoritative, Is.EqualTo(true));
        }

        [Test]
        public void Truncated_Is_Returned_Correctly()
        {
            var result = new DnsQueryHeader(test_dot_cocktail_dot_local_A_Record);

            Assert.That(result.Truncated, Is.EqualTo(false));
        }
    }
}
