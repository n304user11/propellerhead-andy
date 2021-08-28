using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Propellerhead_Andy.Dtos
{
    public record UpdateNoteDto
    {
        [Required]
        public string Details { get; set; }

    }
}