using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Models;

namespace MyNotesApi.Controllers
{
    [Route("[controller]")]
    [EnableCors("MyNotesApplication")]
    [ApiController]
    public class NotesController : Controller
    {
        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            return new List<Note> {
                new Note { 
                    Title = "First Note", 
                    Content="This is a hard-coded note on the API side.",
                    Tags = new List<Tag> {
                        new Tag { Name = "Introduction" },
                        new Tag { Name = "ASP.NET Core"}
                    }
                },
                new Note { 
                    Title = "Second Note", 
                    Content="The next step is to get those notes from the database.",
                    Tags = new List<Tag> {
                        new Tag { Name = "Graph Database"},
                        new Tag { Name = "Neo4j"}
                    }
                }
            };
        }
    }
}