namespace Sork.Commands;

public class DanceCommand : BaseCommand
{
    private readonly UserInputOutput io;
    public DanceCommand(UserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput) => GetCommandFromInput(userInput) == "dance";
    public override CommandResult Execute() {
        io.WriteMessageLine("You dance!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}