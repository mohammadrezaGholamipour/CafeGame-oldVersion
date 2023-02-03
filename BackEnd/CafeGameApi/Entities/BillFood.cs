using System.Text.Json.Serialization;

namespace CafeGameApi.Entities;

public class BillFood
{
    public int Id { get; set; } = default!;
    public int Count { get; set; } = default!;
    public int FoodId { get; set; } = default!;
    public int BillId { get; set; } = default!;
    [JsonIgnore]
    public Bill? Bill { get; set; }
    [JsonIgnore]
    public Food? Food { get; set; }
}