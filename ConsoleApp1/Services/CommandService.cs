using ConsoleApp1.Repository;
using ConsoleApp1.Services.Base;

namespace ConsoleApp1.Services;

public class CommandService : ICommandService
{
    private readonly IRepository _repository;

    public CommandService(IRepository repository)
    {
        _repository = repository;
    }

    public string ExecuteCommand(string command)
    {
        if (command.StartsWith("delete "))
        {
            var sessionId = command.Substring("delete ".Length);

            return Delete(sessionId);
        }
        else if(command.StartsWith("deactivate "))
        {
            var sessionId = command.Substring("deactivate ".Length);

            return Deactivate(sessionId);
        }
        else
        {
            return "Неизвестная команда.";
        }
    }

    private string Delete(string sessionId)
    {
        var responce = _repository.DeleteSession(sessionId);

        if (responce)
            return $"Сессия с ID {sessionId} удалена.";

        return $"Сессии с ID {sessionId} не существует";
    }

    private string Deactivate(string sessionId)
    {
        var responce = _repository.DeactivateSession(sessionId);

        if (responce)
            return $"Сессия с ID {sessionId} деактивирована.";

        return $"Сессии с ID {sessionId} не существует";
    }
}
