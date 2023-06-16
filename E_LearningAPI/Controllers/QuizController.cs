using E_Learning_Library.DataTransferObjects;
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
        var quizes = _context.Quizzes
            .Include(q => q.Questions)
            .ToList();
        return Ok(quizes);
    }

    // GET api/Quiz/5
    [HttpGet("{id}")]
    public ActionResult<Quiz> Get(int id)
    {
        var quiz = _context.Quizzes
            .SingleOrDefault(x => x.QuizId == id);

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
    public ActionResult Post(QuizCreateDTO quizCreateDTO)
    {
        var course = _context.Courses
            .Where(c => c.CourseId == quizCreateDTO.CourseId)
            .FirstOrDefault();

        if (course is null)
        {
            return BadRequest();
        }
        else
        {
            Quiz quiz = new Quiz();
            quiz.CourseId = quizCreateDTO.CourseId;
            quiz.QuizName = quizCreateDTO.QuizName;
            quiz.Course = course;

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();

            return Ok(quiz);
        }
    }

    // PUT api/Quiz/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, QuizEditDTO quizEditDTO)
    {
        var quiz = _context.Quizzes.Find(id);

        if (quiz != null)
        {
            try
            {
                _context.Quizzes
                .Where(i => i.QuizId == id)
                .ExecuteUpdate(s => s
                    .SetProperty(n => n.QuizName, n => quizEditDTO.QuizName)
                );

                return Ok(quizEditDTO);
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
