using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Keyword : Entity
    {
        public string Phrase { get; set; }
        public int GlobalMonthlySearches { get; set; }
    }
}
