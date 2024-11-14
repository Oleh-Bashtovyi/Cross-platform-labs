namespace Lab5.DTO;

public class DiveSiteTypeResponse
{
    public string DiveSiteCode { get; set; }
    public string DiveSiteDetails { get; set; }
    public ICollection<DiveSiteResponse> DiveSites { get; set; }
}
