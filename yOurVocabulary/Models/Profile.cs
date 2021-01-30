using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public String ProfileName { get; set; }
        public List<Word> TranslatedWords { get; set; }
        public List<Story> ReadStories { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
        public Profile()
        {
            TranslatedWords = new List<Word>();
            ReadStories = new List<Story>();
            Stories = new List<Story>();
        }
    }
}