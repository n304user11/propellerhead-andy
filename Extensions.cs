using System;
using Propellerhead_Andy.Dtos;
using Propellerhead_Andy.Entities;
using System.Collections.Generic;

namespace Propellerhead_Andy
{
    public static class Extensions
    {
        public static CustomerDto AsDto(this Customer customer, IEnumerable<NoteDto> notes = null)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Status = customer.Status,
                CreatedDate = customer.CreatedDate,
                Name = customer.Name,
                ContactNumber = customer.ContactNumber,
                Notes = notes
            };
        }

        public static NoteDto AsDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Details = note.Details,
                CreatedDate = note.CreatedDate,
                UpdatedDate = note.UpdatedDate,
                CustomerId = note.CustomerId
            };
        }
    }
}