using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ThroughEveryAge.Data;
using ThroughEveryAge.Models;

namespace ThroughEveryAge.Controllers
{
    public class HomeController : Controller
    {

        private IHostingEnvironment _hostingEnvironment;

        public IConfiguration Configuration { get; }

        public HomeController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel viewModel)
        {

            return RedirectToAction("Index");
        }

        public IActionResult Calendar()
        {
            
            return View();
        }

        public IActionResult ShowDailyLesson(string fileId, int lessonId)
        {
            LessonViewModel dailyLesson = new LessonViewModel();
            //dailyLesson.DailyLessons = new List<DailyLessonViewModel>();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(optionsBuilder.Options, Configuration))
            {
                var lesson = context.LessonContents.FirstOrDefault(x => x.LessonContentId == lessonId);
                dailyLesson.Date = lesson.Date;
                dailyLesson.Description = lesson.Description;
                dailyLesson.LessonTypeId = lesson.LessonType;
                dailyLesson.Title = lesson.Title;
                dailyLesson.FileName = lesson.FileId;
            }

            return View(dailyLesson);
        }

        public IActionResult DailyLessons()
        {
            var lessons = new List<DailyLessonViewModel>();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(optionsBuilder.Options, Configuration))
            {
                var dailyLessons = context.LessonContents.ToList();
                foreach (var lesson in dailyLessons)
                {
                    var viewModel = new DailyLessonViewModel
                    {
                        LessonContentId = lesson.LessonContentId,
                        Date = lesson.Date,
                        Description = lesson.Description,
                        Title = lesson.Title,
                        LessonTypeId = lesson.LessonType,
                        FileName = lesson.FileId,
                        
                    };
                    lessons.Add(viewModel);
                }

            }

            var dlModel = new LessonViewModel { DailyLessons = lessons };
      
            return View(dlModel);
        }

        public IActionResult CreateLesson()
        {
            DailyLessonViewModel model = GetDailyLessonViewModel();

            model.Date = DateTime.Now;

            return View(model);
        }

        private static DailyLessonViewModel GetDailyLessonViewModel()
        {
            var model = new DailyLessonViewModel();

            var lessonTypes = Enum.GetNames(typeof(LessonType)).ToList();

            model.LessonTypes = lessonTypes;

            var selectListItems = new List<SelectListItem>();
            foreach (var type in lessonTypes)
            {
                selectListItems.Add(new SelectListItem
                {
                    Value = type,
                    Text = type
                });
            }

            model.LessonTypeSelectListItems = selectListItems;
            return model;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Member")]
        public async Task<IActionResult> CreateLesson(DailyLessonViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = GetDailyLessonViewModel();
                return View(model);
            }
            else
            {
                int lessonType = 0;
                if (viewModel.LessonType == "Reading")
                {
                    lessonType = 1;
                }
                if (viewModel.LessonType == "Reflection")
                {
                    lessonType = 2;
                }
                if (viewModel.LessonType == "Video")
                {
                    lessonType = 3;
                }
                if (viewModel.LessonType == "Journal")
                {
                    lessonType = 4;
                }
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                string fileid = string.Empty;
                if (viewModel.File != null)
                {
                    fileid = viewModel.File.FileName;
                }
                using (var context = new ApplicationDbContext(optionsBuilder.Options, Configuration))
                {
                    var lessonContent = new LessonContent
                    {
                        Date = viewModel.Date,
                        FileId = fileid,
                        Description = viewModel.Description,
                        LessonType = lessonType,
                        Title = viewModel.Title
                    };
                    context.LessonContents.Add(lessonContent);
                    context.SaveChanges();
                }

                if (!string.IsNullOrEmpty(fileid))
                {
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, viewModel.File.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.File.CopyToAsync(fileStream);
                    }
                }  
            }

            return RedirectToAction("DailyLessons");
        }

        [Authorize(Roles = "Admin,Manager,Member")]
        public IActionResult Curriculum()
        {
            return View();
        }

        public JsonResult GetSingleCalendarDate(string date)
        {
            var splitStrings = date.Split("_");

            var dateString = splitStrings.Where(x => x.Contains("-")).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            using (var context = new ApplicationDbContext(optionsBuilder.Options, Configuration))
            {
                var singleEvent = context.Events.FirstOrDefault(x => x.Date == Convert.ToDateTime(dateString));

                return Json(singleEvent);
            }
        }

        public JsonResult GetCalendarData(int id)
        {
            var calendarEvents = new List<CalendarEvent>();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            using (var context = new ApplicationDbContext(optionsBuilder.Options, Configuration))
            {
                var eventsFromDb = context.Events.ToList();

                foreach (var item in eventsFromDb)
                {
                    calendarEvents.Add(new CalendarEvent
                    {
                        date = item.Date.ToString(@"yyyy-MM-dd"),
                        badge = item.Badge,
                        body = "<p>" + item.Body + "</p>",
                        classname = item.ClassName,
                        footer = item.Footer,
                        title = item.Title
                    });
                }
            }


            return Json(calendarEvents);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
