using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public struct DnsQueryQuestion
    {
        public DnsName Name { get; private set; }
        public DnsRecordType RecordType { get; private set; }
        public DnsRecordClass RecordClass { get; set; }

        private byte[] Bytes { get; set; }

        public DnsQueryQuestion(string name, DnsRecordType recordType)
            : this()
        {
            Name = new DnsName(name);
            RecordType = recordType;
        }

        public DnsQueryQuestion(byte[] packetContent, int startingOffset, int questionLength)
            : this()
        {
            Name = new DnsName(packetContent, startingOffset);
            Bytes = null;
            RecordType = (DnsRecordType)BitConverter.ToInt16(new byte[] { packetContent[startingOffset + questionLength - 3], packetContent[startingOffset + questionLength - 4] }, 0);
            RecordClass = (DnsRecordClass)BitConverter.ToInt16(new byte[] { packetContent[startingOffset + questionLength - 1], packetContent[startingOffset + questionLength - 2] }, 0);
        }

        public byte[] ToByteArray()
        {
            if (Bytes == null)
            {
                var result = new List<byte>();

                result.AddRange(Name.ToByteArray());

                result.AddRange(BitConverter.GetBytes((short)RecordType).Reverse().ToArray()); // type
                result.AddRange(new byte[] { 0, 1 }); // class

                Bytes = result.ToArray();
            }

            return Bytes;
        }
    }
}
