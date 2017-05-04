using Models.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data;
using Data.Extensions;

namespace Services
{
    public class RankingService : IRankingsService
    {
        private readonly StatContext _statContext;
        public RankingService(StatContext statContext)
        {
            _statContext = statContext;
        }

        public IEnumerable<RankingResult> GetRankings(
            string site,
            string market,
            string device,
            string keyword,
            DateTime? start,
            DateTime? end,
            bool weighted)
        {
            var spName = weighted ? "GetWeightedRankingsForKeyword" : "GetRankingsForKeyword";
            return _statContext.ExecuteStoredProcedure<RankingResult>(spName,
                new StoredProcedureParameter("Site", site),
                new StoredProcedureParameter("Market", market),
                new StoredProcedureParameter("Device", device),
                new StoredProcedureParameter("Phrase", keyword),
                new StoredProcedureParameter("Start", start.HasValue ? start.ToString() : null),
                new StoredProcedureParameter("End", end.HasValue ? end.ToString() : null));
        }
    }
}
