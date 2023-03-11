using System.Security.Claims;
using CafeGameApi.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CafeGameApi.Controllers;

public abstract class AppBaseUserController : ControllerBase
{
    protected int AppUserId { get; }
    protected IdentityUser<int> AppUser { get; }

    protected AppBaseUserController(UserManager<IdentityUser<int>> userManager,
        IHttpContextAccessor contextAccessor)
    {
        
        var userIdString =
            contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value ??
            throw new EntityNotFoundException(typeof(IdentityUser<int>));

        AppUserId = int.Parse(userIdString);

        AppUser = userManager.FindByIdAsync(userIdString).Result ?? 
                  throw new EntityNotFoundException(typeof(IdentityUser<int>));
    }
}