using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarShop.Services;

public interface ITokenService {
    JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
}