using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public enum DnsRecordClass : short
    {
        IN = 1,
        CS =2,
        CH = 3,
        HS = 4,
        STAR = 55
    }
}
