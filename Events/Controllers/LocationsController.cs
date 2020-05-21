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

namespace Events.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        // GET: Locations
        public IActionResult Index()
        {
            var locations = locationService.GetAllNonPopularLocations();
            return View(locations);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromForm] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            locationService.CreateLocation(location);

            return Redirect(Url.Action("AllLocations", "Locations"));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AllLocations()
        {
            var locations = locationService.GetAllLocations();
            if(!locations.Any() || locations == null)
            {
                return NotFound();
            }

            return View(locations);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Locations/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = locationService.GetLocationById(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Locations/Delete/5
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            locationService.Delete(id);
            TempData["Success"] = "Location deleted successfully!";
            return Redirect(Url.Action("AllLocations", "Locations"));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var location = locationService.GetLocationById(Id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditSave([FromForm] Location location)
        {
            if(location == null)
            {
                return NotFound();
            }

            locationService.Update(location);
            TempData["Success"] = "Location eddited successfully!";
            return Redirect(Url.Action("AllLocations", "Locations"));
        }
    }
}
