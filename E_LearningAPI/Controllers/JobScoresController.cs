using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    // GET: api/JobScores
    [HttpGet]
    public ActionResult<IEnumerable<JobScores>> Get()
    {
        var jobScores = _context.JobScores.ToList();
        return Ok(jobScores);
    }

    // GET: api/JobScores/3
    [HttpGet("{id}")]
    public ActionResult<JobScores> Get(int id)
    {
        try
        {
            var jobScore = _context.JobScores
            .Where(i => i.User_Id == id)
            .FirstOrDefault();

            return Ok(jobScore);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return NotFound();
        }
    }

    // PUT api/JobScores
    [HttpPut("{id}")]
    public ActionResult Put(int id, JobScoresEditDTO jobScoresEditDTO)
    {
        try
        {
            _context.JobScores
            .Where(i => i.User_Id == id)
            .ExecuteUpdate(s => s
                .SetProperty(j => j.Software_Developer, j => jobScoresEditDTO.Software_Developer)
                .SetProperty(j => j.Web_Developer, j => jobScoresEditDTO.Web_Developer)
                .SetProperty(j => j.Mobile_App_Developer, j => jobScoresEditDTO.Mobile_App_Developer)
                .SetProperty(j => j.UI_UX_Designer, j => jobScoresEditDTO.UI_UX_Designer)
                .SetProperty(j => j.Data_Scientist, j => jobScoresEditDTO.Data_Scientist)
                .SetProperty(j => j.Machine_Learning_Engineer, j => jobScoresEditDTO.Machine_Learning_Engineer)
                .SetProperty(j => j.Network_Administrator, j => jobScoresEditDTO.Network_Administrator)
                .SetProperty(j => j.Cybersecurity_Analyst, j => jobScoresEditDTO.Cybersecurity_Analyst)
                .SetProperty(j => j.Game_Developer, j => jobScoresEditDTO.Game_Developer)
                .SetProperty(j => j.Professors, j => jobScoresEditDTO.Professors)
            );

            return Ok(jobScoresEditDTO);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
