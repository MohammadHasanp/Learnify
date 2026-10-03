using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserModule.Core.Queries.Users.DTOs;

namespace Learnify.Web.Infrastructure.JwtUtil
{
    public class JwtTokenBuilder
    {
        public static string BuildToken(UserDto user, IConfiguration configuration)
        {
            var roles = user.Roles.Select(s => s.Title);

            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.MobilePhone,user.Mobile),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Role,string.Join("-",roles))
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtConfig:Issuer"],
                audience: configuration["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
