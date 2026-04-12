using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using A_MNY9M.Core.Entities.Guild.Member;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class GuildMemberConfiguration : IEntityTypeConfiguration<GuildMember>
{
    public void Configure(EntityTypeBuilder<GuildMember> builder)
    {
        builder.ToTable("guild_members")
            .HasKey(x => x.Id);


        builder.Property(prop => prop.DiscordId)
            .HasColumnName("discord_id");

        builder.Property(prop => prop.IsGuildAuthorized)
            .HasColumnName("is_guild_authorized");

        builder.Property(prop => prop.DiscordUsername)
            .HasColumnName("username").HasMaxLength(32);

        builder.Property(prop => prop.GuildDisplayName)
            .HasColumnName("display_name").HasMaxLength(32);

        builder.Property(prop => prop.GuildDisplayAvatarUrl)
            .HasColumnName("display_avatar_id").HasMaxLength(255);

        builder.Property(prop => prop.JoinedAt)
            .HasColumnName("joined_at");
    }
}