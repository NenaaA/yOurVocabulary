using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class Story
    {
        [Key]
        public int StoryId { get; set; }
        public String Author { get; set; }
        [Required]
        public String Title { get; set; }
        [Display (Name ="Year Written")]
        public int? YearWritten { get; set; }
        [Required]
        public virtual Language Language { get; set; }
        public float? Rating { get; set; }
        [Display(Name = "Image Link")]
        public String ImageURL { get; set; }
        [Required]
        [Display(Name = "The Story")]
        public String TheStory { get; set; }
        public virtual ICollection<StoryWord> StoryWords { get; set; }
        public virtual ICollection<ProfileStory> ProfileStories { get; set; }
    }
}