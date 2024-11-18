using System.ComponentModel.DataAnnotations;

namespace Lab13.Server.Models;

public class UserRegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Username can only contain english letters, numbers, dots, underscores, and hyphens")]
    public string UserName { get; set; }

    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(500, ErrorMessage = "Full name cannot be longer than 500 characters")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Phone must be in ukrainian format (+380XXXXXXXXX).")]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
}

