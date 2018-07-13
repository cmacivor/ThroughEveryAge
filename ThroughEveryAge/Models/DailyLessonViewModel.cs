using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThroughEveryAge.Models
{
    public class DailyLessonViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        public Guid FileId { get; set; }

        [Required]
        public string LessonType { get; set; }

        //[Required]
        public IFormFile File { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        public List<string> LessonTypes { get; set; }

        public List<SelectListItem> LessonTypeSelectListItems { get; set; }
    }

    enum LessonType {
        Reading = 1,
        Reflection,
        Video,
        Journal
    }
}
