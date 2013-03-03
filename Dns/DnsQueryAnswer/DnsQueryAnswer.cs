using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public abstract class DnsQueryAnswer
    {
        public DnsName Name { get; set; }
        public DnsRecordType RecordType { get; set; }
        public short RecordClass { get; set; }
        public UInt32 Ttl { get; set; }
        public short RecordDataLength { get; set; }
        public byte[] RecordData { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", RecordType.ToString(), Name.Host);
        }
    }
}
