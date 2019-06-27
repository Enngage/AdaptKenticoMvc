using System;
using System.Collections.Generic;
using Adapt.Model;

namespace Web.Models
{
    public class GenerateLogModel
    {
        public DateTime TimestampUTc { get; set; }
        public string CourseName { get; set; }
        public int Pages { get; set; }
        public int Articles { get; set; }
        public int Blocks { get; set; }
        public int Components { get; set; }
    }
}
