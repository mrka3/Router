using MessagePack;

namespace Router.Transport.Models;

/// <summary>
/// Результат выполнения команды.
/// </summary>
[MessagePackObject]
public sealed class CommandResultParams
{
    /// <summary>
    /// Идентификатор трассировки.
    /// </summary>
    [Key(0)]
    public required string TraceId { get; init; }

    /// <summary>
    /// Идентификатор команды.
    /// </summary>
    [Key(1)]
    public required string CommandId { get; init; }

    /// <summary>
    /// Флаг успешной обработки команды.
    /// </summary>
    [Key(2)]
    public required bool Success { get; init; }

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    [Key(3)]
    public required string? ErrorMessage { get; init; }
}
