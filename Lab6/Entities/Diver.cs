using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class Diver
{
    [Key]
    public Guid DiverId { get; set; }

    public string Name { get; set; }
    public string DiverDetails { get; set; }

    public ICollection<DiverCertification> Certifications { get; set; }
    public ICollection<Dive> Dives { get; set; }
}
