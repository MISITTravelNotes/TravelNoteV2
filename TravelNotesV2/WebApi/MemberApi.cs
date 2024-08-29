using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TravelNotesV2.Service;
using TravelNotesV2.Models;

namespace TravelNotesV2.WebApi
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemberApi : Controller
    {
        private readonly MemberSer? _memberSer;
        public MemberApi(MemberSer? memberSer)
        {
            _memberSer = memberSer;
        }

        [HttpGet]
        public string GetLoginToken(string email, string password)
        {
            return _memberSer!.GetLoginToken(email, password);
        }

        [HttpPost]
        public string Register([FromBody] users user)
        {
            return _memberSer!.Register(user);
        }

        public string Logout()
        {
            return _memberSer!.Logout();
        }

        [HttpPost]
        public string ForgotPwd(string email)
        {
            return _memberSer!.ForgotPwd(email);
        }
    }
}
