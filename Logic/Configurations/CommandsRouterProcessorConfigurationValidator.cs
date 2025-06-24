
using Microsoft.Extensions.Options;

namespace Commands.Router.Logic.Configurations;

/// <summary>
/// Валидатор для <see cref="CommandsRouterProcessorConfiguration"/>.
/// </summary>
[OptionsValidator]
public sealed partial class CommandsRouterProcessorConfigurationValidator :
    IValidateOptions<CommandsRouterProcessorConfiguration>
{
}
