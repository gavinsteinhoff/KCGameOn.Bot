using KCGameOn.Bot.Models;
using Microsoft.EntityFrameworkCore;

namespace KCGameOn.Bot.Data;

public partial class KCGameOnContext : DbContext
{
    public KCGameOnContext()
    {
    }

    public KCGameOnContext(DbContextOptions<KCGameOnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<TournamentMatch> TournamentMatches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.TournamentId).HasName("PRIMARY");

            entity.ToTable("tournaments", tb => tb.HasComment("For all future events titles, descriptions and more\r\n"));

            entity.Property(e => e.TournamentId).HasColumnName("tournamentID");
            entity.Property(e => e.Active)
                .HasComment("active = 1, not active - 0")
                .HasColumnName("active");
            entity.Property(e => e.AllowFreeAgentSignups).HasColumnName("allowFreeAgentSignups");
            entity.Property(e => e.AllowTeamSignups).HasColumnName("allowTeamSignups");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(100)
                .HasComment("place avatar is uploaded to")
                .HasColumnName("avatarURL");
            entity.Property(e => e.BannerUrl)
                .HasMaxLength(100)
                .HasComment("place banner is uploaded to")
                .HasColumnName("bannerURL");
            entity.Property(e => e.Bestof)
                .HasMaxLength(100)
                .HasComment("include full address")
                .HasColumnName("bestof");
            entity.Property(e => e.Description)
                .HasMaxLength(1500)
                .HasComment("long description of event")
                .HasColumnName("description");
            entity.Property(e => e.Feature)
                .HasMaxLength(200)
                .HasComment("short description of what the event is, ie. knockout city, gaming convention, 21 and up only")
                .HasColumnName("feature");
            entity.Property(e => e.IdEventsTable).HasComment("related to eventID from events table - can run mulitple tournaments at one event.");
            entity.Property(e => e.Kccec)
                .HasComment("active = 1, not active - 0")
                .HasColumnName("kccec");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("name of the event")
                .HasColumnName("name");
            entity.Property(e => e.Rounds)
                .HasMaxLength(100)
                .HasComment("how many rounds are used in this tournament")
                .HasColumnName("rounds");
            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("start");
            entity.Property(e => e.Stop)
                .HasColumnType("datetime")
                .HasColumnName("stop");
            entity.Property(e => e.StreamedGames).HasColumnName("streamedGames");
            entity.Property(e => e.Style)
                .HasMaxLength(100)
                .HasComment("swiss, pool play, single, double elimination")
                .HasColumnName("style");
            entity.Property(e => e.TournamentUrl)
                .HasMaxLength(200)
                .HasComment("place html page is being created")
                .HasColumnName("tournamentURL");
        });

        modelBuilder.Entity<TournamentMatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tournamentMatch");

            entity.HasIndex(e => e.TournamentId, "FK_tournamentMatch_tournaments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AwayPoints).HasColumnName("awayPoints");
            entity.Property(e => e.AwayTotalScore).HasColumnName("awayTotalScore");
            entity.Property(e => e.AwayUsername)
                .HasMaxLength(100)
                .HasColumnName("awayUsername");
            entity.Property(e => e.HomePoints).HasColumnName("homePoints");
            entity.Property(e => e.HomeTotalScore).HasColumnName("homeTotalScore");
            entity.Property(e => e.HomeUsername)
                .HasMaxLength(100)
                .HasColumnName("homeUsername");
            entity.Property(e => e.IsStreamed).HasColumnName("isStreamed");
            entity.Property(e => e.Round).HasColumnName("round");
            entity.Property(e => e.TournamentId).HasColumnName("tournamentID");

            entity.HasOne(d => d.Tournament).WithMany(p => p.TournamentMatches)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tournamentMatch_tournaments");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
