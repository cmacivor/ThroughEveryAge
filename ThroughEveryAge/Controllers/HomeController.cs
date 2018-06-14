using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
                date = new DateTime(2018, 06, 13),
                badge = true,
                title = "one title",
                body = "<p>woo some text</p>",
                footer = "the footer text",
                classname = "purple-event"
            });
            //calendarEvents.Add(new CalendarEvent
            //{
            //    date = new DateTime(2018, 06, 10),
            //    badge = true,
            //    title = "one title 2",
            //    body = "<p>woo some textsdfasdf</p>",
            //    footer = "the footer text",
            //    classname = "purple-event"
            //});

            return Json(calendarEvents);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
