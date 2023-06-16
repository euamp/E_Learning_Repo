using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public QuestionsController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/Questions
    [HttpGet]
    public ActionResult<IEnumerable<Question>> Get()
    {
        var questions = _context.Questions
            .Include(a => a.Answers)
            .ToList();
        return Ok(questions);
    }

    // GET api/Questions/5
    [HttpGet("{id}")]
    public ActionResult<Question> Get(int id)
    {
        // TODO: Προσθέτω τα Include() Που έχει και από πάνω
        var question = _context.Questions
            .SingleOrDefault(x => x.QuestionId == id);

        if (question is null)
        {
            return NotFound("Question Not Found");
        }
        else
        {
            return Ok(question);
        }
    }

    // POST api/Questions
    [HttpPost]
    public ActionResult Post(QuestionCreateDTO questionCreateDTO)
    {
        var quiz = _context.Quizzes
            .Where(q => q.QuizId == questionCreateDTO.QuizId)
            .FirstOrDefault();

        if (quiz is null)
        {
            return BadRequest();
        }
        else
        {
            Question question = new Question();
            question.QuizId = questionCreateDTO.QuizId;
            question.QuestionText = questionCreateDTO.QuestionText;
            //question.Quiz = quiz;

            _context.Questions.Add(question);
            _context.SaveChanges();

            return Ok(question);
        }
    }

    // PUT api/Questions/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, QuestionEditDTO questionEditDTO)
    {
        var question = _context.Questions.Find(id);

        if (question != null)
        {
            try
            {
                _context.Questions
                .Where(i => i.QuestionId == id)
                .ExecuteUpdate(s => s
                    .SetProperty(t => t.QuestionText, t => questionEditDTO.QuestionText)
                );

                return Ok(questionEditDTO);
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

    // DELETE api/Questions/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var question = _context.Questions.Find(id);

        if (question == null)
        {
            return NotFound("Question Not Found");
        }
        else
        {
            _context.Questions
                .Where(i => i.QuestionId.Equals(id))
                .ExecuteDelete();

            return Ok("Deleted Successfully");
        }
    }
}
