using System.ComponentModel.DataAnnotations;

using Router.Models;

namespace Commands.Router.Logic.Configurations;

/// <summary>
/// Конфигурация маршрутизатора команд.
/// </summary>
public sealed class CommandsRouterProcessorConfiguration
{
    /// <summary>
    /// Список маршрутов.
    /// </summary>
    [Required]
    [MinLength(1)]
    public required IReadOnlyDictionary<int, IReadOnlyDictionary<DestinationService, string>> Destinations { get; init; }
}
