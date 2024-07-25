using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Dynamic;
using Microsoft.Data.SqlClient;
using System.Data;
using TravelNotesV2.Models;

namespace TravelNotesV2.Repositories.Common
{
    public class CommonRep
    {
        private readonly string _connectString;

        public CommonRep(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("TravelConnstring");
            if (string.IsNullOrEmpty(_connectString) )
            {
                throw new Exception("_connectString IS NULL");
            }
        }

        //取得使用者資訊
        public List<Object> GetUserInfo<T>(int UserId, string tableName) where T : class
        {
            var sql = @$"SELECT * From {tableName} WHERE UserId = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", UserId);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault(sql, parameters);
                return result!;
            }
        }


        

        
    }
}
