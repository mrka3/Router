using Router.Transport.Models;

namespace Router.Transport.Abstractions;

/// <summary>
/// Представляет получателя команды.
/// </summary>
public interface ICommandReceiver
{
    Task<Message> ReadAsync(CancellationToken token);
}