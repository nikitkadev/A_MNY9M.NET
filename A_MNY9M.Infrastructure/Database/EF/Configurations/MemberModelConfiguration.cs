using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Core.Entities.Guild.Member;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class MemberModelConfiguration : IEntityTypeConfiguration<GuildMember>
{
    public void Configure(EntityTypeBuilder<GuildMember> builder)
    {
        builder.ToTable("members").HasKey(x => x.Id);
        builder.Property(prop => prop.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(prop => prop.DiscordId).HasColumnName("discord_id");
        builder.Property(prop => prop.DiscordUsername).HasColumnName("discord_username");
        builder.Property(prop => prop.GuildDisplayName).HasColumnName("guild_display_name");
        builder.Property(prop => prop.GuildDisplayAvatarUrl).HasColumnName("guild_display_avatar_url");
        builder.Property(prop => prop.JoinedAt).HasColumnName("joined_at");
    }
}