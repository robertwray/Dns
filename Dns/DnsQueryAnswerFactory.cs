using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public sealed class DnsQueryAnswerFactory
    {
        public IDnsQueryAnswer GetDnsQueryAnswer(DnsName name, DnsRecordType recordType, short recordClass, UInt32 ttl, short recordDataLength, byte[] recordData, byte[] packetContent)
        {
            var answer = GetDnsQueryAnswer(recordType, recordDataLength, recordData, packetContent);
            answer.Name = name;
            answer.RecordType = recordType;
            answer.RecordClass = recordClass;
            answer.Ttl = ttl;
            answer.RecordDataLength = recordDataLength;
            answer.RecordData = recordData;

            return answer;
        }

        private IDnsQueryAnswer GetDnsQueryAnswer(DnsRecordType recordType, short recordDataLength, byte[] recordData, byte[] packetContent)
        {
            switch (recordType)
            {
                case DnsRecordType.A:
                    return new DnsQueryAnswerA(recordDataLength, recordData);
                case DnsRecordType.CNAME:
                    return new DnsQueryAnswerCName(recordDataLength, recordData, packetContent);
                case DnsRecordType.MX:
                    return new DnsQueryAnswerMx(recordDataLength, recordData, packetContent);
                case DnsRecordType.NS:
                    return new DnsQueryAnswerNS(recordDataLength, recordData, packetContent);
                case DnsRecordType.AAAA:
                case DnsRecordType.SRV:
                default:
                    return new DnsQueryAnswerGeneric();
            }
        }
    }
}
