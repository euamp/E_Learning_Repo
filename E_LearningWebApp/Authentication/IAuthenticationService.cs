using E_LearningWebApp.Models;

namespace E_LearningWebApp.Authentication;

public interface IAuthenticationService
{
    Task<string> Login(LoginModel userForAuthentication);
    Task Logout();
    Task<string> Register(SignUpModel registerUserModel);
}
