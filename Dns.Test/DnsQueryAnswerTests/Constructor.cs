using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns.Test.DnsQueryAnswerTests
{
    [TestFixture]
    public sealed class Constructor
    {
        private readonly static byte[] test_dot_cocktail_dot_local_A_Record = new byte[] { 0, 0, 133, 128, 0, 1, 0, 2, 0, 0, 0, 0, 4, 116, 101, 115, 116, 8, 99, 111, 99, 107, 116, 97, 105, 108, 5, 108, 111, 99, 97, 108, 0, 0, 1, 0, 1, 192, 12, 0, 5, 0, 1, 0, 0, 14, 16, 0, 9, 6, 118, 111, 105, 112, 45, 49, 192, 17, 192, 49, 0, 1, 0, 1, 0, 0, 14, 16, 0, 4, 10, 1, 1, 231 };

        [Test]
        public void Extract_Answer_Correctly_From_Well_Formed_Answer_In_Dns_Response()
        {
            //var answerFactory = new DnsQueryAnswerFactory();
            //var answer = answerFactory.GetDnsQueryAnswer(
        }
    }
}
