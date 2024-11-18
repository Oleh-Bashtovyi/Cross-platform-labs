using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class DiverCertification
{
    [Key]
    public Guid DiverId { get; set; }
    [Key]
    public string CertificationCode { get; set; }
    public DateTime CertificationDate { get; set; }
    public string InstructorName { get; set; }
    public string InstructionLocation { get; set; }

    public Diver? Diver { get; set; }
    public LevelOfCertification? Certification { get; set; }
}
