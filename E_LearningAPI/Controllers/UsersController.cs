using E_Learning_Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ELearningDbContext _context;

        public UsersController(ELearningDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var users = _context.Users.ToList();
            return users;
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
