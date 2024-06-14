namespace KCGameOn.Data.Models;

public partial class TournamentMatch
{
    public int Id { get; set; }

    public int TournamentId { get; set; }

    public int? Round { get; set; }

    public string? AwayUsername { get; set; }

    public string? HomeUsername { get; set; }

    public int? AwayPoints { get; set; }

    public int? HomePoints { get; set; }

    public int? AwayTotalScore { get; set; }

    public int? HomeTotalScore { get; set; }

    public bool IsStreamed { get; set; }

    public virtual Tournament Tournament { get; set; } = null!;
}
