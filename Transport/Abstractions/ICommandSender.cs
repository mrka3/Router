namespace Router.Transport.Abstractions;

/// <summary>
/// Отправитель команды во внешние сервисы.
/// </summary>
public interface ICommandSender
{
    /// <summary>
    /// Отправляет команду
    /// </summary>
    /// <param name="message"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task SendAsync(IMessage message, CancellationToken token);
}