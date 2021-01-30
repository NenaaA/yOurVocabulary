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
        public int Id { get; set; }
        public String Author { get; set; }
        [Required]
        public String Title { get; set; }
        [Display (Name ="Year Written")]
        public int? YearWritten { get; set; }
        [Required]
        public String Language { get; set; }
        public float? Rating { get; set; }
        [Display(Name = "Image Link")]
        public String ImageURL { get; set; }
        [Required]
        [Display(Name = "The Story")]
        public String TheStory { get; set; }
        public virtual ICollection<Word> Words { get; set; }
        public Story()
        {
            Words = new List<Word>();
        }
    }
}