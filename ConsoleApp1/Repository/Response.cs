namespace ConsoleApp1.Repository;

public class Response
{
    public bool Status { get; private set; }
    public string Msg { get; private set; }

    public Response(bool status, string msg)
    {
        Status = status;
        Msg = msg;
    }
}
