using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound("User Not Found");
            }
            else
            {
                return Ok(user);
            }
        }

        // POST api/Users
        [HttpPost]
        public ActionResult Post(UserDTO userDTO)
        {
            var user1 = _context.Users
                .Where(u => u.Username == userDTO.Username)
                .FirstOrDefault();

            var user2 = _context.Users
                .Where(e => e.Email == userDTO.Email)
                .FirstOrDefault();

            if (user1 is not null)
            {
                return BadRequest();
            }
            else if (user2 is not null)
            {
                return BadRequest();
            }
            else
            {
                User user = new User();
                user.Username = userDTO.Username;
                user.Password = userDTO.Password;
                user.Email = userDTO.Email;
                user.Firstname = userDTO.Firstname;
                user.Lastname = userDTO.Lastname;
                user.RoleName = "User";
                user.IsGraduated = userDTO.IsGraduated;

                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok(user);
            }
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, UserDTO userDTO)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                try
                {
                    _context.Users
                    .Where(i => i.Id == id)
                    .ExecuteUpdate(s => s
                        .SetProperty(u => u.Username, u => userDTO.Username)
                        .SetProperty(p => p.Password, p => userDTO.Password)
                        .SetProperty(f => f.Firstname, f => userDTO.Firstname)
                        .SetProperty(l => l.Lastname, f => userDTO.Lastname)
                        .SetProperty(e => e.Email, f => userDTO.Email)
                        .SetProperty(g => g.IsGraduated, g => userDTO.IsGraduated)
                    );

                    return Ok(userDTO);
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

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound("User Not Found");
            }
            else
            {
                _context.Users
                    .Where(i => i.Id.Equals(id))
                    .ExecuteDelete();

                return Ok("Deleted Successfully");
            }
        }
    }
}
