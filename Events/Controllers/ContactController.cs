using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Events.Areas.Identity.Data;
using Events.Models;
using Events.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Events.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private UserManager<EventsUser> _userManager;

        public ContactController(ILogger<ContactController> logger, UserManager<EventsUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            bool isUser = currentUser.IsInRole("User");
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }

        public IActionResult SendMessage(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(model.Email);
                    msz.To.Add("simon.test.gabriel@gmail.com");
                    msz.Subject = model.Subject + " - " + model.Email;
                    msz.Body = model.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("simon.test.gabriel@gmail.com", "Test12345Test6789");
                    smtp.EnableSsl = true;

                    smtp.Send(msz);
                    ModelState.Clear();
                    TempData["Success"] = "Message sent successfully!";
                }
                catch(Exception ex)
                {
                    ModelState.Clear();
                    TempData["Success"] = $" Sorry we are facing Problem here {ex.Message}";
                }
            }
            return Redirect(Url.Action("Index", "Contact"));
        }
    }
}