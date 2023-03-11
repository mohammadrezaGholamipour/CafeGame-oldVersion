using Microsoft.AspNetCore.Identity;

namespace CafeGameApi.Entities;

public class HourRate
{
    public int Id { get; set; } = default!;
    public int Rate { get; set; } = default!;

    public int UserId { get; set; }
    public virtual IdentityUser<int>? User { get; set; }
}