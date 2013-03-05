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
    public interface IDnsNameParser
    {
        /// <summary>
        /// The content of the packet that names are extracted from
        /// </summary>
        byte[] PacketContent { get; }

        /// <summary>
        /// Retrieve a name that is found at a given offset, processing through any "pointers" present in the packet
        /// </summary>
        /// <param name="offset">The offset at which the name begins (should contain the length of the first name part)</param>
        /// <returns></returns>
        DnsName GetNameAtOffset(int offset);
    }
}
