namespace Sork.Commands;

public class WhistleCommand : BaseCommand
{
    private readonly UserInputOutput io;
    public WhistleCommand(UserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput) => GetCommandFromInput(userInput) == "whistle";
    public override CommandResult Execute() {
        io.WriteMessageLine("You whistle!");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}