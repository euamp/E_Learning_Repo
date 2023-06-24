namespace E_LearningWebApp.Models;

public class Quiz
{
    public int QuizId { get; set; }

    public int CourseId { get; set; }

    public string QuizName { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();

    public virtual ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
}
