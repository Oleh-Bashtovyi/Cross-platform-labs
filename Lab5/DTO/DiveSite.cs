namespace Lab5.DTO;

public class DiveSite
{
    public Guid DiveSiteId { get; set; }
    public string DiveSiteCode { get; set; }    
    public string DiveSiteName { get; set; }
    public string DiveSiteDescription { get; set; }
    public string OtherDetails { get; set; }
    public DiveSiteType DiveSiteType { get; set; }
    public ICollection<Dive> Dives { get; set; }
    public Wreck Wreck { get; set; }
}