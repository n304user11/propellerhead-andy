using System;
using System.Collections.Generic;

namespace Propellerhead_Andy.Entities
{
    public record Customer
    {
        public Guid Id { get; init; }
        public Status Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }

    }
}