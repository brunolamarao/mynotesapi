using System.Collections.Generic;

namespace MyNotes.Models.Ports
{
    public interface INotesRepository
    {
        IEnumerable<Note> GetNotes();
    }
}