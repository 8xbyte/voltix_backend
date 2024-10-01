using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Voltix.Auth.Interfaces;

namespace Voltix.Auth.Services
{
    public interface IJwtTokenService
    {
        public string GenerateToken(ITokenPayload payload);
        public ITokenPayload? VerifyToken(string token);
    }
    
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly JwtHeader _jwtHeader;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(credentials);
        }
        
        public string GenerateToken(ITokenPayload payload) {
            var jwtPayload = new JwtPayload() {
                { "userId", Convert.ToInt32(payload.UserId) },
                { "exp", DateTime.UtcNow.AddSeconds(36000).Ticks },
            };
            var jwtToken = new JwtSecurityToken(_jwtHeader, jwtPayload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwtToken);
            return token;
        }

        public ITokenPayload? VerifyToken(string token) {
            try {
                _jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters() {
                    IssuerSigningKey = _securityKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                }, out SecurityToken securityToken);

                var jwtSecurityToken = securityToken as JwtSecurityToken;
                return new() {
                    UserId = Convert.ToInt32(jwtSecurityToken!.Payload["userId"]),
                };
            } catch {
                return null;
            }
        }
    }
}

