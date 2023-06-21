using System;
using System.Collections.Generic;

namespace E_Learning_Library.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
