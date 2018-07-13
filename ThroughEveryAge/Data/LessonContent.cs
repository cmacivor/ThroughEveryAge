using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThroughEveryAge.Data
{
    public class LessonContent
    {
        public int LessonContentId { get; set; }

        public DateTime Date { get; set; }

        public Guid FileId { get; set; }

        public int LessonType { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }
    }
}
