using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class CreatorApplication
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}