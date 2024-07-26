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
            var sql = @"SELECT Mail FROM users WHERE Mail = @Mail";
            var parameters = new DynamicParameters();
            parameters.Add("Mail", Mail);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<string>(sql, parameters);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return "Mail not found";
                }
            }
        }

        public string GetPassWord(string Pwd)
        {
            var sql = @"SELECT Pwd FROM users Where Pwd = @Pwd";
            var parameters = new DynamicParameters();
            parameters.Add("Pwd", Pwd);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<string>(sql, parameters);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return "Mail not found";
                }
            }
        }

        public string CheckSuperUser(string Mail)
        {
            var sql = @"SELECT SuperUser FROM users WHERE Mail=@Mail";
            var parameters = new DynamicParameters();
            parameters.Add("Mail", Mail);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<string>(sql, parameters);
                if (result != null)
                {
                    if (result == "Y")
                    {
                        return "Y";
                    }
                    else
                    {
                        return "N";
                    }
                }else{ return "User Not Found"; }
            }
        }

        public string GetHeadshot(int UserId)
        {
            var sql = @"SELECT Headshot FROM users WHERE UserId = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", UserId);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<string>(sql, parameters);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return "Mail not found";
                }
            }
        }

        public string GetUserId(string Mail, string Pwd)
        {
            var sql = @"SELECT UserId FROM users 
                        WHERE Mail = @Mail AND Pwd = @Pwd";
            var parameters = new DynamicParameters();
            parameters.Add("Mail", Mail);
            parameters.Add("Pwd", Pwd);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<string>(sql, parameters);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return "UserId not found";
                }
            }
        }

        //註冊
        public string CreateNewUser(string _Mail, string _Pwd)
        {
            string sql = @"SELECT Mail FROM users WHERE Mail = @Mail";
            string sql2 = @"INSERT INTO users (Mail, Pwd, SuperUser, Headshot) VALUES (@Mail, @Pwd, 'N', '/images/userImageDefault.jpg');";
            var parameters = new DynamicParameters();
            parameters.Add("Mail", _Mail);

            var parameters2 = new
            {
                Mail = _Mail,
                Pwd = _Pwd,
            };

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<string>(sql, parameters);
                if (result == null)
                {
                    conn.Execute(sql2, parameters2);
                    return "OK";
                }
                else
                {
                    return "Create Fail";
                }
            }
        }


    }
}
