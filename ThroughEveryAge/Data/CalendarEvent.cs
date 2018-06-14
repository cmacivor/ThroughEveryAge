using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThroughEveryAge.Data
{
    public class Event
    {
        public int EventId { get; set; }

        public DateTime Date { get; set; }

        public bool Badge { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Footer { get; set; }

        public string ClassName { get; set; }
    }
}
