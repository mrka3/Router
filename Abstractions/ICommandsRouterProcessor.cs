namespace Router.Abstractions;

/// <summary>
/// Представляет маршрутизатор команд.
/// </summary>
public interface ICommandsRouterProcessor
{
    /// <summary>
    /// Перенаправляет команды во внешние системы.
    /// </summary>
    /// <param name="token">
    /// Токен отмены операции.
    /// </param>
    Task ProcessAsync(CancellationToken token);
}
