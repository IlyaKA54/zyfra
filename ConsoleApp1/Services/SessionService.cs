using ConsoleApp1.Repository;
using ConsoleApp1.Services.Base;

namespace ConsoleApp1.Services;

public class SessionService : ISessionService
{
    private readonly IRepository _repository;

    public SessionService(IRepository repository)
    {
        _repository = repository;
    }

    public bool CheckSessionActive(string sessionId)
    {
        return _repository.SessionExists(sessionId);
    }

}
