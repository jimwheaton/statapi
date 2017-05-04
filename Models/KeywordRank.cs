using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class KeywordRank : Entity
    {
        //Relationships
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }  
        public Device Device { get; set; }
        public int DeviceId { get; set; }
        public int MarketId { get; set; }
        public Market Market { get; set; }
        public int DateId { get; set; }
        public Date Date { get; set; }

        //Props
        public int Google { get; set; }
        public int GoogleBaseRank { get; set; }
        public int Yahoo { get; set; }
        public int Bing { get; set; }
        public string GoogleUrl { get; set; }
        public string YahooUrl { get; set; }
        public string BingUrl { get; set; }
    }
}
