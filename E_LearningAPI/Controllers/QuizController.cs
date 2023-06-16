using E_Learning_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public QuizController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/Quiz
    [HttpGet]
    public ActionResult<IEnumerable<Quiz>> Get()
    {
        var quizes = _context.Quizzes.ToList();
        return Ok(quizes);
    }

    // GET api/Quiz/5
    [HttpGet("{id}")]
    public ActionResult<Quiz> Get(int id)
    {
        var quiz = _context.Quizzes.SingleOrDefault(x => x.QuizId == id);

        if (quiz == null)
        {
            return NotFound("Quiz Not Found");
        }
        else
        {
            return Ok(quiz);
        }
    }

    // POST api/Quiz
    [HttpPost]
    public ActionResult Post([FromBody] string value)
    {
        var course = _context.Courses.ToList();

        if(course == null)
        {
            return BadRequest();
        }
    }

    // PUT api/Quiz/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/Quiz/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var quiz = _context.Quizzes.Find(id);

        if (quiz == null)
        {
            return NotFound("Quiz Not Found");
        }
        else
        {
            _context.Quizzes
                .Where(i => i.QuizId.Equals(id))
                .ExecuteDelete();

            return Ok("Deleted Successfully");
        }
    }
}
