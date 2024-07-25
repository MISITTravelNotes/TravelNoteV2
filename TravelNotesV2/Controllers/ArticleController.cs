using Microsoft.AspNetCore.Mvc;

namespace TravelNotesV2.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
