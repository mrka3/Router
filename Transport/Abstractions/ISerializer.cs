using Router.Transport.Models;

namespace Router.Transport.Abstractions;

/// <summary>
/// Представляет сериализатор команды.
/// </summary>
public interface ISerializer
{
    /// <summary>
    /// Сериализует параметры результата команды в байты.
    /// </summary>
    /// <param name="source">
    /// Параметры результата команды.
    /// </param>
    ReadOnlyMemory<byte> Serialize(CommandResultParams source);
}