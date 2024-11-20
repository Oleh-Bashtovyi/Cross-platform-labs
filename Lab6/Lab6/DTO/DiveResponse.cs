namespace Lab6.DTO;

public class DiveResponse
{
    public Guid DiveId { get; set; }

    public Guid DiverId { get; set; }
    public DateTime DiveDate {  get; set; }
    public Guid DiveSiteId { get; set; }
    public bool NightDiveYn { get; set; }
    public string OtherDetails { get; set; }
    public string DiverName { get; set; }
    public string DiveSiteName { get; set; }
}
