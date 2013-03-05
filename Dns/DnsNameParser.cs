using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    /// <summary>
    /// Holds onto and returns previosuly parsed DnsName entries from a packet
    /// </summary>
    /// <remarks>
    /// Reduces the number of times, potentially, that a given packet needs to be parsed to extract a DnsName where
    /// there are "pointer" references to previous names in the packet
    /// </remarks>
    public sealed class DnsNameParser
    {
        public byte[] PacketContent { get; private set; }
        public Dictionary<int, DnsName> Names { get; private set; }

        public DnsNameParser(byte[] packetContent)
        {
            PacketContent = packetContent;
            Names = new Dictionary<int, DnsName>();
        }

        public DnsName GetNameAtOffset(int offset)
        {
            if (!Names.ContainsKey(offset))
            {
                var name = new DnsName(this, PacketContent, offset);
                Names.Add(offset, name);
            }

            return Names[offset];
        }
    }
}
