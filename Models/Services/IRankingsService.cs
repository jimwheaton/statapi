using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Services
{
    public interface IRankingsService
    {
        IEnumerable<RankingResult> GetRankings(
            string site,
            string market,
            string device,
            string keyword,
            DateTime? start,
            DateTime? end,
            bool weighted);
    }
}
