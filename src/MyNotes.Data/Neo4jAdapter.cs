using System;
using Neo4j.Driver;
using MyNotes.Models.Ports;
using SimpleInjector;

namespace MyNotes.Data
{
    public class Neo4jAdapter
    {
        private readonly Neo4jSettings _neo4jSettings;
    
        public Neo4jAdapter(Neo4jSettings settings)
        {
            _neo4jSettings = settings;
        }        

        public void Register(Container container)
        {
            var driver = GraphDatabase.Driver(_neo4jSettings.Url, AuthTokens.Basic(_neo4jSettings.UserName, _neo4jSettings.Password));
            container.Register<INotesRepository>(() => new NotesRepository(driver));
        }
    }
}
