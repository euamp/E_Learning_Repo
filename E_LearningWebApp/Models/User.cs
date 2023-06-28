namespace E_LearningWebApp.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string IsGraduated { get; set; } = null!;
}
