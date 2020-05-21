﻿using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface ITicketService
    {
        Ticket Create(Ticket ticket);
        Ticket GetTicketById(Guid ticketId);
    }
}
