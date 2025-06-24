using MessagePack;

using Router.Models;

namespace Router.Transport.Models;

/// <summary>
/// Параметры команды.
/// </summary>
[MessagePackObject]
public sealed class CommandParams
{
    /// <summary>
    /// Тип сервиса, который будет обрабатывать команду.
    /// </summary>
    [Key(0)]
    public required DestinationService CommandDestination { get; init; }
}
