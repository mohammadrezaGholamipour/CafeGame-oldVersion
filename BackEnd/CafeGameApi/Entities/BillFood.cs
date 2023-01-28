namespace CafeGameApi.Entities;

public class BillFood
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int FoodId { get; set; }
    public int BillId { get; set; }
    public Bill? Bill { get; set; }
    public Food? Food { get; set; }
}