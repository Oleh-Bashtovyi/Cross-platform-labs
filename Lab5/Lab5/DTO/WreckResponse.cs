namespace Lab5.DTO;

public class WreckResponse
{
    public Guid DiveSiteId { get; set; }
    public DateTime WreckDate { get; set; }
    public string WreckDetails { get; set; }


    // DiveSite properties
    public string DiveSiteCode { get; set; }
    public string DiveSiteName { get; set; }
    public string DiveSiteDescription { get; set; }
    public string OtherDetails { get; set; }
}
