using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public struct DnsName
    {
        public string Host { get; set; }
        private byte[] Bytes { get; set; }

        public DnsName(string host)
            : this()
        {
            Host = host;
        }

        public override string ToString()
        {
            return Host;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetContent"></param>
        /// <param name="startingOffset">The starting offset of the name, the value of the length of the first name part</param>
        public DnsName(byte[] packetContent, int startingOffset)
            : this()
        {
            var host = new StringBuilder();
            var numberOfCharactersInCurrentNameSection = Convert.ToInt32(packetContent[startingOffset]);
            var i = startingOffset + 1;
            while (packetContent[i] != 0 && i < packetContent.Length - 1)
            {
                var twoBytes = packetContent.Skip(i).Take(2);
                
                var twoBytesAsBits = new BitArray(twoBytes.Reverse().ToArray());

                if (twoBytesAsBits[14] && twoBytesAsBits[15])
                {
                    // We have a "pointer"
                    var name = new DnsName(packetContent, twoBytes.Last());
                    if (host.Length != 0)
                    {
                        host.Append(".");
                    }
                    host.Append(name);
                    break;
                }

                if (numberOfCharactersInCurrentNameSection > 0)
                {
                    host.Append(Convert.ToChar(packetContent[i]));
                    numberOfCharactersInCurrentNameSection--;
                }
                else
                {
                    if (packetContent[i] == 0)
                    {
                        break;
                    }
                    else
                    {
                        host.Append(".");
                        numberOfCharactersInCurrentNameSection = Convert.ToInt32(packetContent[i]);
                    }
                }
                i++;
            }
            Host = host.ToString();
            Bytes = packetContent;
        }

        public DnsName(byte[] hostContent, byte[] packetContent)
            : this()
        {
            var host = new StringBuilder();
            var i = 1;
            var numberOfCharacters = Convert.ToInt32(hostContent[0]);
            while (hostContent[i] != 0 && i < hostContent.Length - 1)
            {
                var twoBytes = hostContent.Skip(i).Take(2);
                var twoBytesAsBits = new BitArray(twoBytes.Reverse().ToArray());

                if (twoBytesAsBits[14] && twoBytesAsBits[15])
                {
                    // We have a "pointer"
                    var name = new DnsName(packetContent, twoBytes.Last());
                    if (host.Length != 0)
                    {
                        host.Append(".");
                    }
                    host.Append(name);
                    break;
                }
                else
                {
                    if (numberOfCharacters > 0)
                    {
                        host.Append(Convert.ToChar(hostContent[i]));
                        numberOfCharacters--;
                    }
                    else
                    {
                        if (hostContent[i] == 0)
                        {
                            break;
                        }
                        else
                        {
                            host.Append(".");
                            numberOfCharacters = Convert.ToInt32(hostContent[i]);
                        }
                    }
                    i++;
                }
            }
            Host = host.ToString();
            Bytes = hostContent;
        }

        public byte[] ToByteArray()
        {
            if (Bytes == null)
            {
                var result = new List<byte>();

                var name = Host.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var namePart in name)
                {
                    result.AddRange(new byte[] { Convert.ToByte(namePart.Length) });
                    result.AddRange(System.Text.Encoding.ASCII.GetBytes(namePart));
                }

                result.AddRange(new byte[] { 0 }); // name terminator
                Bytes = result.ToArray();
            }

            return Bytes;
        }
    }
}
