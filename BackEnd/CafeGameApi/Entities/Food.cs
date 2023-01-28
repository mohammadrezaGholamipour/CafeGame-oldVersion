namespace CafeGameApi.Entities;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<BillFood> BillFoods { get; set; } = new List<BillFood>();
}