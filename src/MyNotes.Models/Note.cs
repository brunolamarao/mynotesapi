using System;
using System.Collections.Generic;

namespace MyNotes.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<Tag> Tags {get; set;}
        public bool Private { get; set; }
    }
}
