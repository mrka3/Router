namespace Router.Models;

/// <summary>
/// Тип сервиса, который будет обрабатывать команду.
/// </summary>
public enum DestinationService
{
    /// <summary>
    /// Значение по умолчанию.
    /// </summary>
    None = 0,

    /// <summary>
    /// Сервис обработки.
    /// </summary>
    Processing = 1,
}