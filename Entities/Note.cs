using System;

namespace Propellerhead_Andy.Entities
{
    public record Note
    {
        public Guid Id { get; init; }
        public string Details { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CustomerId { get; set; }

    }
}