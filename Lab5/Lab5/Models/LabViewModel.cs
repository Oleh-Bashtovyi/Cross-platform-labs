using System.ComponentModel.DataAnnotations;

namespace Lab6.ViewModels;

public class TestCase
{
    public string Input { get; set; }
    public string Output { get; set; }
}

public class LabViewModel
{
    public int LabNumber { get; set; }
    public string TaskVariant { get; set; }
    public string Description { get; set; }
    public string InputDescription { get; set; }
    public string OutputDescription { get; set; }
    public List<TestCase> TestCases { get; set; }


    [Required]
    public IFormFile InputFile { get; set; }
    public string InputContent { get; set; }
    public string OutputContent { get; set; }
}
