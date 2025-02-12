using Sork.World;

namespace Sork.Commands;

public class DropCommand : BaseCommand
{
    private readonly IUserInputOutput io;
    public DropCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "drop";
    }

    public override CommandResult Execute(string userInput, GameState gameState)
    {
        var parameters = GetParametersFromInput(userInput);
        if (parameters.Count() != 1)
        {
            io.WriteMessageLine("Please specify one item to drop.");
            return new CommandResult { RequestExit = false, IsHandled = true };
        }
        var noun = parameters[0];

        var item = gameState.Player.Inventory.FirstOrDefault(i => 
            i.Name.Equals(noun, StringComparison.OrdinalIgnoreCase));

        if (item == null)
        {
            io.WriteMessageLine($"You don't have a {noun}.");
            return new CommandResult { RequestExit = false, IsHandled = true };
        }

        gameState.Player.Inventory.Remove(item);
        gameState.Player.Location.Inventory.Add(item);
        io.WriteMessageLine($"You dropped the {item.Name}.");

        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}