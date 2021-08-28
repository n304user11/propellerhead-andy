using System;
using Propellerhead_Andy.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Propellerhead_Andy.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetNoteAsync(Guid id);
        Task<IEnumerable<Note>> GetNotesByCustomerIdAsync(Guid customerId);
        Task CreateNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(Guid id);
    }
}