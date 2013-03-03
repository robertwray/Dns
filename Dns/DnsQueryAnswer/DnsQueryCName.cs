using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public sealed class DnsQueryAnswerCName : DnsQueryAnswer
    {
        public DnsName CName { get; set; }

        public DnsQueryAnswerCName(short recordDataLength, byte[] recordData, byte[] packetContent)
        {
            CName = new DnsName(recordData, packetContent);
        }
    }
}
