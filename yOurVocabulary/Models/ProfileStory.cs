using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class ProfileStory
    {
        [Key, Column(Order = 0)]
        public int ProfileId { get; set; }
        [Key, Column(Order = 1)]
        public int StoryId { get; set; }


        public virtual Profile Profile { get; set; }
        public virtual Story Story { get; set; }

        //additional attributes to the relation
        public bool Read { get; set; }
        public float? Rating { get; set; }
    }
}