using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAnswersController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public UserAnswersController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/UserAnswers
    [HttpGet]
    public ActionResult<IEnumerable<UserAnswer>> Get()
    {
        var userAnswers = _context.UserAnswers
            .ToList();
        return Ok(userAnswers);
    }

    // GET api/UserAnswers/5
    [HttpGet("{id}")]
    public ActionResult<UserAnswer> Get(int id)
    {
        var userAnswer = _context.UserAnswers
            .SingleOrDefault(x => x.UserAnswerId == id);

        if (userAnswer == null)
        {
            return NotFound("User Answer Not Found");
        }
        else
        {
            return Ok(userAnswer);
        }
    }

    // POST api/UserAnswers
    [HttpPost]
    public ActionResult Post(UserAnswerCreateDTO userAnswerCreateDTO)
    {
        var user = _context.Users
            .Where(i => i.Id == userAnswerCreateDTO.User_id)
            .FirstOrDefault();

        var answer = _context.Answers
            .Where(i => i.AnswerId == userAnswerCreateDTO.AnswerId)
            .FirstOrDefault();

        var question = _context.Questions
            .Where(i => i.QuestionId == userAnswerCreateDTO.QuestionId)
            .FirstOrDefault();

        var quiz = _context.Quizzes
            .Where(i => i.QuizId == userAnswerCreateDTO.QuizId)
            .FirstOrDefault();

        if (user is null)
        {
            return BadRequest();
        }
        else if(answer is null)
        {
            return BadRequest();
        }
        else if (question is null)
        {
            return BadRequest();
        }
        else if (quiz is null)
        {
            return BadRequest();
        }
        else
        {
            UserAnswer userAnswer = new UserAnswer();
            userAnswer.User_id = userAnswerCreateDTO.User_id;
            userAnswer.AnswerId = userAnswerCreateDTO.AnswerId;
            userAnswer.QuestionId = userAnswerCreateDTO.QuestionId;
            userAnswer.QuizId = userAnswerCreateDTO.QuizId;

            _context.UserAnswers.Add(userAnswer);
            _context.SaveChanges();

            return Ok(userAnswer);
        }
    }

    // PUT api/UserAnswers/5
    //[HttpPut("{id}")]
    //public ActionResult Put()
    //{
        
    //}

    // DELETE api/UserAnswers/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var userAnswer = _context.UserAnswers.Find(id);

        if (userAnswer == null)
        {
            return NotFound("User answer Not Found");
        }
        else
        {
            _context.UserAnswers
                .Where(i => i.UserAnswerId.Equals(id))
                .ExecuteDelete();

            return Ok("Deleted Successfully");
        }
    }
}
