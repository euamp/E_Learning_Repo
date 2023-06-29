using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning_Library.DataTransferObjects;

public class UserProgressCreateDTO
{
    public int UserId { get; set; }

    public int QuizId { get; set; }

    public string Score { get; set; } = null!;
}
