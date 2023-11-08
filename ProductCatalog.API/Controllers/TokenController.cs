using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.API.DTOs;
using ProductCatalog.Domain.Account;

namespace ProductCatalog.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authenticatation;
    private readonly IConfiguration _configuration;

    public TokenController(IAuthenticate authenticatation, IConfiguration configuration)
    {
        _authenticatation = authenticatation ?? throw new ArgumentNullException(nameof(authenticatation));
        _configuration = configuration;
    }

    [HttpPost("CreateUser")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> CreateUser([FromBody] LoginDTO userInfo)
    {
        var result = await _authenticatation.RegisterUserAsync(userInfo.Email, userInfo.Password);
        if (result)
        {
            return Ok($"User {userInfo.Email} was created successfully");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid user creation attempt");
            return BadRequest(ModelState);
        }
    }

    [AllowAnonymous]
    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserTokenDTO>> Login([FromBody] LoginDTO userInfo)
    {
        var result = await _authenticatation.AuthenticateAsunc(userInfo.Email, userInfo.Password);
        if (result)
        {
            return GenerateToken(userInfo);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return BadRequest(ModelState);
        }
    }

    private UserTokenDTO GenerateToken(LoginDTO userInfo)
    {
        var claims = new[]
        {
            new Claim ("email", userInfo.Email),
            new Claim("meuvalor", "o que vc quiser"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? string.Empty));
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(10);
        JwtSecurityToken token = new
        (
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserTokenDTO()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
