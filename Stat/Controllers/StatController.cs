using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models.Services;
using System.IO;
using Data;

namespace Stat.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StatController : Controller
    {
        private readonly ICsvService _csvService;
        private readonly StatContext _statContext;

        public StatController(ICsvService csvService, StatContext statContext)
        {
            _csvService = csvService;
            _statContext = statContext;
        }

        [HttpGet]
        public JsonResult Lookups()
        {
            var lookups = new
            {
                keywords = _statContext.Keywords.Select(k => k.Phrase).ToArray(),
                sites = _statContext.Sites.Select(s => s.Name).ToArray(),
                dates = _statContext.Dates.OrderBy(d => d.DateTime).Select(d => d.DateTime.ToString("yyyy-MM-dd")).ToArray(),
                markets = _statContext.Markets.Select(m => m.Name).ToArray(),
                devices = _statContext.Devices.Select(d => d.Name).ToArray()
            };

            return Json(lookups);
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                return Ok(await _csvService.Import(reader));
            }
        }
    }
}
