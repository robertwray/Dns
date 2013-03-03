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
        public DnsQueryAnswerMx(short recordDataLength, byte[] recordData, byte[] packetContent)
        {
            Preference = BitConverter.ToInt16(recordData.Take(2).ToArray(), 0);
            MxName = new DnsName(recordData.Skip(2).ToArray(), packetContent);
        }
    }
}
