using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using Models.Services;
using System.Net.Http;
using System.Net;

namespace Stat.Controllers
{
    [Route("api")]
    public class RankingsController : Controller
    {
        private IRankingsService _rankingsService;

        public RankingsController(IRankingsService rankingsService)
        {
            _rankingsService = rankingsService;
        }

        [HttpGet]
        [Route("[controller]")]
        public JsonResult Get(
            string site,
            string market,
            string device,
            string keyword,
            DateTime? start,
            DateTime? end,
            bool weighted)
        {
            return Json(_rankingsService.GetRankings(site, market, device, keyword, start, end, weighted));
        }

        [HttpGet]
        [Route("[controller].csv")]
        [Produces("text/csv")]
        public IActionResult GetCsv(
            string site,
            string market,
            string device,
            string keyword,
            DateTime? start,
            DateTime? end,
            bool weighted)
        {
            return Ok(_rankingsService.GetRankings(site, market, device, keyword, start, end, weighted));
        }
    }
}
