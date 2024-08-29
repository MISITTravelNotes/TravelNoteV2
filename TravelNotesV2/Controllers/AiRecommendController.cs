using Microsoft.AspNetCore.Mvc;

namespace TravelNotesV2.Controllers
{
    public class AiRecommendController : Controller
    {
        private readonly ILogger<AiRecommendController> _logger;

        public AiRecommendController(ILogger<AiRecommendController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
