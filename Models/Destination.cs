namespace Router.Models;

/// <summary>
/// Конечная точка отправки команды.
/// </summary>
public sealed record class Destination
{
    /// <summary>
    /// Значение назначения команды.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт экземпляр типа <see cref="Destination"/>.
    /// </summary>
    /// <param name="value">
    /// Значение назначения команды.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Если значение пустое или <see langword="null"/>.
    /// </exception>
    public Destination(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        Value = value;
    }
}
