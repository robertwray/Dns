using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public sealed class DnsQueryAnswerMx : DnsQueryAnswer
    {
        public int Preference { get; set; }
        public DnsName MxName { get; set; }
        public DnsQueryAnswerMx(DnsNameParser parser, short recordDataLength, int recordDataOffset, byte[] packetContent)
        {
            Preference = BitConverter.ToInt16(packetContent.Skip(recordDataOffset).Take(2).ToArray(), 0);
            MxName = new DnsName(parser, packetContent, recordDataOffset + 2);
        }
    }
}
