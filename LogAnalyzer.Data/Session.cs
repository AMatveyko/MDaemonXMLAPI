using System;
using System.Collections.Generic;
using System.Drawing;

namespace LogAnalyzer.Data
{
    public class Session
    {
        public int Id { get; set; }
        public Proto Proto { get; set; }
        public Direct Direction { get; set; }
        public int Child { get; set; }
        public Boolean SSL { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string SrcAddr { get; set; }
        public string SrcPort { get; set; }
        public string DstAddr { get; set; }
        public string DstPort { get; set; }
        public String Log { get; set; }
        public string Status { get; set; }
        public long ByteIn { get; set; }
        public long ByteOut { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Subject { get; set; }

        //public Brush 
        //{
        //    get
        //    {

        //    }
        //}
    }
}
