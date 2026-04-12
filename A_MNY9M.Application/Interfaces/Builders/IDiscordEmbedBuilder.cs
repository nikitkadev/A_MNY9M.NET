using Discord;
using Amnyam.Shared.Dtos;

namespace A_MNY9M._2_Application.Interfaces.Builders;

public interface IDiscordEmbedBuilder
{
    Task<Embed> BuildEmbedAsync(GuildMessageEmbedDto embedDto);
}