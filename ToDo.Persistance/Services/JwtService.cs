using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Persistance.Identity;

namespace ToDo.Persistance.Services
{
    public class JwtService(IOptions<AuthSettings> options)
    {
        public string GenerateJwtToken(ApplicationUser user)
        {
            //Собираем claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Генерируем симметричный ключ и креденшалы
            var symmetricKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.SecretKey));

            var creds = new SigningCredentials(
                symmetricKey, SecurityAlgorithms.HmacSha256);

            //Создаём сам токен
            var jwt = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                signingCredentials: creds
            );

            //Превращаем в строку
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
