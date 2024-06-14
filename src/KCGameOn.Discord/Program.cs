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

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
