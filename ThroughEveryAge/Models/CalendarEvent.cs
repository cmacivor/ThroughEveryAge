using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThroughEveryAge.Models
{
    public class CalendarEvent
    {
        public DateTime date { get; set; }

        public bool badge { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        public string footer { get; set; }

        public string classname { get; set; }
    }
}
