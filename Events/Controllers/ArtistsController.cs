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
using System.Net;

namespace Events.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IWebHostEnvironment _environment;

        public ArtistsController(IArtistService artistService, IWebHostEnvironment environment)
        {
            this.artistService = artistService;
            _environment = environment;
        }

        // GET: Artists
        public IActionResult Index()
        {
            List<Artist> artists = artistService.GetAllArtists();
            if (!artists.Any())
            {
                return NotFound();
            }
            return View(artists);
        }

        public IActionResult Search(string name)
        {
            List<Artist> artists = artistService.GetArtistsByName(name);
            if (!artists.Any())
            {
                return NotFound();
            }
            return View("Index", artists);
        }

        // GET: Events/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create([FromForm]Artist model)
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
                            model.PhotoPath = pathDb;
                        }
                    }
                }
            }

            artistService.CreateArtist(model.Name, model.Description, model.PhotoPath);

            return Redirect(Url.Action("Index", "Artists"));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Guid Id)
        {
            if(Id == null)
            {
                return NotFound();
            }

            var artist = artistService.GetArtistById(Id);
            if(artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditSave([FromForm] Artist artist)
        {
            if(artist == null)
            {
                return NotFound();
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
                            artist.PhotoPath = pathDb;
                        }
                    }
                }
            }
            artistService.Update(artist);
            TempData["Success"] = "Artist eddited successfully!";
            return Redirect(Url.Action("EditList", "Artists"));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var artist = artistService.GetArtistById(id);
            if(artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            artistService.Delete(Id);
            TempData["Success"] = "Artist deleted successfully!";
            return Redirect(Url.Action("EditList", "Artists"));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditList()
        {
            var artists = artistService.GetAllArtists();
            if (!artists.Any())
            {
                return NotFound();
            }

            return View(artists);
        }
    }
}
