using MyNotes.Data;

namespace MyNotesApi
{
    public class WebAppSettings
    {
        public string CorsAllowedHosts { get; set;}

        public Neo4jSettings Neo4jSettings { get; set; }
    }    
}