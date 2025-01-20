using Sork.World;

namespace Sork.Commands;

public class LookCommand : BaseCommand
{
    private readonly IUserInputOutput io;
    public LookCommand(IUserInputOutput io)
    {
        this.io = io;
    }
    public override bool Handles(string userInput) => GetCommandFromInput(userInput) == "look";
    public override CommandResult Execute(string userInput, GameState gameState)
    {
        var location = gameState.Player.Location;
        io.WriteMessageLine($"Location: {location.Name} - {location.Description}");
        io.WriteMessageLine("");
        if (location.Exits.Count > 0)
        {
            io.WriteMessageLine("You can go in any of these directions to exit: ");
            foreach (var exit in location.Exits)
            {
                io.WriteMessageLine($"{exit.Key} - {exit.Value.Description}");
            }
        }
        else
        {
            io.WriteMessageLine("There are no exits from this location.");
        }
        io.WriteMessageLine("");
        io.WriteMessageLine("Inventory:");
        foreach (var item in gameState.Player.Location.Inventory)
        {
            io.WriteMessageLine($"{item.Name} - {item.Description}");
        }
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}