using Sork.World;

namespace Sork.Commands;

public class LookCommand : BaseCommand
{
    private readonly UserInputOutput io;
    public LookCommand(UserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput) => GetCommandFromInput(userInput) == "look";
    public override CommandResult Execute(string userInput, GameState gameState) => new CommandResult { RequestExit = false, IsHandled = true };
}