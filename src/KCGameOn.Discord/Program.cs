using KCGameOn.Data;
using KCGameOn.Discord.Core.Interactions;
using Timbn.Discord;

var builder = WebApplication.CreateBuilder(args);

var timbnDiscordSettings = builder.Configuration.GetRequiredSection(nameof(TimbnDiscordSettings)).Get<TimbnDiscordSettings>();
if (timbnDiscordSettings is null)
    throw new ArgumentNullException("TimbnDiscordSettings");

var databaseConnectionString = builder.Configuration.GetConnectionString("KCGameOnDatabase");
if (databaseConnectionString is null)
    throw new ArgumentNullException("KCGameOnDatabase");

builder.Services
    .AddKCGameOnData(databaseConnectionString)
    .AddTimbnDiscord<InteractionHandler>(options =>
    {
        options.DiscordBotToken = timbnDiscordSettings.DiscordBotToken;
        options.DevDiscordGuidId = timbnDiscordSettings.DevDiscordGuidId;
    });

var app = builder.Build();
await app.RunTimbnDiscordAsync();
app.UseHttpsRedirection();
app.Run();
