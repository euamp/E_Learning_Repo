using E_Learning_Library.DataTransferObjects;
using E_Learning_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_LearningAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ELearningDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ELearningDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public ActionResult<User> Register(UserSignUpDTO request)
    {
        User user = new User();
        user.Firstname = request.Firstname;
        user.Lastname = request.Lastname;
        user.Email = request.Email;
        user.Username = request.Username;
        user.Password = request.Password;
        user.RoleName = "User";
        user.IsGraduated = "no";

        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost("login")]
    public ActionResult<string> Login(UserLoginDTO request)
    {
        User? user = _context.Users
            .Where(u => u.Username.Equals(request.Username))
            .FirstOrDefault();

        if (user is null)
        {
            return NotFound();
        }
        else
        {
            if (request.Password.Equals(user.Password))
            {
                var roles = user.RoleName.Split(',');

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.GivenName, user.Username),
                    new Claim(ClaimTypes.Name, user.Firstname),
                    new Claim(ClaimTypes.Surname, user.Lastname),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                        claims: claims,
                        expires: DateTime.Now.AddHours(6),
                        signingCredentials: credentials
                    );

                var jsonWebToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jsonWebToken);
            }
            else
            {
                return NotFound();
            }
        }
    }

    //public string createToken(User user)
    //{
    //    var roles = user.RoleName.Split(',');

    //    List<Claim> claims = new List<Claim>()
    //    {
    //        new Claim(ClaimTypes.GivenName, user.Username),
    //        new Claim(ClaimTypes.Name, user.Firstname),
    //        new Claim(ClaimTypes.Surname, user.Lastname),
    //        new Claim(ClaimTypes.Email, user.Email)
    //    };

    //    foreach (var role in roles)
    //    {
    //        claims.Add(new Claim(ClaimTypes.Role, role));
    //    }

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

    //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken
    //        (
    //            claims: claims,
    //            expires: DateTime.Now.AddHours(6),
    //            signingCredentials: credentials
    //        );

    //    var jsonWebToken = new JwtSecurityTokenHandler().WriteToken(token);

    //    return jsonWebToken;
    //}
}
