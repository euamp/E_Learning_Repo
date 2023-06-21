using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning_Library.DataTransferObjects;

public class CourseDTO
{
    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }
}
