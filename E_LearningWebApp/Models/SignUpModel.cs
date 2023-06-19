using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_LearningWebApp.Models;

public class SignUpModel
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [DisplayName("Email address:")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DisplayName("First Name:")]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    [DisplayName("Last Name:")]
    public string Lastname { get; set; } = string.Empty;
}
