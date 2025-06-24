using ADF.Service.Commands.Router.Transport.Models;
using ADF.Service.Commands.Router.Transport.Serialization;

using FluentAssertions;

using MessagePack;

namespace ADF.Service.Commands.Router.Serialization.Tests;

public class CommandResultSerializerTests
{
    [Fact(DisplayName = $"Can create {nameof(CommandResultSerializer)}.")]
    [Trait("Category", "Unit")]
    public void CanCreate()
    {
        // Act
        var exception = Record.Exception(() => new CommandResultSerializer());

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = "Can't serialize null values.")]
    [Trait("Category","Unit")]
    public void CannotSerializeNullValues()
    {
        // Arrange
        var serializer = new CommandResultSerializer();

        // Act
        var exception = Record.Exception(() => serializer.Serialize(null!));

        //Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"Can serialize.")]
    [Trait("Category", "Unit")]
    public void CanSerialize()
    {
        // Arrange
        var commandResult = new CommandResultParams
        {
            TraceId = "test-trace-id",
            CommandId = "test-command-id",
            Success = false,
            ErrorMessage = "some error message."
        };

        var serializer = new CommandResultSerializer();
        var expectedResult = new ReadOnlyMemory<byte>(
            MessagePackSerializer.Serialize(
                commandResult,
                MessagePackOptions.SerializerOptions));

        // Act
        var result = serializer.Serialize(commandResult);

        // Assert
        result.ToArray().Should().BeEquivalentTo(
            expectedResult.ToArray(),
            opt => opt.WithStrictOrdering());
    }
}