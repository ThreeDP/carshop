using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CarShop.DTO;
using CarShop.Migrations;
using CarShop.Models;
using CarShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;

namespace CarShop.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase {
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration) {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDTO model) {
        var user = await _userManager.FindByNameAsync(model.Username!);
        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!)) {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAcessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:refreshTokenValidityInMinutes"],
                out int refreshTokenValidityInMinutes);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);
            return Ok(new {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDTO model)
    {
        var userExist = await _userManager.FindByNameAsync(model.Username!);
        if (userExist is not null) {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseDTO { Status = "Error", Message = "User already exists!" });
        }

        ApplicationUser user = new() {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseDTO { Status = "Error", Message = "User creation failed." });
        }
        return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!"});
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModelDTO tokenModel) {
        if (tokenModel is null) {
            return BadRequest("Invalid client request");
        }
        string? accessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));
        string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentException(nameof(tokenModel));
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);
        if (principal == null) {
            return BadRequest("Invalid access token/refresh token");
        }
        string username = principal.Identity.Name;
        var user = await _userManager.FindByNameAsync(username);
        if (user is null || user.RefreshToken != refreshToken
                         || user.RefreshTokenExpiryTime != DateTime.Now) {
            return BadRequest("Invalid access token/refresh token");
        }
        var newAccessToken = _tokenService.GenerateAcessToken(principal.Claims.ToList(), _configuration);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);
        return new ObjectResult(new {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username) {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) return BadRequest("Invalid user name");
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
        return NoContent();
    }
}