﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns
{
    /// <summary>
    /// Describes a query request to, or response from, a DNS server
    /// </summary>
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
            var parser = new DnsNameParser(packetContent);
            Header = new DnsQueryHeader(packetContent);
            
            var questions = new List<DnsQueryQuestion>();

            var startOfQuestion = 12;
            for (int i = 0; i < Header.QuestionCount; i++)
            {
                var question = ExtractQuestion(parser, packetContent, ref startOfQuestion);
                questions.Add(question);
            }
            Questions = questions;

            var answers = new List<IDnsQueryAnswer>();
            var dnsQueryAnswerFactory = new DnsQueryAnswerFactory();
            var startOfAnswer = startOfQuestion - 1;
            for (int i = 0; i < Header.AnswerCount; i++)
            {
                var answer = dnsQueryAnswerFactory.GetDnsQueryAnswer(parser, packetContent, ref startOfAnswer);
                answers.Add(answer);
            }
            Answers = answers;
        }


        private DnsQueryQuestion ExtractQuestion(IDnsNameParser parser, byte[] packetContent, ref int startOfQuestion)
        {
            DnsQueryQuestion? question = null;
            for (int i = startOfQuestion; i < packetContent.Length; i++)
            {
                // We've found the null that terminates
                if (packetContent[i] == 0)
                {
                    var endOfQuestion = i + 5;
                    question = new DnsQueryQuestion(parser, packetContent, startOfQuestion, (i + 5) - startOfQuestion);
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
        public override string ToString()
        {
            return this.Answers != null ? this.Answers.Count().ToString() : "0";
        }
    }
}