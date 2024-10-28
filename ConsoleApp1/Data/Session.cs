namespace ConsoleApp1.Data;

public class Session
{
    public string Id { get; private set; }
    public string User { get; private set; }
    public long StartedAt { get; private set; }
    public long LifeTime { get; private set; }
    public long FinishedAt { get; private set; }

    public Session(string id, string user,long lifeTime)
    {
        Id = id;
        User = user;
        StartedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        LifeTime = lifeTime;
        FinishedAt = -1;
    }

    public bool CheckActive()
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (FinishedAt != -1 || (StartedAt + LifeTime) < currentTime)
        {
            FinishedAt = currentTime;
            return false;
        }

        return true;
    }

    public void DeactivateSession()
    {
        FinishedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}

