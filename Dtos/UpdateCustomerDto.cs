using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Propellerhead_Andy.Dtos
{
    public record UpdateCustomerDto
    {
        [Required]
        [Range(1, 3)]
        public Status Status { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public List<NoteDto> Notes { get; set; }

    }
}