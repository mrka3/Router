using Microsoft.Extensions.Logging;

using Router.Abstractions;
using Router.Models;
using Router.Transport.Abstractions;

namespace Commands.Router.Logic;

/// <summary>
/// Представляет маршрутизатор команд.
/// </summary>
public sealed class CommandsRouterProcessor : ICommandsRouterProcessor
{
    /// <summary>
    /// Создаёт экземпляр типа <see cref="CommandsRouterProcessor"/>.
    /// </summary>
    /// <param name="commandsReceiver">
    /// Получатель команд в том виде, в котором предоставляет инфраструктура.
    /// </param>
    /// <param name="commandSender">
    /// Отправитель команд.
    /// </param>
    /// <param name="destinationRegistry">
    /// Сервис маршрутизации.
    /// </param>
    /// <param name="commandResultSender">
    /// Отправитель результатов процессинга команд.
    /// </param>
    /// <param name="logger">
    /// Логгер.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из аргументов равен <see langword="null"/>.
    /// </exception>
    public CommandsRouterProcessor(
        ICommandReceiver commandsReceiver,
        ICommandSender commandSender,
        ICommandResultSender commandResultSender,
        IDestinationRegistry destinationRegistry,
        ILogger<CommandsRouterProcessor> logger)
    {
        ArgumentNullException.ThrowIfNull(commandsReceiver);
        ArgumentNullException.ThrowIfNull(commandSender);
        ArgumentNullException.ThrowIfNull(commandResultSender);
        ArgumentNullException.ThrowIfNull(destinationRegistry);
        ArgumentNullException.ThrowIfNull(logger);

        _commandsReceiver = commandsReceiver;
        _commandSender = commandSender;
        _commandResultSender = commandResultSender;
        _destinationRegistry = destinationRegistry;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task ProcessAsync(CancellationToken token)
    {
        try
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                await ProcessMessageAsync(token);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Processing cancelled");
            throw;
        }
    }

    private async Task ProcessMessageAsync(CancellationToken token)
    {
        var message = await _commandsReceiver.ReadAsync(token).ConfigureAwait(false);

        using var loggingScope =
            _logger.BeginScope(
                "Command routing scope {CommandId} - {@Command}",
                message.Id,
                message.Value);

        _logger.LogInformation("Starting command routing");

        try
        {
            var destination =
                _destinationRegistry.GetDestination(message.Id, message.Destination);

            _logger.LogDebug(
                "Destination defined - {Destination}",
                destination.Value);

            var commandMessage =
                new TargetedMessage<string>(message.Id, message.Value, destination);

            await _commandSender.SendAsync(commandMessage, token)
                .ConfigureAwait(false);

            _logger.LogInformation(
                "Command {CommandId} has been routed to {Destination}",
                message.Id,
                destination.Value);
        }
        catch (Exception ex) when
            (ex is InvalidOperationException
                or ArgumentNullException)
        {
            _logger.LogError(ex, "Error occured on routing command");

            // todo: обрабатываем ошибку
        }
        finally
        {
            if (!token.IsCancellationRequested)
            {
                // todo: коммитим сообщение
            }
        }
    }

    private sealed record class TargetedMessage<TValue>(
        long Key,
        TValue Value,
        Destination Destination) : IMessage;

    private readonly ICommandSender _commandSender;
    private readonly ICommandResultSender _commandResultSender;
    private readonly ICommandReceiver _commandsReceiver;
    private readonly IDestinationRegistry _destinationRegistry;
    private readonly ILogger<CommandsRouterProcessor> _logger;
}