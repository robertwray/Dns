using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    public sealed class DnsQuery
    {
        public byte[] PacketContent { get; private set; }
        public DnsQueryHeader Header { get; set; }
        public IEnumerable<DnsQueryQuestion> Questions { get; private set; }
        public IEnumerable<IDnsQueryAnswer> Answers { get; private set; }
        public DnsQuery(int transactionId, string hostname, DnsRecordType recordType)
        {
            Header = new DnsQueryHeader(transactionId, 1, DnsQueryType.Query, true);
            Questions = new List<DnsQueryQuestion> { new DnsQueryQuestion(hostname, recordType) };
        }

        public DnsQuery(byte[] packetContent)
        {
            PacketContent = packetContent;
            var headerBytes = packetContent.Take(12).ToArray();
            Header = new DnsQueryHeader(headerBytes);

            var questions = new List<DnsQueryQuestion>();

            var startOfQuestion = 12;
            for (int i = 0; i < Header.QuestionCount; i++)
            {
                var question = ExtractQuestion(packetContent, ref startOfQuestion);
                questions.Add(question);
            }
            Questions = questions;

            var answers = new List<IDnsQueryAnswer>();
            var startOfAnswer = startOfQuestion - 1;
            for (int i = 0; i < Header.AnswerCount; i++)
            {
                var answer = ExtractAnswer(packetContent, ref startOfAnswer);
                answers.Add(answer);
            }
            Answers = answers;
        }

        private IDnsQueryAnswer ExtractAnswer(byte[] packetContent, ref int startOfAnswer)
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
                var rData = packetContent.Skip(startOfAnswer + 12).Take(rDataLength).ToArray();

                answer = new DnsQueryAnswerFactory().GetDnsQueryAnswer(name, recordType, recordClass, ttl, rDataLength, rData, packetContent);

                startOfAnswer = startOfAnswer + 12 + rDataLength;
            }
            return answer;
        }

        private DnsQueryQuestion ExtractQuestion(byte[] packetContent, ref int startOfQuestion)
        {
            DnsQueryQuestion? question = null;
            for (int i = startOfQuestion; i < packetContent.Length; i++)
            {
                // We've found the null that terminates the 
                if (packetContent[i] == 0)
                {
                    var endOfQuestion = i + 5;
                    question = new DnsQueryQuestion(packetContent.Skip(startOfQuestion).Take(endOfQuestion - startOfQuestion).ToArray());
                    startOfQuestion = endOfQuestion + 1;
                    break;
                }
            }
            return question.Value;
        }

        public byte[] ToByteArray()
        {
            var result = new List<byte>();
            result.AddRange(Header.ToByteArray());
            result.AddRange(Questions.First().ToByteArray());

            return result.ToArray();
        }
    }
}