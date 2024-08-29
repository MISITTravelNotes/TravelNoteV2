using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using TravelNotesV2.Models;

namespace TravelNotesV2.Repositories
{
    public class AiRecommendRep : ControllerBase
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
        [HttpGet("{id}")]
        public async Task<ActionResult<model_list>> Get(int id)
        {
            var sql = @"SELECT * FROM model_list WHERE modelId = @modelId";
            var parameters = new DynamicParameters();
            parameters.Add("modelId", id);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = await conn.QueryFirstOrDefaultAsync<model_list>(sql, parameters);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
        }


        // POST: api/model_list
        [HttpPut("{id}")]
        public async Task<ActionResult<model_list>> Update(int id, model_list list)
        {

            var sql = @"SELECT modelId FROM model_list WHERE modelId=@modelId";
            var sql2 = @"UPDATE model_list 
                        SET modelName=@modelName
                        WHERE modelId=@modelId";
            

            var parameters = new DynamicParameters();
            parameters.Add("modelId", id);
            parameters.Add("modelName", list.modelName);

            if(id != list.modelId)
            {
                return BadRequest();
            }

            using (var conn = new SqlConnection(_connectString))
            {
                var getId = await conn.QueryFirstOrDefaultAsync<model_list>(sql, parameters);
                if (getId != null) 
                {
                    var execute = await conn.ExecuteAsync(sql2, parameters);
                    if (execute <= 0)
                    {
                        return BadRequest();
                    }
                    return NoContent();
                }
                return BadRequest();
            }
        }

        // POST: api/model_list
        [HttpPost]
        public async Task<ActionResult<model_list>> Create(model_list list)
        {
            if(list.modelName == null)
            {
                return BadRequest();
            }
            else
            {
                var sql = @"INSERT INTO model_list(modelName, useCount)
                        VALUES(@modelName, 0)";
                var parameters = new DynamicParameters();
                parameters.Add("modelName", list.modelName);
                using (var conn = new SqlConnection(_connectString))
                {
                    var execute = await conn.ExecuteAsync(sql, parameters);

                    if (execute > 0)
                    {
                        return Ok("新增成功");
                    }
                    else
                    {
                        return BadRequest("新增失敗");
                    }
                }
            }
        }

        // DELETE: api/model_list/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sql = @"SELECT modelId FROM model_list WHERE modelId=@modelId";
            var sql2 = @"DELETE FROM model_list WHERE modelId=@modelId";
            var parameters = new DynamicParameters();
            parameters.Add("modelId", id);
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


