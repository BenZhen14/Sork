public class Program
{
    public static void Main(string[] args)
    {
        ICommand lol = new LaughCommand();
        ICommand dance = new DanceCommand();
        ICommand sing = new SingCommand();
        ICommand whistle = new WhistleCommand();
        ICommand exit = new ExitCommand();
        List<ICommand> commands = new List<ICommand> { lol, dance, sing, whistle, exit };

        do
        {
            Console.Write(" > ");
            string input = Console.ReadLine();
            input = input.ToLower();
            input = input.Trim();

            var result = new CommandResult { RequestExit = false, IsHandled = false };
            var handled = false;
            foreach (var command in commands)
            {
                if (command.Handles(input)) 
                {
                    handled = true;
                    result = command.Execute();
                    if (result.RequestExit) { break; }
                }
            }
            if (!handled) { Console.WriteLine("Unknown command"); }
            if (result.RequestExit) { break; }

        } while (true);
    }
}