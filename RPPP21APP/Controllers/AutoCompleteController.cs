using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using RPPP21APP.Data;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
