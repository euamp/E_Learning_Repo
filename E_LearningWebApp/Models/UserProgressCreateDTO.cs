namespace E_LearningWebApp.Models;

public class UserProgressCreateDTO
{
    public int UserId { get; set; }

    public int QuizId { get; set; }

    public string Quizname { get; set; }

    public string Score { get; set; } = null!;
}
