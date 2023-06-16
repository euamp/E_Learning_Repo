using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning_Library.DataTransferObjects;

public class QuizCreateDTO
{
    public int CourseId { get; set; }

    public string QuizName { get; set; } = null!;
}
