using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.Application.Services;

namespace User.Infrastructure.Services.TokenGeneration
{
    public sealed class TokenGenerationService : ITokenGenerationService
    {
        private readonly TokenSettings _tokenSettings;

        public TokenGenerationService(IOptions<TokenSettings> tokenSettings) =>
            _tokenSettings = tokenSettings.Value;
        
        public string GenerateToken(Guid ownerId)
        {
            var secretKey = Encoding.ASCII.GetBytes(_tokenSettings.SecurityKey);
            var validityTimeInMinutes = Convert.ToDouble(_tokenSettings.ValidityTimeInMinutes);
            var expirationTime = DateTime.UtcNow.AddMinutes(validityTimeInMinutes);
            var claims = new List<Claim>
            {
                new (Constants.ClaimTypes.OwnerId, ownerId.ToString())
            };
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
