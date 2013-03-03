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
    public class Class1
    {
        [Test]
        public void hell()
        {
            var d = new DnsServices();
            
            var queryResult = d.GetDnsEntries("google.co.uk", DnsRecordType.NS);

        }
        [Test]
        public void fneh2()
        {
            BitArray f = new BitArray(new bool[] { true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false });
            byte[] g = new Byte[3];
            f.CopyTo(g, 0);
        }
    }
}
