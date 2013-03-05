using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
namespace Dns.Test.DnsNameTests
{
    [TestFixture]
    public sealed class ToByteArray
    {
        private readonly static byte[] host_dnsname = new byte[] { 4, 104, 111, 115, 116, 0 };
        private readonly static byte[] host_dot_com_dnsname = new byte[] { 4, 104, 111, 115, 116, 3, 99, 111, 109, 0 };
        [Test]
        public void Terminates_With_Null_Byte_On_Valid_Name()
        {
            var name = new DnsName("test.local");
            var result = name.ToByteArray();

            Assert.That(result.Last(), Is.EqualTo((byte)0));
        }

        [Test]
        public void Returns_Correctly_For_Single_Label_Name()
        {
            var name = new DnsName("host");
            var result = name.ToByteArray();

            Assert.That(result.Length, Is.EqualTo(6));
            for (int i = 0; i < host_dnsname.Length; i++)
            {
                Assert.That(result[i], Is.EqualTo(host_dnsname[i]));
            }
        }

        [Test]
        public void Returns_Correctly_For_Two_Label_Name()
        {
            var name = new DnsName("host.com");
            var result = name.ToByteArray();

            Assert.That(result.Length, Is.EqualTo(10));
            for (int i = 0; i < host_dot_com_dnsname.Length; i++)
            {
                Assert.That(result[i], Is.EqualTo(host_dot_com_dnsname[i]));
            }
        }
    }
}
