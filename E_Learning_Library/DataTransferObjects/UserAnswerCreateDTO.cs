using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning_Library.DataTransferObjects;

public class UserAnswerCreateDTO
{
    public int? User_id { get; set; }

    public int? AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public int? QuizId { get; set; }
}
