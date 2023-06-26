using System.ComponentModel.DataAnnotations;

namespace E_LearningWebApp.Models;

public class UserAnswer
{
    public int UserAnswerId { get; set; }

    public int? User_id { get; set; }

    public int? QuizId { get; set; }

    [Required(ErrorMessage = "Δεν επιτρέπεται να προχωρήσετε πρωτού δώσετε απάντηση")]
    public int? AnswerId { get; set; }

    public int? QuestionId { get; set; }
}
