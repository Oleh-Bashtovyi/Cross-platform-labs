using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class Wreck
{
    [Key]
    [ForeignKey("DiveSite")]
    public Guid DiveSiteId { get; set; }
    public DateTime WreckDate { get; set; }
    public string WreckDetails { get; set; }

    public DiveSite? DiveSite { get; set; }
}
