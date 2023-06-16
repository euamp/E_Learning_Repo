using System;
using System.Collections.Generic;

namespace E_Learning_Library.Models;

public partial class UserAnswer
{
    public int UserAnswerId { get; set; }

    public int? User_id { get; set; }    

    public int? AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public int? QuizId { get; set; }

    //public virtual Answer? Answer { get; set; }

    //public virtual Question? Question { get; set; }

    //public virtual Quiz? Quiz { get; set; }
}
