using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        // 必須存在session中
        public string GetLoginToken(string email, string password)
        {
            var dbEmail = _memberRep?.GetMail(email);
            string hashedPassword = ComputeSHA256Hash(password);
            var dbPassword = _memberRep?.GetPassWord(hashedPassword);

            if (dbEmail == null)
            {
                return "無此Email";
            }

            if (dbPassword != password)
            {
                return "密碼錯誤";
            }

            var token = GenerateJwtToken(email);
            var sessionUserId = _memberRep?.GetUserId(dbEmail, dbPassword);

            if (sessionUserId != null)
            {
                _httpContextAccessor.HttpContext?.Session.SetString("SessionUserId", sessionUserId);
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

        [HttpPost]
        //註冊會員
        public string Register(string _Mail, string _Pwd)
        {
            if(!string.IsNullOrEmpty(_Mail) && !string.IsNullOrEmpty(_Pwd))
            {
                string hashedPassword = ComputeSHA256Hash(_Pwd);
                return _memberRep!.CreateNewUser(_Mail, hashedPassword);
            }
            else
            {
                return "Register Fail";
            }
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
    }
}
