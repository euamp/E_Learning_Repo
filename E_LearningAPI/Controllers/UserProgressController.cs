using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProgressController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public UserProgressController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/UserProgress
    [HttpGet]
    public ActionResult<IEnumerable<UserProgress>> Get()
    {
        var userProgresses = _context.UserProgresses
            .ToList();
        return Ok(userProgresses);
    }

    // GET api/UserProgress/5
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<UserProgress>> GetProgressOfUser(int id)
    {
        var progress = _context.UserProgresses
            .Where(x => x.UserId == id)
            .OrderByDescending(d => d.DateCompleted)
            .OrderBy(q => q.Quizname)
            .ToList();

        if (progress.Count.Equals(0))
        {
            return NotFound("Progress Not Found");
        }
        else
        {
            return Ok(progress);
        }
    }

    // POST api/userProgress
    [HttpPost]
    public ActionResult Post(UserProgressCreateDTO userProgressCreateDTO)
    {

        UserProgress userProgress = new UserProgress();
        userProgress.UserId = userProgressCreateDTO.UserId;
        userProgress.QuizId = userProgressCreateDTO.QuizId;
        userProgress.Quizname = userProgressCreateDTO.Quizname;
        userProgress.Score = userProgressCreateDTO.Score;
        userProgress.DateCompleted = DateTime.Now;

        try
        {
            _context.UserProgresses.Add(userProgress);
            _context.SaveChanges();

            return Ok(userProgress);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return BadRequest();
        }
    }
}
