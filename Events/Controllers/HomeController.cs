using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Events.Models;
using Events.Services;
using Events.Models.Entities;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService eventService;
        private readonly IEventTypeService eventTypeService;
        private readonly IEventSubTypeService eventSubTypeService;

        public HomeController(IEventService eventService, IEventTypeService eventTypeService, IEventSubTypeService eventSubTypeService)
        {
            this.eventService = eventService;
            this.eventTypeService = eventTypeService;
            this.eventSubTypeService = eventSubTypeService;
        }

        public IActionResult Index()
        {
            var model = new EventsViewModel();
            List<Event> events = eventService.GetAllEvents();
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();

            if (events.Count() > 3)
            {
                events = events.OrderBy(x => x.DateTime).Take<Event>(3).ToList();
            }

            model.EventsList = events;
            model.EventTypes = eventTypes;
            model.EventSubTypes = eventSubTypes;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
