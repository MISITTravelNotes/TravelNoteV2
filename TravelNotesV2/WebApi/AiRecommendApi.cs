using Microsoft.AspNetCore.Mvc;
using TravelNotesV2.Models;
using TravelNotesV2.Repositories;

namespace TravelNotesV2.WebApi
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AiRecommendApiController : ControllerBase
    {
        private readonly AiRecommendRep _api;
        public AiRecommendApiController(AiRecommendRep api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> GetModels()
        {
            var query = await _api.GetAll();
            return Ok(query);
        }

        [HttpGet]
        public async Task<ActionResult> GetFilterId(int modelId)
        {
            var query = await _api.GetFilterId(modelId);
            if(query == null)
            {
                return NotFound("沒找到");
            }
            return Ok(query);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateModel(int modelId, model_list list)
        {
            var query = await _api.UpdateModel(modelId, list);
            if (query == null)
            {
                return BadRequest("更新失敗");
            }
            return Ok("更新成功");
        }

        [HttpPost]
        public async Task<ActionResult> CreateModel(model_list list)
        {
            var query = await _api.CreateModel(list);
            if(query == null)
            {
                return BadRequest("新增失敗");
            }
            return Ok("新增成功");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteModel(int modelId)
        {
            var query = await _api.DeleteModel(modelId);
            if (query == null)
            {
                return BadRequest("刪除失敗");
            }
            return Ok("刪除成功");
        }
    }
}

//https://localhost:7293/api/AiRecommendApi/GetAllModel