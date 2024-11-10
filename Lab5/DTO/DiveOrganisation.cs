namespace Lab5.DTO;

public class DiveOrganisation
{
    public string OrganisationCode { get; set; }
    public string CountryOfOrigin { get; set; }
    public string OrganisationDetails { get; set; }
    public ICollection<LevelOfCertification> Certifications { get; set; }
}
