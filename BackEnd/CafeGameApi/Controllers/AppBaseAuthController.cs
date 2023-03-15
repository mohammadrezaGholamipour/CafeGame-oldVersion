using CafeGameApi.Config;
using CafeGameApi.ConfigModels;
using CafeGameApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace CafeGameApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AppBaseAuthController : ControllerBase
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly SignInManager<IdentityUser<int>> _signInManager;
    private readonly SmtpClient _smtpClient;
    private readonly IConfiguration _configuration;

    public AppBaseAuthController(IOptions<JwtOptions> jwtOptions, UserManager<IdentityUser<int>> userManager, SmtpClient smtpClient, SignInManager<IdentityUser<int>> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _smtpClient = smtpClient;
        _signInManager = signInManager;
        _configuration = configuration;
        _jwtOptions = jwtOptions?.Value ?? new JwtOptions();
    }

    #region Private Methods
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
    #endregion

    [HttpPost("login")]
    public async Task<IActionResult> AttemptLogin([FromBody] LoginRequest loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);

        if (user == null)
            return NotFound();

        if (!(await _signInManager.CheckPasswordSignInAsync(user,
                loginModel.Password,
                false)).Succeeded)
            return BadRequest("Wrong Password");

        return await _userManager.GetRolesAsync(user) switch
        {
            var value when value.Contains(AppConstants.UserRoles.Banned) =>
                Ok(new LoginResponse
                {
                    UserState = UserState.Banned,
                    Token = string.Empty
                }),
            var value when value.Contains(AppConstants.UserRoles.NotRegisteredUser) =>
                Ok(new LoginResponse
                {
                    UserState = UserState.NotRegistered,
                    Token = string.Empty
                }),
            var value when value.Contains(AppConstants.UserRoles.RegisteredUser) =>
                Ok(new LoginResponse
                {
                    UserState = UserState.Registered,
                    Token = new JwtSecurityTokenHandler().WriteToken(GenerateLoginJwtToken(user.Id.ToString(),
                    user.Email!, AppConstants.UserRoles.RegisteredUser))
                }),
            var value when value.Contains(AppConstants.UserRoles.Admin) =>
                Ok(new LoginResponse
                {
                    UserState = UserState.Admin,
                    Token = new JwtSecurityTokenHandler().WriteToken(GenerateLoginJwtToken(user.Id.ToString(),
                    user.Email!, AppConstants.UserRoles.Admin))
                }),
            _ => Ok(new LoginResponse { UserState = UserState.NotRegistered, Token = string.Empty })
        };
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerModel)
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

    [HttpGet("reset-pass")]
    public async Task<IActionResult> ResetPassword([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("Invalid Email");
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return NotFound("No User Registered With This Email");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //Send token to email
        var message = new MailMessage(_configuration["Smtp:Username"] 
                                      ?? throw new NullReferenceException("email account not found"), 
            email, 
            "Password Reset", 
            $"Your reset code is : {token}");

        await _smtpClient.SendMailAsync(message);

        return Ok("Email Sent");
    }

    [HttpPost("reset-pass")]
    public async Task<IActionResult> ResetPassword([FromBody] LoginRequest model, [FromQuery] string token)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
            return NotFound();

        var resetResult = await _userManager.ResetPasswordAsync(user, token, model.Password);

        if (!resetResult.Succeeded)
        {
            return BadRequest(resetResult.Errors);
        }

        return await AttemptLogin(model);
    }
}