using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning_Library.DataTransferObjects;

public class AnswerCreateDTO
{
    public int QuestionId { get; set; }

    public string AnswerText { get; set; } = null!;

    public string IsCorrect { get; set; } = null!;
}
