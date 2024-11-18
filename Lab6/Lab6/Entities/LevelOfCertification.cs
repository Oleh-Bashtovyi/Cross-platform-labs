using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class LevelOfCertification
{
    [Key]
    public string CertificationCode { get; set; }

    [ForeignKey("DiveOrganisation")]
    public string OrganisationCode { get; set; }


    public string CertificationName { get; set; }
    public string OtherDetails { get; set; }

    public DiveOrganisation? DiveOrganisation { get; set; }
    public ICollection<DiverCertification>? DiverCertifications { get; set; }
}
