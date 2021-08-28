using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Propellerhead_Andy.Dtos;
using Propellerhead_Andy.Entities;
using Propellerhead_Andy.Repositories;

namespace Propellerhead_Andy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly INoteRepository noteRepository;

        public NoteController(ICustomerRepository customerRepository, INoteRepository noteRepository)
        {
            this.customerRepository = customerRepository;
            this.noteRepository = noteRepository;
        }

        [HttpGet]
        public async Task<NoteDto> GetNoteAsync(Guid id)
        {
            var note = (await noteRepository.GetNoteAsync(id)).AsDto();
            return note;
        }
        // POST /note
        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNoteAsync(CreateNoteDto noteDto)
        {
            var existingCustomer = await customerRepository.GetCustomerAsync(noteDto.CustomerId);

            if (existingCustomer is null)
            {
                return NotFound("Note can only add to existing customer");
            }

            Note note = new()
            {
                Id = Guid.NewGuid(),
                Details = noteDto.Details,
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                CustomerId = noteDto.CustomerId
            };

            await noteRepository.CreateNoteAsync(note);

            return CreatedAtAction(nameof(GetNoteAsync), new { id = note.Id }, note.AsDto());

        }

        // PUT /note/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNoteAsync(Guid id, UpdateNoteDto noteDto)
        {
            var existingNote = await noteRepository.GetNoteAsync(id);

            if (existingNote is null)
            {
                return NotFound();
            }

            Note updatedNote = existingNote with
            {
                Details = noteDto.Details,
                UpdatedDate = DateTimeOffset.UtcNow
            };

            await noteRepository.UpdateNoteAsync(updatedNote);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNoteAsync(Guid id)
        {
            var existingNote = await noteRepository.GetNoteAsync(id);

            if (existingNote is null)
            {
                return NotFound();
            }

            await noteRepository.DeleteNoteAsync(id);

            return NoContent();
        }
    }
}