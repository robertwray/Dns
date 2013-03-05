using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public sealed class DnsQueryAnswerNS : DnsQueryAnswer
    {
        public DnsName NsName { get; set; }
        public DnsQueryAnswerNS(IDnsNameParser parser, short recordDataLength, int recordDataOffset, byte[] packetContent)
        {
            NsName = parser.GetNameAtOffset(recordDataOffset);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", base.ToString(), NsName.ToString());
        }
    }
}
