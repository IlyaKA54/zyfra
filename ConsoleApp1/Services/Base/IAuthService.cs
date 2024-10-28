using ConsoleApp1.Repository;

namespace ConsoleApp1.Services.Base;

public interface IAuthService
{
    Response Authenticate(string login, string password);
}
