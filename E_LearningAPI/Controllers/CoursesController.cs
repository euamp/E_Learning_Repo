using E_Learning_Library.Models;
using Microsoft.AspNetCore.Mvc;

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
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/Courses/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/Courses
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/Courses/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/Courses/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
