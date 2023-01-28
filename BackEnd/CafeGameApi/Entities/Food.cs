using System.Text.Json.Serialization;

namespace CafeGameApi.Entities;

public class Food
{
    public int? Id { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public int? Cost { get; set; } = default!;
    [JsonIgnore]
    public ICollection<BillFood> BillFoods { get; set; } = new List<BillFood>();
}