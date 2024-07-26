using Microsoft.AspNetCore.Mvc;
using TravelNotesV2.Service;

namespace TravelNotesV2.WebApi
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemberController : Controller
    {
        private readonly MemberSer? _memberSer;
        public MemberController(MemberSer? memberSer)
        {
            _memberSer = memberSer;
        }

        public string GetLoginToken(string email, string password)
        {
            return _memberSer!.GetLoginToken(email, password);
        }

        public string Register(string email, string password)
        {
            return _memberSer!.Register(email, password);
        }
    }
}
