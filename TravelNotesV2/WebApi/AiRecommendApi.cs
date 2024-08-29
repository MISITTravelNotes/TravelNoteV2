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
        public async Task<ActionResult> GetFilterId(int id)
        {
            var query = await _api.Get(id);
            return Ok(query.Result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateModel(int id, model_list list)
        {
            var query = await _api.Update(id, list);
            return Ok(query.Result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateModel(model_list list)
        {
            var query = await _api.Create(list);
            return Ok(query.Result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteModel(int modelId)
        {
            var query = await _api.Delete(modelId);
            return Ok(query);
        }
    }
}

//https://localhost:7293/api/AiRecommendApi/GetAllModel