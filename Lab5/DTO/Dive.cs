namespace Lab5.DTO;

public class Dive
{
    public Guid DiveId { get; set; }
    public Guid DiverId { get; set; }
    public Guid DiveSiteId { get; set; }
    public DateTime DiveDate { get; set; }
    public bool NightDiveYn { get; set; }
    public string OtherDetails { get; set; }
    public Diver Diver { get; set; }
    public DiveSite DiveSite { get; set; }
}
