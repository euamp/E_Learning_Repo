using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public CoursesController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/Courses
    [HttpGet]
    public ActionResult<IEnumerable<Course>> Get()
    {
        var courses = _context.Courses.ToList();
        return Ok(courses);
    }

    // GET api/Courses/5
    [HttpGet("{id}")]
    public ActionResult<Course> Get(int id)
    {
        var course = _context.Courses.SingleOrDefault(x => x.CourseId == id);

        if (course == null)
        {
            return NotFound("Course Not Found");
        }
        else
        {
            return Ok(course);
        }
    }

    // POST api/Courses
    [HttpPost]
    public ActionResult Post(CourseDTO courseCreateDTO)
    {
        try
        {
            Course course = new Course();
            course.CourseName = courseCreateDTO.CourseName;
            course.Description = courseCreateDTO.Description;
            course.Image = courseCreateDTO.Image;

            _context.Courses.Add(course);
            _context.SaveChanges();

            return Ok(course);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    // PUT api/Courses/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, CourseDTO courseDTO)
    {
        var course = _context.Courses.Find(id);

        if(course != null)
        {
            try
            {
                _context.Courses
                .Where(i => i.CourseId == id)
                .ExecuteUpdate(s => s
                    .SetProperty(n => n.CourseName, n => courseDTO.CourseName)
                    .SetProperty(d => d.Description, d => courseDTO.Description)
                    .SetProperty(i => i.Image, i => courseDTO.Image)
                );

                return Ok(courseDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    // DELETE api/Courses/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var courses = _context.Courses.Find(id);

        if (courses == null)
        {
            return NotFound("Course Not Found");
        }
        else
        {
            _context.Courses
                .Where(i => i.CourseId.Equals(id))
                .ExecuteDelete();

            return Ok("Deleted Successfully");
        }
    }
}
