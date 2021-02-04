using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class Language
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}