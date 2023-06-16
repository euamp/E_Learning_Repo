using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning_Library.DataTransferObjects;

public class QuestionCreateDTO
{
    public int QuizId { get; set; }

    public string QuestionText { get; set; } = null!;
}
