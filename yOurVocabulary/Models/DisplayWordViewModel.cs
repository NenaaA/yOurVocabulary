using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yOurVocabulary.Models
{
    public class DisplayWordViewModel
    {
        public string WordName { get; set; }
        public string LastChecked { get; set; }
        public int CheckedCount { get; set; }
        public string Language { get; set; }
    }
}