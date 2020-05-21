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
using Microsoft.AspNetCore.Identity;
using Events.Areas.Identity.Data;
using Events.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Events.Controllers
{
    [Authorize(Roles = "User")]
    public class OrdersController : Controller
    {
        private readonly ICartService cartService;
        private readonly UserManager<EventsUser> _userManager;
        private readonly IOrderService orderService;
        private readonly ITicketService ticketService;
        private readonly IEventService eventService;

        public OrdersController(ICartService cartService, UserManager<EventsUser> userManager, IOrderService orderService, ITicketService ticketService, IEventService eventService)
        {
            this.cartService = cartService;
            _userManager = userManager;
            this.orderService = orderService;
            this.ticketService = ticketService;
            this.eventService = eventService;
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = cartService.GetCartById(user.Id);
            if(cart == null)
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

            var order = orderService.Create(cart);
            return View("ThankYou", order);
        }

        public async Task<IActionResult> Index()
        {
            var model = new OrderViewModel();
            var user = await _userManager.GetUserAsync(User);
            List<Order> orders = orderService.GetUsersOrders(user.Id);

            if (!orders.Any())
            {
                string referer = Request.Headers["Referer"].ToString();
                TempData["Success"] = "You have no orders made.";
                return Redirect(referer);
            }

            model.User = user;
            model.Orders = orders;

            return View(model);
        }
    }
}
