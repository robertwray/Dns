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
        public DnsQueryAnswerA(short recordDataLength, byte[] recordData)
        {
            IPAddress = new IPAddress(recordData);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}",base.ToString(), IPAddress.ToString());
        }
    }
}
