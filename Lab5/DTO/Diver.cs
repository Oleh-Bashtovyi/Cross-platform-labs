namespace Lab5.DTO;

public class Diver
{
    public Guid DiverId { get; set; }
    public string Name { get; set; }
    public string DiverDetails { get; set; }

    public ICollection<DiverCertification> Certifications { get; set; }
    public ICollection<Dive> Dives { get; set; }
}
