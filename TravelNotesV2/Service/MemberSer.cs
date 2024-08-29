using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TravelNotesV2.Models;
using TravelNotesV2.Repositories;

namespace TravelNotesV2.Service
{
    public class MemberSer
    {
        private readonly MemberRep? _memberRep;
        private readonly IConfiguration? _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberSer(MemberRep memberRep, 
                        IConfiguration configuration, 
                        JwtSettings jwtSettings,
                        IHttpContextAccessor httpContextAccessor)
        {
            _memberRep = memberRep;
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        private string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }


        // 必須存在session中
        [HttpPost]
        public string GetLoginToken(string email, string password)
        {
            var dbEmail = _memberRep?.GetMail(email);
            if (dbEmail == "Mail not found")
            {
                return "無此Email";
            }

            string hashedPassword = ComputeSHA256Hash(password);
            var dbPassword = _memberRep?.GetPassWord(hashedPassword);

            

            if (dbPassword != hashedPassword)
            {
                return "密碼錯誤";
            }

            

            var token = GenerateJwtToken(email);
            var sessionUserId = _memberRep!.GetUserId(dbEmail, dbPassword);
            var sessionUserRole = _memberRep!.CheckSuperUser(email);
            var sessionUserRoleHeadShot = _memberRep!.GetHeadshot(email);
            var sessionPassword = _memberRep.GetPassWord(email);


            if (sessionUserId != null)
            {
                _httpContextAccessor.HttpContext?.Session.SetString("SessionUserId", sessionUserId);
                _httpContextAccessor.HttpContext?.Session.SetString("SessionUserRole", sessionUserRole);
                _httpContextAccessor.HttpContext?.Session.SetString("SessionUserHeadShot", sessionUserRoleHeadShot);
                _httpContextAccessor.HttpContext?.Session.SetString("SessionUserPassword", sessionPassword);
            }

            return token;
        }

        [HttpPost]
        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration?["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration!["JwtSettings:Issuer"],
                audience: _configuration!["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //登出
        [HttpPost]
        public string Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("SessionUserId");
            return "登出成功";
        }

        [HttpPost]
        //註冊會員
        public string Register([FromBody] users user)
        {
            if(!string.IsNullOrEmpty(user.Mail) && !string.IsNullOrEmpty(user.Pwd))
            {
                string hashedPassword = ComputeSHA256Hash(user.Pwd);
                return _memberRep!.CreateNewUser(user.Mail, hashedPassword);
            }
            else
            {
                return "註冊失敗";
            }
        }

        //忘記密碼
        [HttpPost]
        public string ForgotPwd(string Mail)
        {
            var tempPwd = "";
            string mail = _memberRep!.GetMail(Mail);
            if (mail != null)
            {
                string randomString = GenerateRandomString(8);
                tempPwd += randomString;
                _memberRep.ChangePwd(Mail, tempPwd);


                MailMessage msg = new MailMessage();
                msg.To.Add(Mail);
                msg.From = new MailAddress("travelnotes9802@gmail.com",
                    "TravelNotes.org",
                    Encoding.UTF8);

                /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
                msg.Subject = "旅行筆記-忘記密碼";//郵件標題
                msg.SubjectEncoding = Encoding.UTF8;//郵件標題編碼
                msg.Body = $"這是您暫時的密碼 : {tempPwd}"; //郵件內容
                msg.BodyEncoding = Encoding.UTF8;//郵件內容編碼 
                                                 //msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
                msg.IsBodyHtml = true;//是否是HTML郵件 
                                      //msg.Priority = MailPriority.High;//郵件優先級 

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("Aa0977706956@gmail.com",
                    "etyymhpjhwfxfxym"); //這裡要填正確的帳號跟密碼
                client.Host = "smtp.gmail.com"; //設定smtp Server
                client.Port = 25; //設定Port
                client.EnableSsl = true; //gmail預設開啟驗證
                client.Send(msg); //寄出信件
                client.Dispose();
                msg.Dispose();

                return "請確認郵件";
            }
            else
            {
                return "寄送失敗";
            }            
        }

    }
}
