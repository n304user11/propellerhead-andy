using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Propellerhead_Andy.Dtos
{
    public record CreateNoteDto
    {
        [Required]
        public string Details { get; set; }
        [Required]
        public Guid CustomerId { get; set; }

    }
}