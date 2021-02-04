using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class ProfileWord
    {
        [Key, Column(Order = 0)]
        public int ProfileId { get; set; }
        [Key, Column(Order = 1)]
        public int WordId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Word Word { get; set; }


        //additional attributes to relation
        public DateTime? LastChecked { get; set; }
        public int CheckedCount { get; set; }
    }
}