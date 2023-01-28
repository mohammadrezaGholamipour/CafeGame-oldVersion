namespace CafeGameApi.Entities;

public class PSSystem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
}