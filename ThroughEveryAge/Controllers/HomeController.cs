using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThroughEveryAge.Data;
using ThroughEveryAge.Models;

namespace ThroughEveryAge.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult Calendar()
        {
            
            return View();
        }

        public JsonResult GetCalendarData(int id)
        {
            var calendarEvents = new List<CalendarEvent>();
            calendarEvents.Add(new CalendarEvent
            {
                date = "2018-06-14", //new DateTime(2018, 06, 13).Date.ToString(),
                badge = true,
                title = "one title",
                body = "<p>woo some text</p>",
                footer = "the footer text",
                classname = "purple-event"
            });
            calendarEvents.Add(new CalendarEvent
            {
                date = "2018-06-12", //new DateTime(2018, 06, 10).Date.ToString(),
                badge = true,
                title = "one title 2",
                body = "<p>woo some textsdfasdf</p>",
                footer = "the footer text",
                classname = "purple-event"
            });

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
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
