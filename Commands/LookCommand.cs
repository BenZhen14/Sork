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
    public override CommandResult Execute(string userInput, GameState gameState)
    {
        var location = gameState.Player.Location;
        io.WriteMessageLine(location.Description);
        if (location.Exits.Count > 0)
        {
            io.WriteMessage("You can go in any of these directions to exit: ");
            foreach (var exit in location.Exits)
            {
                io.WriteMessageLine($"- {exit.Key}");
            }
        }
        else
        {
            io.WriteMessageLine("There are no exits from this location.");
        }
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}