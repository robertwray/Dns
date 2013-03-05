using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dns.Test.DnsQueryHeaderTests
{
    [TestFixture]
    public sealed class Constructor
    {
        [Test]
        public void TransactionId_Is_Stored_Correctly()
        {
            var header = new DnsQueryHeader(1, 2, DnsQueryType.Query, true);

            Assert.That(header.TransactionId, Is.EqualTo(1));
        }

        [Test]
        public void QuestionCount_Is_Stored_Correctly()
        {
            var header = new DnsQueryHeader(1, 2, DnsQueryType.Query, true);

            Assert.That(header.QuestionCount, Is.EqualTo(2));
        }

        [Test]
        public void QueryType_Of_Query_Is_Stored_Correctly()
        {
            var header = new DnsQueryHeader(1, 2, DnsQueryType.Query, true);

            Assert.That(header.QueryType, Is.EqualTo(DnsQueryType.Query));
        }

        [Test]
        public void QueryType_Of_Response_Is_Stored_Correctly()
        {
            var header = new DnsQueryHeader(1, 2, DnsQueryType.Response, true);

            Assert.That(header.QueryType, Is.EqualTo(DnsQueryType.Response));
        }
    }
}
