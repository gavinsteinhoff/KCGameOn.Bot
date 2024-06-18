using Discord;
using Discord.Interactions;
using KCGameOn.Data;
using Microsoft.EntityFrameworkCore;

namespace KCGameOn.Discord.Core.Interactions.Tournament;

[Group("tournament", "Tournament Commands")]
public class TournamentModule : InteractionModuleBase
{
    private readonly KCGameOnContext _context;

    public TournamentModule(KCGameOnContext context)
    {
        _context = context;
    }

    [SlashCommand("schedule", "Shows the schedule for the week.")]
    public async Task Schedule(int round, int tournamentId)
    {
        var tournament = await _context
            .Tournaments
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.IdEventsTable == 1009 && x.TournamentId == tournamentId);

        if (tournament is null)
        {
            await RespondAsync("Invalid Tournament", ephemeral: true);
            return;
        }

        var matches = await _context
        .TournamentMatches
        .AsNoTracking()
        .Where(x =>
            x.TournamentId == tournament.TournamentId &&
            x.Round == round
        )
        .ToListAsync();

        if (matches is null || !matches.Any())
        {
            await RespondAsync("Tournament has no matches.", ephemeral: true);
            return;
        }

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
