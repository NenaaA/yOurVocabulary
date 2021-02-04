using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class StoryWord
    {
        [Key, Column(Order = 0)]
        public int StoryId { get; set; }
        [Key, Column(Order = 1)]
        public int WordId { get; set; }

        public virtual Story Story { get; set; }
        public virtual Word Word { get; set; }
    }
}