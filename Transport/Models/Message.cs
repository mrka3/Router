using MessagePack;

namespace Router.Transport.Models;

/// <summary>
/// Сообщение.
/// </summary>
[MessagePackObject]
public sealed class Message
{
    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    [Key(0)]
    public required long Id { get; init; }

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    [Key(1)]
    public required string Value { get; init; }

    /// <summary>
    /// Тип сервиса, обрабатывающий сообщение.
    /// </summary>
    [Key(2)]
    public required long Destination { get; init; }
}