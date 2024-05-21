using Discord;
using Discord.Interactions;
using KCGameOn.Bot.Data;
using Microsoft.EntityFrameworkCore;

namespace KCGameOn.Bot.Interactions.Tournament;

[Group("tournament", "Tournament Commands")]
public class TournamentModule : InteractionModuleBase
{
    private readonly KCGameOnContext _context;

    public TournamentModule(KCGameOnContext context)
    {
        _context = context;
    }

    [SlashCommand("ping", "Ping Pong")]
    public async Task Ping([Summary(description: "Message to Pong")] string message = "")
    {
        await RespondAsync($"Pong {message}", ephemeral: true);
    }

    [SlashCommand("schedule", "Shows the schedule for the week.")]
    public async Task Schedule(int round, int tournamentId)
    {
        var tournament = await _context
            .Tournaments
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.IdEventsTable == 1009 && x.TournamentId == tournamentId);

        if (tournament is null)
            return;

        var matches = await _context
            .TournamentMatches
            .AsNoTracking()
            .Where(x =>
                x.TournamentId == tournament.TournamentId &&
                x.Round == round
            )
            .ToListAsync();

        if (matches is null || !matches.Any())
            return;

        var embeds = new List<Embed>();
        foreach (var item in matches)
        {
            var embed = new EmbedBuilder();
            embed.AddField("Home", item.HomeUsername);
            embed.AddField("Away", item.AwayUsername);
            embeds.Add(embed.Build());
        }

        await RespondAsync($"Matches for round {round} of {tournament.Name} for {tournament.Feature}", embeds: embeds.ToArray(), ephemeral: true);
    }
}
