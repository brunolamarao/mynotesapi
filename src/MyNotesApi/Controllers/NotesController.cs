using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Models;
using MyNotes.Models.Ports;

namespace MyNotesApi.Controllers
{
    [Route("[controller]")]
    [EnableCors("MyNotesApplication")]
    [ApiController]
    public class NotesController : Controller
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
           return _notesRepository.GetNotes();
        }
    }
}