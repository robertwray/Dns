using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public interface IDnsQueryAnswer
    {
        DnsName Name { get; set; }
        DnsRecordType RecordType { get; set; }
        DnsRecordClass RecordClass { get; set; }
        UInt32 Ttl { get; set; }
        short RecordDataLength { get; set; }
        byte[] RecordData { get; set; }
    }
}
