using System;
using System.Collections.Generic;

namespace E_Learning_Library.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; } = null!;

    public string IsCorrect { get; set; } = null!;

    //public virtual Question Question { get; set; } = null!;


    // Πιθανόν αχρείαστο. Συζητάμε με Ευα για τον αν θα το διαγράψουμε
    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
