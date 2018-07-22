using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ThroughEveryAge.Data;
using ThroughEveryAge.Models;

namespace ThroughEveryAge.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class EventController : Controller
    {
        public IConfiguration Configuration { get; }

        public EventController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            var message = new CalendarEventMessageViewModel();

            if (TempData["CreateEvent"] != null)
            {
                message.Message = TempData["CreateEvent"].ToString();
            }

            return View(message);
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
            
            using (var context = new ApplicationDbContext(optionsBuilder.Options, Configuration))
            {
                context.Events.Add(calendarEvent);
                context.SaveChanges();
            }

            TempData["CreateEvent"] = "Event Successfully Created";

            return RedirectToAction("Index");
        }
    }
}