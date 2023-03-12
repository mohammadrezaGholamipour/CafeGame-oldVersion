using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CafeGameApi.Entities;

public class Bill
{
    public int Id { get; set; } = default!;
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public int? FinalCost { get; set; }

    public int SystemId { get; set; } = default!;
    [JsonIgnore]
    public virtual PSSystem? System { get; set; }

    public int HourRateId { get; set; } = default!;
    [JsonIgnore]
    public virtual HourRate? HourRate { get; set; }


    public ICollection<BillFood> BillFoods { get; set; } = new List<BillFood>();

    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public virtual IdentityUser<int>? User { get; set; }
}