using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Areas.Identity.Data;
using Events.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Authorize(Roles = "User")]
    public class CartsController : Controller
    {
        private readonly UserManager<EventsUser> _userManager;
        private readonly ICartService cartService;
        private readonly IEventService eventService;
        private readonly ITicketService ticketService;

        public CartsController(UserManager<EventsUser> userManager, ICartService cartService, IEventService eventService, ITicketService ticketService)
        {
            _userManager = userManager;
            this.cartService = cartService;
            this.eventService = eventService;
            this.ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = cartService.GetCartById(user.Id);

            if (cart == null)
            {
                string referer = Request.Headers["Referer"].ToString();
                TempData["Success"] = "You have nothing in your cart.";
                return Redirect(referer);
            }
            var ticket = ticketService.GetTicketById(cart.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }
            var Ievent = eventService.GetEventById(ticket.EventId);
            if (Ievent == null)
            {
                return NotFound();
            }
            ticket.TicketEvent = Ievent;
            cart.CartTicket = ticket;
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(Guid id, [FromForm] int TicketsQuantity)
        {
            var user = await _userManager.GetUserAsync(User);
            var ticket = ticketService.GetTicketById(eventService.GetEventById(id).TicketId);
            var oldCart = cartService.GetCartById(user.Id);
            if (oldCart == null)
            {
                var cart = cartService.Create(user, ticket, TicketsQuantity);
                return Redirect(Url.Action("Index", "Carts"));
            }
            else
            {
                var cartTicket = ticketService.GetTicketById(oldCart.TicketId);
                if (cartTicket.Id == ticket.Id)
                {
                    cartService.Update(oldCart, TicketsQuantity);
                    return Redirect(Url.Action("Index", "Carts"));
                }
                TempData["Success"] = "You cannot add more than 1 event in the cart for now! This problem will be fixed in future!";
                return Redirect(Url.Action("Details", "Events", new { id = id }));
            }
        }

        public async Task<IActionResult> DeleteOne(int TicketsQuantity)
        {
            var user = await _userManager.GetUserAsync(User);
            var oldCart = cartService.GetCartById(user.Id);
            if (oldCart == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetTicketById(oldCart.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }
            var Ievent = eventService.GetEventById(ticket.EventId);
            if (Ievent == null)
            {
                return NotFound();
            }
            ticket.TicketEvent = Ievent;
            oldCart.CartTicket = ticket;

            var newCart = cartService.DeleteTickets(oldCart, TicketsQuantity);
            if (newCart == null)
            {
                TempData["Success"] = "You deleted all tickets from cart successfully!";
                return Redirect(Url.Action("Index", "Home"));
            }
            else
            {
                TempData["Success"] = "You deleted " + TicketsQuantity + " tickets from cart successfully!";
                return Redirect(Url.Action("Index", "Carts"));
            }
        }

        public async Task<IActionResult> AddOne()
        {
            var user = await _userManager.GetUserAsync(User);
            var oldCart = cartService.GetCartById(user.Id);
            if (oldCart == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetTicketById(oldCart.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }
            var Ievent = eventService.GetEventById(ticket.EventId);
            if (Ievent == null)
            {
                return NotFound();
            }
            ticket.TicketEvent = Ievent;
            oldCart.CartTicket = ticket;

            cartService.AddOne(oldCart);
            TempData["Success"] = "You added one more ticket successfully!";
            return Redirect(Url.Action("Index", "Carts"));
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = cartService.GetCartById(user.Id);
            if (cart == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetTicketById(cart.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }

            var Ievent = eventService.GetEventById(ticket.EventId);
            if (Ievent == null)
            {
                return NotFound();
            }
            ticket.TicketEvent = Ievent;
            cart.CartTicket = ticket;
            cart.User = user;

            return View(cart);
        }
    }
}