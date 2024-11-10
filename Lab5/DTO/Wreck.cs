namespace Lab5.DTO;

public class Wreck
{
    public Guid DiveSiteId { get; set; }
    public DateTime WreckDate { get; set; }
    public string WreckDetails { get; set; }
    public DiveSite DiveSite { get; set; }
}
