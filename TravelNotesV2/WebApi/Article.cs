using Microsoft.AspNetCore.Mvc;
using TravelNotesV2.Models;
using TravelNotesV2.Repositories.Common;

namespace TravelNotesV2.WebApi
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ArticleController : ControllerBase
    {
        private readonly CommonRep _commonRepository;

        public ArticleController(CommonRep commonRepository)
        {
            _commonRepository = commonRepository;
        }

        // 抓使用資訊
        [HttpGet]
        public List<Object> GetUserInfo(int UserId, string tableName)
        {
            var result = _commonRepository.GetUserInfo<users>(UserId, tableName);
            return result;
        }

    }
}
