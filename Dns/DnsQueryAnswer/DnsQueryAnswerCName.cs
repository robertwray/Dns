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

        public DnsQueryAnswerCName(IDnsNameParser parser, short recordDataLength, int recordDataOffset, byte[] packetContent)
        {
            CName = parser.GetNameAtOffset(recordDataOffset);
        }
    }
}
