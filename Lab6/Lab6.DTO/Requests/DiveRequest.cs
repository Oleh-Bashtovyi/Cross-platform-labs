namespace Lab6.DTO;

public class DiveRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? DiverId { get; set; }
    public string? SiteNameStart { get; set; }
    public string? SiteNameEnd { get; set; }


    public string ToQueryString()
    {
        var parameters = new List<string>();

        if (StartDate.HasValue)
            parameters.Add($"{nameof(StartDate)}={StartDate?.ToString("yyyy-MM-dd")}");

        if (EndDate.HasValue)
            parameters.Add($"{nameof(EndDate)}={EndDate?.ToString("yyyy-MM-dd")}");

        if (DiverId.HasValue)
            parameters.Add($"{nameof(DiverId)}={DiverId}");

        if (!string.IsNullOrEmpty(SiteNameStart))
            parameters.Add($"{nameof(SiteNameStart)}={Uri.EscapeDataString(SiteNameStart)}");

        if (!string.IsNullOrEmpty(SiteNameEnd))
            parameters.Add($"{nameof(SiteNameEnd)}={Uri.EscapeDataString(SiteNameEnd)}");

        return parameters.Count > 0 ? string.Join("&", parameters) : string.Empty;
    }

}
