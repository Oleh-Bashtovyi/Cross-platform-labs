namespace Lab6.DTO;

public class DiveRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? DiverId { get; set; }
    public string? SiteNameStart { get; set; }
    public string? SiteNameEnd { get; set; }
}
