using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class Word
    {
        [Key]
        public int WordId { get; set; }
        public String Name { get; set; }
        public virtual ICollection<StoryWord> StoryWords { get; set; }
        public virtual ICollection<ProfileWord> ProfileWords { get; set; }
    }
}