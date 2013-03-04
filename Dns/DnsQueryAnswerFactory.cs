using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public sealed class DnsQueryAnswerFactory
    {
        public IDnsQueryAnswer GetDnsQueryAnswer(byte[] packetContent, ref int startOfAnswer)
        {
            IDnsQueryAnswer answer = null;
            var twoBytes = packetContent.Skip(startOfAnswer).Take(2);
            var twoBytesAsBits = new BitArray(twoBytes.Reverse().ToArray());
            if (twoBytesAsBits[14] && twoBytesAsBits[15])
            {
                // We have a "pointer"
                var name = new DnsName(packetContent, twoBytes.Last());
                var recordType = (DnsRecordType)BitConverter.ToInt16(packetContent.Skip(startOfAnswer + 2).Take(2).Reverse().ToArray(), 0);
                var recordClass = BitConverter.ToInt16(packetContent.Skip(startOfAnswer + 4).Take(2).Reverse().ToArray(), 0);
                var ttl = BitConverter.ToUInt32(packetContent.Skip(startOfAnswer + 6).Take(4).Reverse().ToArray(), 0);
                var rDataLength = BitConverter.ToInt16(packetContent.Skip(startOfAnswer + 10).Take(2).Reverse().ToArray(), 0);
                //var rData = packetContent.Skip(startOfAnswer + 12).Take(rDataLength).ToArray();

                var recordDataOffset = startOfAnswer + 12;

                answer = GetDnsQueryAnswer(name, recordType, recordClass, ttl, rDataLength, recordDataOffset, packetContent);

                startOfAnswer = startOfAnswer + 12 + rDataLength;
            }
            return answer;
        }

        private IDnsQueryAnswer GetDnsQueryAnswer(DnsName name, DnsRecordType recordType, short recordClass, UInt32 ttl, short recordDataLength, int recordDataOffset, byte[] packetContent)
        {
            var answer = GetDnsQueryAnswer(recordType, recordDataLength, null, packetContent, recordDataOffset);
            answer.Name = name;
            answer.RecordType = recordType;
            answer.RecordClass = recordClass;
            answer.Ttl = ttl;
            answer.RecordDataLength = recordDataLength;
            //answer.RecordData = recordData;

            return answer;
        }

        //public IDnsQueryAnswer GetDnsQueryAnswer(DnsName name, DnsRecordType recordType, short recordClass, UInt32 ttl, short recordDataLength, byte[] recordData, byte[] packetContent)
        //{
        //    var answer = GetDnsQueryAnswer(recordType, recordDataLength, recordData, packetContent);
        //    answer.Name = name;
        //    answer.RecordType = recordType;
        //    answer.RecordClass = recordClass;
        //    answer.Ttl = ttl;
        //    answer.RecordDataLength = recordDataLength;
        //    answer.RecordData = recordData;

        //    return answer;
        //}

        private IDnsQueryAnswer GetDnsQueryAnswer(DnsRecordType recordType, short recordDataLength, byte[] recordData, byte[] packetContent, int recordDataOffset)
        {
            switch (recordType)
            {
                case DnsRecordType.A:
                    return new DnsQueryAnswerA(recordDataLength, recordDataOffset, packetContent);
                case DnsRecordType.CNAME:
                    return new DnsQueryAnswerCName(recordDataLength, recordDataOffset, packetContent);
                case DnsRecordType.MX:
                    return new DnsQueryAnswerMx(recordDataLength, recordDataOffset, packetContent);
                case DnsRecordType.NS:
                    return new DnsQueryAnswerNS(recordDataLength, recordDataOffset, packetContent);
                case DnsRecordType.AAAA:
                case DnsRecordType.SRV:
                default:
                    return new DnsQueryAnswerGeneric();
            }
        }
    }
}
