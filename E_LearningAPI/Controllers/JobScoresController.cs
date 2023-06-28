using E_Learning_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobScoresController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public JobScoresController(ELearningDbContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public ActionResult<IEnumerable<JobScores>> Get()
    {
        var jobScores = _context.JobScores.ToList();
        return Ok(jobScores);
    }
}
