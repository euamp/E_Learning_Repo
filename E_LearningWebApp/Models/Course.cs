namespace E_LearningWebApp.Models;

public class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }
}
