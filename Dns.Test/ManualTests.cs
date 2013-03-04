using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;

namespace Dns.Test
{
    [TestFixture]
    public class ManualTests
    {
        [Test]
        public void Query_Google_Co_Uk_For_NS_Records()
        {
            var dnsService = new DnsService();
            
            var queryResult = dnsService.GetDnsEntries("google.co.uk", DnsRecordType.NS);
        }
    }
}
