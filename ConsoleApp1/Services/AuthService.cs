using ConsoleApp1.Repository;
using ConsoleApp1.Services.Base;

namespace ConsoleApp1.Services;

public class AuthService : IAuthService
{
    private readonly IRepository _repository;
    private readonly long _sessionLifetime;

    public AuthService(IRepository repository, long sessionLifetime)
    {
        _repository = repository;
        _sessionLifetime = sessionLifetime;
    }

    public Response Authenticate(string login, string password)
    {
        if (_repository.IsValidUser(login, password))
        {
           var response = _repository.AddSession(login, _sessionLifetime);
           return response;
        }

        return new Response(
            false,
            "Такого пользователя не существует"
            );
    }
}
