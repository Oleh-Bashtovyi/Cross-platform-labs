using System.ComponentModel.DataAnnotations;

namespace Lab5.ViewModels;

public class UserLoginViewModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
    public required string Password { get; set; }
}
