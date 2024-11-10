namespace Lab5.DTO;

public class DiverCertification
{
    public Guid DiverId { get; set; }
    public string CertificationCode { get; set; }
    public DateTime CertificationDate { get; set; }
    public string InstructorName { get; set; }
    public string InstructionLocation { get; set; }
    public Diver Diver { get; set; }
    public LevelOfCertification Certification { get; set; }
}
