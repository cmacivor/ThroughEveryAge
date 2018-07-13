using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThroughEveryAge.Models
{
    public class LessonViewModel
    {
        public List<DailyLessonViewModel> DailyLessons { get; set; }

        public int LessonContentId { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        public string FileName { get; set; }

        //[Required]
        public string LessonType { get; set; }

        public int LessonTypeId { get; set; }

        //[Required]
        public IFormFile File { get; set; }

        //[Required]
        public string Description { get; set; }

        //[Required]
        public string Title { get; set; }
    }
}
