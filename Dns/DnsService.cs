using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public class DnsServices
    {
        private int transactionId = 0;
        private List<IPAddress> dnsServers;

        public DnsServices()
        {
            dnsServers = GetDnsServers();
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
        }

        public IEnumerable<IPAddress> DnsServers
        {
            get
            {
                return dnsServers;
            }
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            InvalidateDnsServers();
        }

        private void InvalidateDnsServers()
        {
            dnsServers = GetDnsServers();
        }

        public DnsQuery GetDnsEntries(string host, DnsRecordType recordType)
        {
            using (var client = new UdpClient())
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(DnsServers.First(), 53);
                client.Connect(RemoteIpEndPoint);
                var query = new DnsQuery(transactionId, host, recordType);
                transactionId++;
                var bytes = query.ToByteArray();
                client.Send(bytes, bytes.Length);
             
                // Get response
                Byte[] receiveBytes = client.Receive(ref RemoteIpEndPoint);

                var result = new DnsQuery(receiveBytes);

                return result;
            }
        }

        private List<IPAddress> GetDnsServers()
        {
            List<IPAddress> dnsServers = new List<IPAddress>();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var adapter in adapters)
            {
                dnsServers.AddRange(adapter.GetIPProperties().DnsAddresses.Select(x => x.MapToIPv4()));
            }

            dnsServers = new List<IPAddress>(dnsServers.Distinct());

            return dnsServers;
        }
    }
}
