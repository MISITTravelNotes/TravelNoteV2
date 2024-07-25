using Dapper;
using Microsoft.Data.SqlClient;
using TravelNotesV2.Models;

namespace TravelNotesV2.Repositories
{
    public class MemberRep
    {
        private readonly string _connectString;

        public MemberRep(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("TravelConnstring");
            if (string.IsNullOrEmpty(_connectString))
            {
                throw new Exception("_connectString IS NULL");
            }
        }

        public string GetMail(string Mail)
        {
            var sql = @"SELECT Mail FROM users";
            var parameters = new DynamicParameters();
            parameters.Add("Mail", Mail);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault(sql, parameters);
                return result!;
            }
        }

        public string GetPassWord(string Pwd)
        {
            var sql = @"SELECT Pwd FROM users";
            var parameters = new DynamicParameters();
            parameters.Add("Pwd", Pwd);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault(sql, parameters);
                return result!;
            }
        }

        public string GetSuperUser()
        {
            var sql = @"SELECT UserId FROM users WHERE SuperUser = 'Y'";
            using (var conn = new SqlConnection(_connectString))
            {
                return conn.QueryFirstOrDefault(sql)!;
            }
        }

        public string GetHeadshot(int UserId)
        {
            var sql = @"SELECT Headshot FROM users WHERE UserId = @UserId";
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
