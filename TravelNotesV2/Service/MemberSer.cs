using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelNotesV2.Repositories;

namespace TravelNotesV2.Service
{
    public class MemberSer
    {
        private readonly MemberRep? _memberRep;
        private readonly IConfiguration? _configuration;

        public MemberSer(MemberRep memberRep, IConfiguration configuration)
        {
            _memberRep = memberRep;
            _configuration = configuration;
        }

        public string Login(string email, string password)
        {
            var dbEmail = _memberRep!.GetMail(email);
            var dbPassword = _memberRep.GetPassWord(password);

            if (dbEmail == null)
            {
                throw new Exception("User does not exist");
            }

            if (dbPassword != password)
            {
                throw new Exception("Invalid password");
            }

            var token = GenerateJwtToken(email);
            return token;
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration?["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration!["Jwt:Issuer"],
                audience: _configuration!["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
