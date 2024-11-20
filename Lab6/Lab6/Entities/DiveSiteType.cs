using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class DiveSiteType
{
    [Key]
    public string DiveSiteCode { get; set; }
    public string DiveSiteDetails { get; set; }

    public ICollection<DiveSite>? DiveSites { get; set; }
}
