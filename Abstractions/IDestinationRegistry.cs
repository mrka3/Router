using Router.Models;

namespace Router.Abstractions;

/// <summary>
/// Представляет сервис для определения маршрута.
/// </summary>
public interface IDestinationRegistry
{
    /// <summary>
    /// Получить топик для команды.
    /// </summary>
    /// <param name="id">
    /// Идентификатор.
    /// </param>
    /// <param name="destination">
    /// Тип сервиса, обрабатывающий сообщение.
    /// </param>
    /// <returns>
    /// Название топика.
    /// </returns>
    Destination GetDestination(long id, long destination);
}
