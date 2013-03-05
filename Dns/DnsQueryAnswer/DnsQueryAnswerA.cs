using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public sealed class DnsQueryAnswerA : DnsQueryAnswer
    {
        public IPAddress IPAddress { get; set; }
        public DnsQueryAnswerA(IDnsNameParser parser, short recordDataLength, int recordDataOffset, byte[] packetContent)
        {
            IPAddress = new IPAddress(packetContent.Skip(recordDataOffset).Take(recordDataLength).ToArray());
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}",base.ToString(), IPAddress.ToString());
        }
    }
}
