namespace KCGameOn.Data.Models;

/// <summary>
/// For all future events titles, descriptions and more
/// 
/// </summary>
public partial class Tournament
{
    /// <summary>
    /// related to eventID from events table - can run mulitple tournaments at one event.
    /// </summary>
    public int IdEventsTable { get; set; }

    public int TournamentId { get; set; }

    /// <summary>
    /// name of the event
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// short description of what the event is, ie. knockout city, gaming convention, 21 and up only
    /// </summary>
    public string Feature { get; set; } = null!;

    /// <summary>
    /// long description of event
    /// </summary>
    public string Description { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime? Stop { get; set; }

    /// <summary>
    /// place html page is being created
    /// </summary>
    public string TournamentUrl { get; set; } = null!;

    /// <summary>
    /// swiss, pool play, single, double elimination
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// include full address
    /// </summary>
    public string? Bestof { get; set; }

    /// <summary>
    /// how many rounds are used in this tournament
    /// </summary>
    public string Rounds { get; set; } = null!;

    /// <summary>
    /// place avatar is uploaded to
    /// </summary>
    public string AvatarUrl { get; set; } = null!;

    /// <summary>
    /// place banner is uploaded to
    /// </summary>
    public string BannerUrl { get; set; } = null!;

    /// <summary>
    /// active = 1, not active - 0
    /// </summary>
    public int Kccec { get; set; }

    /// <summary>
    /// active = 1, not active - 0
    /// </summary>
    public int Active { get; set; }

    public bool AllowTeamSignups { get; set; }

    public bool AllowFreeAgentSignups { get; set; }

    public sbyte StreamedGames { get; set; }

    public virtual ICollection<TournamentMatch> TournamentMatches { get; set; } = new List<TournamentMatch>();
}
