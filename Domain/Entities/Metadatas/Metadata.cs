using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Metadatas
{
    public class Metadata
    {
        //public Guid MetadataId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;
    }
}
