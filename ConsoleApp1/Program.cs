using ConsoleApp1.Repository;
using ConsoleApp1.Services.Base;
using ConsoleApp1.Services;

class Program
{
    private const int _lifeTime = 36000;

    private static IAuthService _authService;
    private static ICommandService _commandService;
    private static ISessionService _sessionService;
    private static IRepository _repository;

    static void Main(string[] args)
    {
        _repository = new FileRepository("Files/users.json", "Files/sessions.json");
        _authService = new AuthService(_repository, _lifeTime);
        _commandService = new CommandService(_repository);
        _sessionService = new SessionService(_repository);

        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Аутентификация");
            Console.WriteLine("2. Проверить активность сессии");
            Console.WriteLine("3. Ввести команду");
            Console.WriteLine("4. Выйти");

            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine(Authenticate().Msg);
                    break;
                case "2":
                    Console.WriteLine(CheckSession());
                    break;
                case "3":
                    Console.WriteLine(ExecuteCommand());
                    break;
                case "4":
                    exit = true;
                    Console.WriteLine("Выход из программы...");
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
            }
        }
    }

    static Response Authenticate()
    {
        Console.Write("Введите логин: ");
        string login = Console.ReadLine();
        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();

        var response = _authService.Authenticate(login, password);

        return response;
    }

    static string CheckSession()
    {
        Console.Write("Введите ID сессии: ");
        string sessionId = Console.ReadLine();

        if (_sessionService.CheckSessionActive(sessionId))
            return "Сессия активна.";
        else
            return "Сессия неактивна или не существует.";
    }

    static string ExecuteCommand()
    {
        Console.Write("Введите команду: ");
        string command = Console.ReadLine();

        return _commandService.ExecuteCommand(command);
    }

}