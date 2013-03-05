using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    /// <summary>
    /// Describes the header of a DnsQuery
    /// </summary>
    public struct DnsQueryHeader
    {
        public int TransactionId { get; private set; }
        public int QuestionCount { get; private set; }
        public int AnswerCount { get; private set; }
        public int NameServerCount { get; set; }
        public int AdditionalRecordCount { get; set; }

        public DnsQueryType QueryType { get; private set; }

        public bool RecursionDesired { get; set; }
        public bool RecursionAvailable { get; set; }
        public bool Authoritative { get; set; }
        public bool Truncated { get; set; }
        public DnsQueryResponseCode ResponseCode { get; set; }

        private byte[] Bytes { get; set; }

        /// <summary>
        /// Construct a DnsQuery for transmission to a DNS Server
        /// </summary>
        /// <param name="transactionId">The Id of the transaction with the server</param>
        /// <param name="questionCount">The number of questions being asked in this query</param>
        /// <param name="queryType">The type of query being performed</param>
        /// <param name="recursionDesired">Is recursion desired?</param>
        public DnsQueryHeader(int transactionId, int questionCount, DnsQueryType queryType, bool recursionDesired)
            : this()
        {
            QueryType = queryType;
            TransactionId = transactionId;
            QuestionCount = questionCount;
            RecursionDesired = recursionDesired;
        }

        /// <summary>
        /// Constructs a DnsQuery from its byte representation, usually as a result of receiving a response from a DNS server
        /// </summary>
        /// <param name="packetContents">The bytes that make up the response</param>
        public DnsQueryHeader(byte[] packetContents)
            : this()
        {
            Bytes = packetContents;
            TransactionId = BitConverter.ToInt16(Bytes.Skip(0).Take(2).Reverse().ToArray(), 0);
            var flagBytes = Bytes.Skip(2).Take(2).Reverse().ToArray();
            var flagBits1 = new BitArray(flagBytes);

            QueryType = (DnsQueryType)Convert.ToInt32(flagBits1[15]);
            Authoritative = flagBits1[10];
            Truncated = flagBits1[9];
            RecursionDesired = flagBits1[8];
            RecursionAvailable = flagBits1[7];
            ResponseCode = GetDnsQueryResponseCode(flagBits1[3], flagBits1[2], flagBits1[1], flagBits1[0]);

            QuestionCount = BitConverter.ToInt16(Bytes.Skip(4).Take(2).Reverse().ToArray(), 0);
            AnswerCount = BitConverter.ToInt16(Bytes.Skip(6).Take(2).Reverse().ToArray(), 0);
            NameServerCount = BitConverter.ToInt16(Bytes.Skip(8).Take(2).Reverse().ToArray(), 0);
            AdditionalRecordCount = BitConverter.ToInt16(Bytes.Skip(10).Take(2).Reverse().ToArray(), 0);
        }

        private DnsQueryResponseCode GetDnsQueryResponseCode(bool p1, bool p2, bool p3, bool p4)
        {
            if (!p1 && !p2 && !p3 && !p4)
            {
                return DnsQueryResponseCode.NoError;
            }
            else
            {
                return DnsQueryResponseCode.NotImplemented;
            }
        }

        public byte[] ToByteArray()
        {
            if (Bytes == null)
            {
                var transactionIdBytes = BitConverter.GetBytes(Convert.ToInt16(TransactionId)).Reverse().ToArray();

                var flagBits1 = new BitArray(8);
                var flagBits2 = new BitArray(8);
                // Query Type
                flagBits1[7] = Convert.ToBoolean(QueryType);

                // Opcode
                flagBits1[6] = false;
                flagBits1[5] = false;
                flagBits1[4] = false;
                flagBits1[3] = false;

                // Authoritative Answer
                flagBits1[2] = false;

                // Truncated
                flagBits1[1] = false;

                // Recursion Desired
                flagBits1[0] = RecursionDesired;

                // Recursion Available
                flagBits2[7] = false;

                flagBits2[6] = false;
                flagBits2[5] = false;
                flagBits2[4] = false;

                flagBits2[3] = false;
                flagBits2[2] = false;
                flagBits2[1] = false;
                flagBits2[0] = false;

                var flagsBytes = new Byte[2];
                flagBits1.CopyTo(flagsBytes, 0);
                flagBits2.CopyTo(flagsBytes, 1);
                var questionsBytes = BitConverter.GetBytes(Convert.ToInt16(QuestionCount)).Reverse().ToArray();
                var answersBytes = new byte[] { 0, 0 };
                var authoritiesBytes = new byte[] { 0, 0 };
                var additionalsBytes = new byte[] { 0, 0 };

                var result = new List<byte>();
                result.AddRange(transactionIdBytes);
                result.AddRange(flagsBytes);
                result.AddRange(questionsBytes);
                result.AddRange(answersBytes);
                result.AddRange(authoritiesBytes);
                result.AddRange(additionalsBytes);
                Bytes = result.ToArray();
            }

            return Bytes;
        }
    }
}
