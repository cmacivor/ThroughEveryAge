using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThroughEveryAge.Data;
using ThroughEveryAge.Models;

namespace ThroughEveryAge.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(CalendarEventViewModel calendarEventViewModel)
        {
            var calendarEvent = new Data.Event
            {
               Date = Convert.ToDateTime(calendarEventViewModel.Date),
               Badge = calendarEventViewModel.Badge,
               Body = calendarEventViewModel.Body,
               ClassName = calendarEventViewModel.Classname,
               Footer = calendarEventViewModel.Footer,
               Title = calendarEventViewModel.Title
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                context.Events.Add(calendarEvent);
                context.SaveChanges();
            }

                return RedirectToAction("Index");
        }
    }
}