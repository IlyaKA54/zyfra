namespace ConsoleApp1.Repository;

public interface IRepository
{
    bool IsValidUser(string login, string password);
    Response AddSession(string login, long lifeTime);
    bool DeleteSession(string sessionId);
    bool SessionExists(string sessionId);
    bool DeactivateSession(string sessionId);
}




