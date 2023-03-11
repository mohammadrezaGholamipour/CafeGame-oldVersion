using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CafeGameApi.Entities;

public class PSSystem
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; } = string.Empty;
    [JsonIgnore]
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public int UserId { get; set; }
    public virtual IdentityUser<int>? User { get; set; }
}