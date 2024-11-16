using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class DiveOrganisation
{
    [Key]
    public string OrganisationCode { get; set; }
    public string CountryOfOrigin { get; set; }
    public string OrganisationDetails { get; set; }

    public ICollection<LevelOfCertification> Certifications { get; set; }
}
