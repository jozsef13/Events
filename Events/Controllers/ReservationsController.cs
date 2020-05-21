using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Data;
using Events.Models;
using Events.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Events.Services;
using Events.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Events.Controllers
{
    [Authorize(Roles = "User")]
    public class ReservationsController : Controller
    {
        private readonly UserManager<EventsUser> _userManager;
        private readonly ITicketService ticketService;
        private readonly IEventService eventService;
        private readonly IReservationService reservationService;

        public ReservationsController(UserManager<EventsUser> userManager, ITicketService ticketService, IEventService eventService, IReservationService reservationService)
        {
            _userManager = userManager;
            this.ticketService = ticketService;
            this.eventService = eventService;
            this.reservationService = reservationService;
        }

        public async Task<IActionResult> Reserve(Guid id, [FromForm] int TicketsQuantity)
        {
            var user = await _userManager.GetUserAsync(User);
            var ticket = ticketService.GetTicketById(eventService.GetEventById(id).TicketId);
            if(ticket == null)
            {
                return NotFound();
            }

            var reservation = reservationService.Create(user, ticket, TicketsQuantity);
            return View("ThankYou", reservation);
        }

        public async Task<IActionResult> Index()
        {
            var model = new ReservationViewModel();
            var user = await _userManager.GetUserAsync(User);
            List<Reservation> reservations = reservationService.GetUsersReservations(user.Id);

            if (!reservations.Any())
            {
                string referer = Request.Headers["Referer"].ToString();
                TempData["Success"] = "You have no reservations made.";
                return Redirect(referer);
            }

            model.User = user;
            model.Reservations = reservations;

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            Reservation reservation = reservationService.GetById(id, user);
            if(reservation == null)
            {
                return NotFound();
            }

            reservationService.Delete(reservation);
            TempData["Success"] = "You deleted the reservation successfully.";
            return LocalRedirect("/Identity/Account/Manage");
        }
    }
}
