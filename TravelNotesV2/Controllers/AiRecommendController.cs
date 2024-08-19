using Microsoft.AspNetCore.Mvc;

namespace TravelNotesV2.Controllers
{
    public class AiRecommendController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
