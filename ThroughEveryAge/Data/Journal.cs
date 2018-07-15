using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThroughEveryAge.Data
{
    public class Journal
    {
        public int JournalId { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string JournalEntry { get; set; }

        public DateTime Date { get; set; }
    }
}
