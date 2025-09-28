using RepairShopAPI.Interfaces;
using RepairShopAPI.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace RepairShopAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _key;
        private readonly string _issuer;

        public TokenService(string key, string issuer)
        {
            _key = key;
            _issuer = issuer;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = [];
            if(string.Equals(user.Role.Name,""))
                claims.AddRange(
                [
                    new Claim(ClaimTypes.Name, user.Firstname),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("userId", user.Id.ToString())
                ]);
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //public string GenerateToken(Employee employee)
        //{
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name, employee.Username),
        //        new Claim(ClaimTypes.Role, employee.Role?? ""),
        //        new Claim("userId", employee.Id.ToString())
        //    };

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: _issuer,
        //        audience: _issuer,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddHours(6),
        //        signingCredentials: credentials
        //    );
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
