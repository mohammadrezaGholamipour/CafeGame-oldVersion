using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CafeGameApi.Entities;

public class HourRate
{
    public int Id { get; set; } = default!;
    public int Rate { get; set; } = default!;

    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public virtual IdentityUser<int>? User { get; set; }
}