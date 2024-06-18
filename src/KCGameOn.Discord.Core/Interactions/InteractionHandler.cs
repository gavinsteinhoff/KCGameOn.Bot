using Discord.Interactions;
using Discord.WebSocket;
using Timbn.Discord.Interactions;

namespace KCGameOn.Discord.Core.Interactions;

public class InteractionHandler : TimbnInteractionHandler, ITimbnInteractionHandler
{
    public InteractionHandler(DiscordSocketClient client, InteractionService handler, IServiceProvider services) : base(client, handler, services)
    {
    }
}
