namespace E_LearningWebApp.Models;

public class Answer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; } = null!;

    public string IsCorrect { get; set; } = null!;
}
