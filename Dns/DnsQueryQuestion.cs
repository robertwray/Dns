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
        private byte[] Bytes { get; set; }

        public DnsQueryQuestion(string name, DnsRecordType recordType)
            : this()
        {
            Name = new DnsName(name);
            RecordType = recordType;
        }

        public DnsQueryQuestion(byte[] packetContent, int startingOffset)
        {
            throw new NotImplementedException();
        }

        public DnsQueryQuestion(byte[] packetContent)
            : this()
        {
            Name = new DnsName(packetContent.Take(packetContent.Length - 4).ToArray(), packetContent);
            Bytes = packetContent;
            RecordType = (DnsRecordType)BitConverter.ToInt16(packetContent.Skip(packetContent.Length - 4).Take(2).Reverse().ToArray(), 0);
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
