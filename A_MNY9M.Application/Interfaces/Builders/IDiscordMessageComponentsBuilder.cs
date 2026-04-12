using Discord;
using Amnyam.Shared.Dtos;
using Amnyam.Shared.Results;

namespace A_MNY9M._2_Application.Interfaces.Builders;

public interface IDiscordMessageComponentsBuilder
{
    Task<BaseResult<SelectMenuBuilder>> BuildSelectionMenuAsync(SelectionMenuConfigDto config);
}
