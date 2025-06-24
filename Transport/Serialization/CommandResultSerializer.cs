using MessagePack;

using Router.Transport.Abstractions;
using Router.Transport.Models;

namespace Router.Transport.Serialization;

/// <summary>
/// Представляет сериализатор результата выполнения команды.
/// </summary>
public sealed class CommandResultSerializer: ISerializer
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="source"/> равен <see langword="null"/>.
    /// </exception>
    public ReadOnlyMemory<byte> Serialize(CommandResultParams source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return MessagePackSerializer.Serialize(source, MessagePackOptions.SerializerOptions);
    }
}
