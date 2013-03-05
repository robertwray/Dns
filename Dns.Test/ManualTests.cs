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
        public void Query_Google_Co_Uk_For_NS_Records()
        {   
            var dnsService = new DnsService();
            
            var queryResult = dnsService.GetDnsEntries("test.cocktail.local", DnsRecordType.A);
            Console.WriteLine();
            Console.Write("new byte[] { ");
            for (int i = 0; i < queryResult.PacketContent.Length; i++)
            {
                Console.Write("{0}, ", queryResult.PacketContent[i]);
            }
            Console.Write(" } ");
        }
    }
}
