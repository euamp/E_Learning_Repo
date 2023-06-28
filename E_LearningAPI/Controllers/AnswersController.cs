using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswersController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public AnswersController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/Answers
    [HttpGet]
    public ActionResult<IEnumerable<Answer>> Get()
    {
        var answers = _context.Answers
            .ToList();
        return Ok(answers);
    }

    // GET api/Answers/5
    [HttpGet("{id}")]
    public ActionResult<Answer> Get(int id)
    {
        // TODO: Προσθέτω τα Include() Που έχει και από πάνω
        var answer = _context.Answers
            .SingleOrDefault(x => x.AnswerId == id);

        if (answer == null)
        {
            return NotFound("Answer Not Found");
        }
        else
        {
            return Ok(answer);
        }
    }

    // POST api/Answers
    [HttpPost]
    public ActionResult Post(AnswerCreateDTO answerCreateDTO)
    {
        var question = _context.Questions
            .Where(q => q.QuestionId == answerCreateDTO.QuestionId)
            .FirstOrDefault();

        if (question is null)
        {
            return BadRequest();
        }
        else
        {
            
            Answer answer = new Answer();
            answer.QuestionId = answerCreateDTO.QuestionId;
            answer.AnswerText = answerCreateDTO.AnswerText;
            answer.IsCorrect = answerCreateDTO.IsCorrect;

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return Ok(answer);
        }
    }

    // PUT api/Answers/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, AnswerEditDTO answerEditDTO)
    {
        var quiz = _context.Answers.Find(id);

        if (quiz != null)
        {
            try
            {
                _context.Answers
                .Where(i => i.AnswerId == id)
                .ExecuteUpdate(s => s
                    .SetProperty(t => t.AnswerText, t => answerEditDTO.AnswerText)
                );

                return Ok(answerEditDTO);
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

    // DELETE api/Answers/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var answer = _context.Answers.Find(id);

        if (answer == null)
        {
            return NotFound("Answer Not Found");
        }
        else
        {
            _context.Answers
                .Where(i => i.AnswerId.Equals(id))
                .ExecuteDelete();

            return Ok("Deleted Successfully");
        }
    }
}
