namespace CafeGameApi.Entities;

public class Bill
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public int? FinalCost { get; set; }

    public int SystemId { get; set; }
    public virtual PSSystem? System { get; set; }

    public ICollection<BillFood> BillFoods { get; set; } = new List<BillFood>();
}