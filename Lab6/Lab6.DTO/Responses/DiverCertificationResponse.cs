namespace Lab6.DTO;

public class DiverCertificationResponse
{
    public Guid DiverId { get; set; }
    public string CertificationCode { get; set; }
    public DateTime CertificationDate { get; set; }
    public string InstructorName { get; set; }
    public string InstructionLocation { get; set; }
}
