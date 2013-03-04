using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public interface IDnsService
    {
        IEnumerable<IPAddress> DnsServers { get; }
        DnsQuery GetDnsEntries(string host, DnsRecordType recordType);
    }
}
