using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class DisplayStoryModel
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int? Year { get; set; }
        public string ImageURL { get; set; }
        public List<string> Words { get; set; }
    }
}