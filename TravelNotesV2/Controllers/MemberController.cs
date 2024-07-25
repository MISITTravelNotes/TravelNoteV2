using Microsoft.AspNetCore.Mvc;

namespace TravelNotesV2.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
