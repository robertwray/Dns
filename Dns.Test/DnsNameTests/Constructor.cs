using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns.Test.DnsNameTests
{
    [TestFixture]
    public sealed class Constructor
    {
        public readonly static byte[] ultraasp_dot_net_A_Record = new byte[] { 0, 0, 129, 128, 0, 1, 0, 1, 0, 0, 0, 0, 8, 117, 108, 116, 114, 97, 97, 115, 112, 3, 110, 101, 116, 0, 0, 1, 0, 1, 192, 12, 0, 1, 0, 1, 0, 0, 111, 69, 0, 4, 91, 209, 187, 19 };

        [Test]
        public void Extracts_Name_Correctly_From_Well_Formed_Question_In_Dns_Response()
        {
            var result = new DnsName(ultraasp_dot_net_A_Record, 12);
            Assert.That(result.Host, Is.EqualTo("ultraasp.net"));
        }
    }
}
