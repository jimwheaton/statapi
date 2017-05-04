using CsvHelper.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public sealed class DataStagingMap : CsvClassMap<DataStaging>
    {
        public DataStagingMap()
        {
            Map(m => m.Date);
            Map(m => m.Site);
            Map(m => m.Keyword);
            Map(m => m.Market);
            Map(m => m.Device);   
            Map(m => m.Google).Name("Google").Default(0);
            Map(m => m.GoogleBaseRank).Name("Google Base Rank").Default(0);
            Map(m => m.Yahoo).Name("Yahoo").Default(0);
            Map(m => m.Bing).Name("Bing").Default(0);
            Map(m => m.GoogleUrl).Name("Google URL");
            Map(m => m.YahooUrl).Name("Yahoo URL");
            Map(m => m.BingUrl).Name("Bing URL");
            Map(m => m.AdvertiserCompetition).Name("Advertiser Competition").Default(0M);
            Map(m => m.GlobalMonthlySearches).Name("Global Monthly Searches").Default(0);
            Map(m => m.RegionalMonthlySearches).Name("Regional Monthly Searches").Default(0);
            Map(m => m.CPC).Default(0M);
            Map(m => m.Tags);
        }
    }
}
