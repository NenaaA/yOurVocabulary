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
        public int Id { get; set; }
        public String WordName { get; set; }
        public int CheckedCount { get; set; }
        public List<DateTime> CheckedLog { get; set; }
    }
}