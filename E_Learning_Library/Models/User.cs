using System;
using System.Collections.Generic;

namespace E_Learning_Library.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string IsGraduated { get; set; } = null!;

    public virtual ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
