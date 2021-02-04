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
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public virtual ApplicationUser ProfileUser { get; set; }
        public virtual ICollection<ProfileStory> ProfileStories { get; set; }
        public virtual ICollection<ProfileWord> ProfileWords { get; set; }
        //public Profile()
        //{
        //    ProfileStories = new List<ProfileStory>();
        //    ProfileWords = new List<ProfileWord>();
        //}
    }
}