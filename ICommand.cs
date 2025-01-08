using Sork.World;

namespace Sork;

public interface ICommand
{
    bool Handles(string userInput);
    CommandResult Execute(string userInput, GameState gameState);
}