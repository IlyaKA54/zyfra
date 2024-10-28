namespace ConsoleApp1.Services.Base;

public interface ISessionService
{
    bool CheckSessionActive(string sessionId);
}
