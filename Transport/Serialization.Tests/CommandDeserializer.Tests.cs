using System.ComponentModel;

using ADF.Service.Commands.Router.Models;
using ADF.Service.Commands.Router.Models.Identifiers;
using ADF.Service.Commands.Router.Transport.Models;
using ADF.Service.Commands.Router.Transport.Serialization;

using Altenar.DataFeed.Transport.Abstractions;

using FluentAssertions;

using MessagePack;

using Moq;

namespace ADF.Service.Commands.Router.Serialization.Tests;

public class CommandDeserializerTests
{
    [Fact(DisplayName = $"Can create {nameof(CommandDeserializer)}.")]
    [Trait("Category", "Unit")]
    public void CanCreate()
    {
        // Act
        var exception = Record.Exception(() => new CommandDeserializer());

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = "Can deserialize.")]
    [Trait("Category", "Unit")]
    public void CanDeserialize()
    {
        // Arrange
        var commandParams = new CommandParams
        {
            SportId = 1,
            MatchId = 0,
            TraceId = "test-trace",
            CommandId = "test-command-id",
            CommandDestination = DestinationService.ProcessingLineLive,
            ResultTopic = "test-topic",
            Payload = "test-payload"
        };

        var bytes = MessagePackSerializer.Serialize(commandParams,
            MessagePackOptions.SerializerOptions);

        var source = new Mock<IRawCommitableMessage<string, byte[]>>(MockBehavior.Strict);
        source.Setup(s => s.Source)
            .Returns("test-source");
        source.Setup(s => s.Key)
            .Returns("test-command-id");
        source.Setup(s => s.Value)
            .Returns(bytes);

        var deserializer = new CommandDeserializer();
        var expectedMessage = new FakeMessage(
            "test-source",
            "test-trace",
            "test-command-id",
            new Command(
                new SportId(1),
                new MatchId(0),
                new CommandId("test-command-id"),
                DestinationService.ProcessingLineLive,
                new Destination("test-topic"),
                new RawCommand("test-command-id", bytes)));

        // Act
        var message = deserializer.Deserialize(source.Object);

        // Assert
        message.Should().NotBeNull().And.BeEquivalentTo(expectedMessage);
    }

    [Fact(DisplayName = "Can't deserialize if source is null.")]
    [Trait("Category", "Unit")]
    public void CanNotDeserializeIfSourceIsNull()
    {
        // Arrange
        var deserializer = new CommandDeserializer();

        // Act
        var exception = Record.Exception(() => deserializer.Deserialize(null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    private sealed record class FakeMessage(
        string Source,
        string Identifier,
        string Key,
        Command Value) :
        IStructuredCommitableMessage<string, string, string, Command>
    {
        public Task CommitAsync(CancellationToken token) =>
            throw new NotSupportedException();
    }
}