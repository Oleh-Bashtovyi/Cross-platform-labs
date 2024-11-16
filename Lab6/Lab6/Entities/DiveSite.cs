using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class DiveSite
{
    [Key]
    public Guid DiveSiteId { get; set; }

    [ForeignKey("DiveSiteType")]
    public string DiveSiteCode { get; set; }    
    

    public string DiveSiteName { get; set; }
    public string DiveSiteDescription { get; set; }
    public string OtherDetails { get; set; }

    public DiveSiteType DiveSiteType { get; set; }
    public ICollection<Dive> Dives { get; set; }
    public Wreck Wreck { get; set; }
}