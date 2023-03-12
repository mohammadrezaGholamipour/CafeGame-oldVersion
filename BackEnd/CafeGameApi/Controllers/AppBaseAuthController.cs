using CafeGameApi.Config;
using CafeGameApi.ConfigModels;
using CafeGameApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeGameApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AppBaseAuthController : ControllerBase
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public AppBaseAuthController(IOptions<JwtOptions> jwtOptions, UserManager<IdentityUser<int>> userManager)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions?.Value ?? new JwtOptions();
    }

    private JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(_jwtOptions.DurationInMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    private JwtSecurityToken GenerateLoginJwtToken(string userId,
        string email, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Sid, userId),
            new Claim(ClaimTypes.Role, role)
        };

        return GenerateJwt(claims);
    }

    [HttpPost("login")]
    public async Task<IActionResult> AttemptLogin(LoginRequest loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);

        if (user == null)
            return NotFound();

        switch (await _userManager.GetRolesAsync(user))
        {
            case var value when value.Contains(AppConstants.UserRoles.NotRegisteredUser):
                return Ok(new LoginResponse
                {
                    UserState = UserState.NotRegistered,
                    Token = string.Empty
                });
            case var value when value.Contains(AppConstants.UserRoles.RegisteredUser):
                return Ok(new LoginResponse
                {
                    UserState = UserState.Registered,
                    Token = new JwtSecurityTokenHandler().WriteToken(
                        GenerateLoginJwtToken(user.Id.ToString(), user.Email!, AppConstants.UserRoles.RegisteredUser))
                });
            case var value when value.Contains(AppConstants.UserRoles.Banned):
                return Ok(new LoginResponse
                {
                    UserState = UserState.Banned,
                    Token = string.Empty
                });
            case var value when value.Contains(AppConstants.UserRoles.Admin):
                return Ok(new LoginResponse
                {
                    UserState = UserState.Admin,
                    Token = new JwtSecurityTokenHandler().WriteToken(
                        GenerateLoginJwtToken(user.Id.ToString(), user.Email!, AppConstants.UserRoles.Admin))
                });
            default:
                return Ok(new LoginResponse
                {
                    UserState = UserState.NotRegistered,
                    Token = string.Empty
                });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerModel)
    {
        var userExists = await _userManager.FindByNameAsync(registerModel.UserName);
        if (userExists != null)
            return Conflict();
        // throw new RepeatedEntityException(typeof(IdentityUser<int>));

        var user = new IdentityUser<int>
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            PhoneNumber = registerModel.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, registerModel.Password);
        if (result.Succeeded)
            if ((await _userManager.AddToRoleAsync(user, AppConstants.UserRoles.RegisteredUser)).Succeeded)
                return Ok(new LoginResponse
                {
                    UserState = UserState.Registered,
                    Token = new JwtSecurityTokenHandler().WriteToken(
                                GenerateLoginJwtToken(user.Id.ToString(), user.Email, AppConstants.UserRoles.RegisteredUser))
                });

        return BadRequest(result.Errors);
    }
}