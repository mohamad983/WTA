using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tickets.Args
{
    public class TicketArgs
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
