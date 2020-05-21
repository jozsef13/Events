using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Data;
using Events.Models;
using Events.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Events.Models.Entities;

namespace Events.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly IEventTypeService eventTypeService;
        private readonly IEventSubTypeService eventSubTypeService;
        private readonly IArtistService artistService;
        private readonly ILocationService locationService;
        private readonly ITicketService ticketService;
        private readonly IWebHostEnvironment _environment;

        public EventsController(ITicketService ticketService,IWebHostEnvironment environment, IEventService eventService, IEventTypeService eventTypeService, IEventSubTypeService eventSubTypeService, IArtistService artistService, ILocationService locationService)
        {
            this.eventService = eventService;
            this.eventTypeService = eventTypeService;
            this.eventSubTypeService = eventSubTypeService;
            this.artistService = artistService;
            this.locationService = locationService;
            this.ticketService = ticketService;
            _environment = environment;
        }

        // GET: Events
        public ActionResult Index()
        {
            var model = new EventsViewModel();
            List<Event> events = eventService.GetAllEvents();
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();
            model.EventsList = events;
            model.EventTypes = eventTypes;
            model.EventSubTypes = eventSubTypes;
            return View("AdminIndex", model);
        }

        public IActionResult Search(string name)
        {
            var model = new EventsViewModel();
            List<Event> searchedEvent = eventService.GetEventByName(name);
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();
            model.EventsList = searchedEvent;
            model.EventTypes = eventTypes;
            model.EventSubTypes = eventSubTypes;
            return View("AdminIndex", model);
        }
        [HttpGet]
        public IActionResult SearchByType(Guid Id)
        {
            var model = new EventsViewModel();
            List<Event> events = eventService.GetEventsBySubType(Id);
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();
            model.EventsList = events;
            model.EventTypes = eventTypes;
            model.EventSubTypes = eventSubTypes;
            return View("AdminIndex", model);
        }

        //GET: Events/Details/5
        [HttpGet]
        [Authorize]
        public IActionResult Details(Guid id)
        {
            var model = new DetailsViewModel();
            var sEvent = eventService.GetEventById(id);
            if(sEvent.TicketId == null)
            {
                throw new Exception("Yes");
            }
            sEvent.Ticket = ticketService.GetTicketById(sEvent.TicketId);
            var events = eventService.GetEventsByLocation(sEvent.Location.City);
            foreach(Event iEvent in events)
            {
                if(iEvent == sEvent)
                {
                    events.Remove(iEvent);
                    break;
                }
            }
            model.DetailedEvent = sEvent;
            model.SimilarEvents = events;
            return View(model);
        }

        // GET: Events/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var model = new CreateEventViewModel();
            List<Artist> artists = artistService.GetAllArtists();
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();
            List<Location> locations = locationService.GetAllLocations();

            model.Artists = artists;
            model.EventSubTypes = eventSubTypes;
            model.EventTypes = eventTypes;
            model.Locations = locations;
            model.Event = new Event();
            
            return View(model);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromForm]CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newFileName = string.Empty;
            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string pathDb = string.Empty;

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        var fileExtension = Path.GetExtension(fileName);
                        newFileName = myUniqueFileName + fileExtension;
                        fileName = Path.Combine(_environment.WebRootPath, "img") + $@"\{newFileName}";
                        pathDb = "/img/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                            model.Event.CoverPhotoPath = pathDb;
                        }
                    }
                }
            }

            eventService.CreateEvent(model.Event.Name, model.Event.Description, model.Event.DateTime, model.Event.MaximumTickets, model.Event.CoverPhotoPath, model.Event.Location.City, model.Event.Location.Country, model.Event.SubType.Name, model.Event.Artist.Name, model.Ticket);

            return Redirect(Url.Action("Index", "Events"));
        }

        public IActionResult SearchEventByArtist(Guid Id)
        {
            var model = new EventsViewModel();
            List<Event> events = eventService.GetEventByArtist(Id);
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();
            model.EventsList = events;
            model.EventTypes = eventTypes;
            model.EventSubTypes = eventSubTypes;

            return View("AdminIndex", model);
        }

        public IActionResult SearchEventByLocation(string Name)
        {
            var model = new EventsViewModel();
            List<Event> events = eventService.GetEventsByLocation(Name);
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();
            model.EventsList = events;
            model.EventTypes = eventTypes;
            model.EventSubTypes = eventSubTypes;

            return View("AdminIndex", model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditList()
        {
            var events = eventService.GetAllEvents();
            return View(events);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Guid id)
        {
            var model = new CreateEventViewModel();
            List<Artist> artists = artistService.GetAllArtists();
            List<EventType> eventTypes = eventTypeService.GetAllTypes();
            List<EventSubType> eventSubTypes = eventSubTypeService.GetAllSubTypes();
            List<Location> locations = locationService.GetAllLocations();

            if (id == null)
            {
                return NotFound();
            }

            var iEvent = eventService.GetEventById(id);
            if(iEvent == null)
            {
                return NotFound();
            }

            model.Artists = artists;
            model.EventSubTypes = eventSubTypes;
            model.EventTypes = eventTypes;
            model.Locations = locations;
            model.Event = iEvent;

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditSave([FromForm] CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newFileName = string.Empty;
            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string pathDb = string.Empty;

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        var fileExtension = Path.GetExtension(fileName);
                        newFileName = myUniqueFileName + fileExtension;
                        fileName = Path.Combine(_environment.WebRootPath, "img") + $@"\{newFileName}";
                        pathDb = "/img/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                            model.Event.CoverPhotoPath = pathDb;
                        }
                    }
                }
            }

            eventService.UpdateEvent(model.Event.Id, model.Event.Name, model.Event.Description, model.Event.DateTime, model.Event.MaximumTickets, model.Event.CoverPhotoPath, model.Event.Location.City, model.Event.Location.Country, model.Event.SubType.Name, model.Event.Artist.Name, model.Ticket);
            TempData["Success"] = "Event eddited successfully!";
            return Redirect(Url.Action("EditList", "Events"));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var iEvent = eventService.GetEventById(id);
            if(iEvent == null)
            {
                return NotFound();
            }

            return View(iEvent);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            eventService.Delete(id);
            TempData["Success"] = "Event deleted successfully!";
            return Redirect(Url.Action("EditList", "Events"));
        }
    }
}
