using ConsoleApp1.Data;
using Newtonsoft.Json;

namespace ConsoleApp1.Repository;

internal class FileRepository : IRepository
{
    private readonly string _usersFilePath;
    private readonly string _sessionsFilePath;

    private Dictionary<string, string> _users;
    private Dictionary<string, Session> _sessions;

    public FileRepository(string usersFilePath, string sessionsFilePath)
    {
        _usersFilePath = usersFilePath;
        _sessionsFilePath = sessionsFilePath;

        LoadUserData();
        LoadSessions();
    }

    public bool IsValidUser(string login, string password)
    {
        return _users.TryGetValue(login, out var storedPassword) && storedPassword == password;
    }

    public Response AddSession(string login, long lifeTime)
    {
        var sessionId = Guid.NewGuid().ToString();

        var activeSession = _sessions.FirstOrDefault(s => s.Value.User == login && s.Value.CheckActive());

        if (activeSession.Value != null)
        {
            return new Response(
                false,
                $"У пользователя уже есть активная сессия {activeSession.Value.Id}"
                );
        }

        var session = new Session(sessionId, login, lifeTime);
        _sessions[sessionId] = session;
        SaveSessions();
        return new Response(
                true,
                "Аутентификация прошла успешно: " + sessionId
                );
    }


    public bool DeleteSession(string sessionId)
    {
        var value = false;

        if (_sessions.Remove(sessionId))
        {
            SaveSessions();
            value = true;
        }

        return value;
    }

    public bool DeactivateSession(string sessionId)
    {
        var value = false;

        if (_sessions.Values.Any(s => s.Id == sessionId && s.CheckActive()))
        {
            _sessions[sessionId].DeactivateSession();
            SaveSessions();
            value = true;
        }

        return value;
    }

    public bool SessionExists(string sessionId)
    {
        if (_sessions.TryGetValue(sessionId, out var session) && session.CheckActive())
            return true;

        return false;
    }

    private void LoadUserData()
    {
        if (File.Exists(_usersFilePath))
        {
            var json = File.ReadAllText(_usersFilePath);
            _users = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }
        else
        {
            Console.WriteLine("Файл с данными пользователей не найден.");
            Environment.Exit(1);
        }
    }

    private void LoadSessions()
    {
        if (File.Exists(_sessionsFilePath))
        {
            var json = File.ReadAllText(_sessionsFilePath);
            _sessions = JsonConvert.DeserializeObject<Dictionary<string, Session>>(json) ?? new Dictionary<string, Session>();
        }
        else
        {
            _sessions = new Dictionary<string, Session>();
        }
    }

    private void SaveSessions()
    {
        var json = JsonConvert.SerializeObject(_sessions);
        File.WriteAllText(_sessionsFilePath, json);
    }
}
