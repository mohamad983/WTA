using Domain.Common;
using Domain.Entities.Metadatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tickets
{
    public class Ticket : BaseEntity
    {
        //public Guid TicketId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        private readonly List<Metadata> _metadata = [];
        public IReadOnlyCollection<Metadata> Metadata => _metadata.AsReadOnly();
    }
}
