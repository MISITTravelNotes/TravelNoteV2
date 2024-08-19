using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using TravelNotesV2.Models;

namespace TravelNotesV2.Repositories
{
    public class AiRecommendRep : Controller
    {
        
        private readonly string _connectString;

        public AiRecommendRep(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("TravelConnstring");
            if (string.IsNullOrEmpty(_connectString))
            {
                throw new Exception("_connectString IS NULL");
            }
        }

        // GET: api/model_list
        [HttpGet]
        public async Task<IEnumerable<model_list>> GetAll()
        {
            var sql = @"SELECT * FROM model_list";
            using (var conn = new SqlConnection(_connectString))
            {
                var result = await conn.QueryAsync<model_list>(sql);
                return result.ToList();
            }
        }

        // GET: api/model_list/5
        [HttpGet("{modelId}")]
        public async Task<ActionResult<model_list>> GetFilterId(int modelId)
        {
            var sql = "@SELECT * FROM modelId = @modelId";
            var parameter = new SqlParameter("modelId", modelId);
            using(var conn = new SqlConnection(_connectString))
            {
                var result = await conn.QueryFirstOrDefaultAsync(sql);
                if (result == null) 
                { 
                    return NotFound();
                }
                return result;
            }
        }

        // POST: api/model_list
        [HttpPut("{modelId}")]
        public async Task<ActionResult<model_list>> UpdateModel(int modelId, model_list list)
        {
            var sql = @"UPDATE model_list 
                        SET modelName=@modelName
                        WHERE modelId=@modelId";
            var parameters = new DynamicParameters();
            parameters.Add("modelId", modelId);
            parameters.Add("modelName", list.modelName);
            using (var conn =new SqlConnection(_connectString))
            {
                var execute = await conn.ExecuteAsync(sql, parameters);

                if (execute > 0)
                {
                    return Ok(execute);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // POST: api/model_list
        [HttpPost]
        public async Task<ActionResult<model_list>> CreateModel(model_list list)
        {
            var sql = @"INSERT INTO model_list(modelName, useCount)
                        VALUES(@modelName, 0)";
            var parameters = new DynamicParameters();
            parameters.Add("modelName", list.modelName);
            using (var conn = new SqlConnection(_connectString))
            {
                var execute = await conn.ExecuteAsync(sql, parameters);

                if(execute > 0)
                {
                    return Ok(execute);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // DELETE: api/model_list/5
        [HttpDelete("{modelId}")]
        public async Task<IActionResult> DeleteModel(int modelId)
        {
            var sql = @"SELECT modelId FROM model_list WHERE modelId=@modelId";
            var sql2 = @"DELETE FROM model_list WHERE modelId=@modelId";
            var parameters = new DynamicParameters();
            parameters.Add("modelId", modelId);
            using (var conn = new SqlConnection(_connectString)) 
            {
                var query = conn.QueryFirstOrDefault(sql, parameters);
                if (query != null)
                {
                    var execute = await conn.ExecuteAsync(sql2, parameters);

                    if (execute > 0)
                    {
                        return Ok(execute);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return BadRequest("刪除失敗");
            }
        }


    }
}


