using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Services
{
    public class RankingResult
    {
        public string Site { get; set; }
        public string Device { get; set; }
        public string Market { get; set; }
        public string Keyword { get; set; }
        public DateTime Date { get; set; }
        public double Bing { get; set; }
        public double Google { get; set; }
        public double GoogleBaseRank { get; set; }
        public double Yahoo { get; set; }
    }

    public class ChartRankingResult
    {
        public string SearchEngine { get; set; }
        public double Rank { get; set; }
    }
}
