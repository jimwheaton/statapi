using System;

namespace Models
{
    public class DataStaging : Entity
    {
        public DateTime Date { get; set; }
        public string Site { get; set; }
        public string Keyword { get; set; }
        public string Market { get; set; }
        public string Device { get; set; }   
        public int Google { get; set; }
        public int GoogleBaseRank { get; set; }
        public int Yahoo { get; set; }
        public int Bing { get; set; }
        public string GoogleUrl { get; set; }
        public string YahooUrl { get; set; }
        public string BingUrl { get; set; }
        public decimal AdvertiserCompetition { get; set; }
        public int GlobalMonthlySearches { get; set; }
        public int RegionalMonthlySearches { get; set; }
        public decimal CPC { get; set; }
        public string Tags { get; set; }
    }
}
