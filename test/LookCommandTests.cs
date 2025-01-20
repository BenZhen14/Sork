using Sork.Commands;
using Sork.World;

namespace Sork.Tests;

[TestClass]
public sealed class LookCommandTests
{
    [TestMethod]
    public void Handle_ShouldReturnTrue_WhenInputIsLook()
    {
        // Arrange
        var command = new LookCommand(new UserInputOutput());

        // Act
        var result = command.Handles("look");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Execute_ShouldOutputLocationDescription_WhenCalled()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new LookCommand(io);
        var gameState = GameState.Create(io);
        gameState.Player.Location = new Room
        {
            Name = "Test Room",
            Description = "A room for testing."
        };

        // Act
        var result = command.Execute("look", gameState);

        // Assert
        Assert.IsTrue(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual(5, io.Outputs.Count);
        Assert.AreEqual("Location: Test Room - A room for testing.", io.Outputs[0]);
        Assert.AreEqual("", io.Outputs[1]);
        Assert.AreEqual("There are no exits from this location.", io.Outputs[2]);
    }

    [TestMethod]
    public void Execute_ShouldOutputExits_WhenExitsAreAvailable()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new LookCommand(io);
        var gameState = GameState.Create(io);
        var room = new Room
        {
            Name = "Test Room",
            Description = "A room for testing."
        };
        room.Exits.Add("north", new Room { Name = "North Room", Description = "A room to the north." });
        gameState.Player.Location = room;

        // Act
        var result = command.Execute("look", gameState);

        // Assert
        Assert.IsTrue(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual(6, io.Outputs.Count);
        Assert.AreEqual("Location: Test Room - A room for testing.", io.Outputs[0]);
        Assert.AreEqual("", io.Outputs[1]);
        Assert.AreEqual("You can go in any of these directions to exit: ", io.Outputs[2]);
        Assert.AreEqual("north - A room to the north.", io.Outputs[3]);
        Assert.AreEqual("", io.Outputs[4]);
        Assert.AreEqual("Inventory:", io.Outputs[5]);
    }
}
