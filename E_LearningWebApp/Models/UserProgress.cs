namespace E_LearningWebApp.Models;

public class UserProgress
{
    public int UserProgressId { get; set; }

    public int UserId { get; set; }

    public int QuizId { get; set; }

    public string Score { get; set; } = null!;

    public DateTime DateCompleted { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    //public virtual User User { get; set; } = null!;
}
