using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dns.Test.DnsQueryTests
{
    [TestFixture]
    public sealed class Constructor
    {
        private readonly static byte[] ultraasp_dot_net_A_Record = new byte[] { 0, 0, 129, 128, 0, 1, 0, 1, 0, 0, 0, 0, 8, 117, 108, 116, 114, 97, 97, 115, 112, 3, 110, 101, 116, 0, 0, 1, 0, 1, 192, 12, 0, 1, 0, 1, 0, 0, 111, 69, 0, 4, 91, 209, 187, 19 };
        private readonly static byte[] test_dot_cocktail_dot_local_A_Record = new byte[] { 0, 1, 133, 128, 0, 1, 0, 2, 0, 0, 0, 0, 4, 116, 101, 115, 116, 8, 99, 111, 99, 107, 116, 97, 105, 108, 5, 108, 111, 99, 97, 108, 0, 0, 1, 0, 1, 192, 12, 0, 5, 0, 1, 0, 0, 14, 16, 0, 9, 6, 118, 111, 105, 112, 45, 49, 192, 17, 192, 49, 0, 1, 0, 1, 0, 0, 14, 16, 0, 4, 10, 1, 1, 231 };

        [Test]
        public void Extracts_TransactionId_Correctly_From_Well_Formed_Question()
        {
            var dnsQuery = new DnsQuery(test_dot_cocktail_dot_local_A_Record);

            Assert.That(dnsQuery.Header.TransactionId, Is.EqualTo(1));
        }
    }
}
