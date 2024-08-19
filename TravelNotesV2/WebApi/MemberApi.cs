using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TravelNotesV2.Service;

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

        public string GetLoginToken(string email, string password)
        {
            return _memberSer!.GetLoginToken(email, password);
        }

        public string Register(string email, string password)
        {
            return _memberSer!.Register(email, password);
        }

        public string Logout()
        {
            return _memberSer!.Logout();
        }

        public string ForgotPwd(string email)
        {
            return _memberSer!.ForgotPwd(email);
        }
    }
}
