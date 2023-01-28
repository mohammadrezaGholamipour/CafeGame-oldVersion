namespace CafeGameApi.Entities;

public class BillBriefDto
{
    public int Id { get; set; }
    public int FinalCost { get; set; }
    public TimeSpan TotalTime { get; set; }
    public DateTime EndTime { get; set; }
}