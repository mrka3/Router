using MessagePack;

namespace Router.Transport.Serialization;

/// <summary>
/// Содержит общие настройки MessagePack.
/// </summary>
public static class MessagePackOptions
{
    /// <summary>
    /// Настройки сериализации MessagePack.
    /// </summary>
    public static MessagePackSerializerOptions SerializerOptions { get; } =
        MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);
}
