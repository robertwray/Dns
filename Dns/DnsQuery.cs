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
            var dnsQueryAnswerFactory = new DnsQueryAnswerFactory();
            var startOfAnswer = startOfQuestion - 1;
            for (int i = 0; i < Header.AnswerCount; i++)
            {
                var answer = dnsQueryAnswerFactory.GetDnsQueryAnswer(packetContent, ref startOfAnswer);
                answers.Add(answer);
            }
            Answers = answers;
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
                    question = new DnsQueryQuestion(packetContent, startOfQuestion, (i + 5) - startOfQuestion);
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