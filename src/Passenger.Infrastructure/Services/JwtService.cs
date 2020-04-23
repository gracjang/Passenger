using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services.Interfaces;
using Passenger.Infrastructure.Settings;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Passenger.Infrastructure.Services
{
  public class JwtService : IJwtService
  {
    private readonly JwtSettings _jwtSettings;

    public JwtService(JwtSettings jwtSettings)
    {
      _jwtSettings = jwtSettings;
    }

    public JwtDto CreateToken(string email, string role)
    {
      var securityTokenHandler = new JwtSecurityTokenHandler();
      var now = DateTime.UtcNow;
      var claims = new Claim[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(now).ToString(), ClaimValueTypes.Integer64),
      };

      var expires = now.AddMinutes(_jwtSettings.ExpiredMinutes);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        NotBefore = now,
        Subject = new ClaimsIdentity(claims),
        Expires = expires,
        Issuer = _jwtSettings.Issuer,
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
          SecurityAlgorithms.HmacSha256Signature)
      };
      var token = securityTokenHandler.CreateToken(tokenDescriptor);

      return new JwtDto()
      {
        Token = securityTokenHandler.WriteToken(token),
        Expiry = expires.ToFileTime(),
      };
    }
  }
}