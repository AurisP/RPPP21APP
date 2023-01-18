using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RPPP21APP.Data;
using RPPP21APP.ViewModels;

namespace RPPP21APP.Controllers
{
    public class AutoCompleteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appData;

        public AutoCompleteController(ApplicationDbContext context, IOptionsSnapshot<AppSettings> options)
        {
            _context = context;
            _appData = options.Value;
        }
        public async Task<List<IdLabel>> Workers(string term)
        {
            var query = _context.Workers
                            .Select(c => new IdLabel
                            {
                                Id = c.WorkerId,
                                Label = c.Name + " " + c.Surname
                            })
                            .Where(i => i.Label.Contains(term));

            var list = await query.OrderBy(l => l.Label)
                                  .ThenBy(l => l.Id)
                                  .Take(_appData.AutoCompleteCount)
                                  .ToListAsync();
            return list;
        }
    }
}
