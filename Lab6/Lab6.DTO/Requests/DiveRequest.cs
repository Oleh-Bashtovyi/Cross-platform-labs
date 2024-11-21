using Microsoft.AspNetCore.WebUtilities;

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
        var parameters = new Dictionary<string, string?>();

        if (StartDate.HasValue)
            parameters["dateFrom"] = StartDate?.ToString("yyyy-MM-ddTHH:mm:ss");

        if (EndDate.HasValue)
            parameters["dateTo"] = EndDate?.ToString("yyyy-MM-ddTHH:mm:ss");

        if (DiverId.HasValue)
            parameters["diverId"] = DiverId.ToString();

        if (!string.IsNullOrEmpty(SiteNameStart))
            parameters["startDate"] = SiteNameStart;

        if (!string.IsNullOrEmpty(SiteNameEnd))
            parameters["endDate"] = SiteNameEnd;

        string query = QueryHelpers.AddQueryString("", parameters);

        return query;
    }
}
