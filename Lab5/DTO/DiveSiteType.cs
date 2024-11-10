namespace Lab5.DTO;

public class DiveSiteType
{
    public string DiveSiteCode { get; set; }
    public string DiveSiteDetails { get; set; }
    public ICollection<DiveSite> DiveSites { get; set; }
}
