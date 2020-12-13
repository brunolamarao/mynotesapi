using System;
using System.Collections.Generic;
using MyNotes.Models;
using MyNotes.Models.Ports;
using Neo4j.Driver;

namespace MyNotes.Data
{      
    public class NotesRepository : INotesRepository
    {
        private readonly IDriver _neo4jDriver;
        
        public NotesRepository(IDriver driver)        
        {
            _neo4jDriver = driver;
        }

        public IEnumerable<Note> GetNotes()
        {
            var notesResult = new List<Note>();

            var statementTemplate = "MATCH (note:Note) RETURN note.title, note.content LIMIT 10";
            //var statementParameters = new Dictionary<string, object> {{"title", title}};

            using (var session = _neo4jDriver.Session())
            {
                var statementResult = session.Run(statementTemplate);

                var notes = statementResult.As<List<object>>();                
                
                foreach (IList<object> noteResult in notes)
                {
                    var castResult = new Note
                    {
                        Title = noteResult[0].As<string>(),
                        Content = noteResult[1].As<string>(),                       
                    };
                    notesResult.Add(castResult);
                }                
            }
            return notesResult;       
        }
    }
}
