using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class Dive
{
    [Key]
    public Guid DiveId { get; set; }

    [ForeignKey("Diver")]
    public Guid DiverId { get; set; }

    [ForeignKey("DiveSite")]
    public Guid DiveSiteId { get; set; }


    public DateTime DiveDate { get; set; }
    public bool NightDiveYn { get; set; }
    public string OtherDetails { get; set; }

    public Diver? Diver { get; set; }
    public DiveSite? DiveSite { get; set; }
}
