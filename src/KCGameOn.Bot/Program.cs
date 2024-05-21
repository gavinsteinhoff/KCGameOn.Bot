using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using KCGameOn.Bot.Data;
using KCGameOn.Bot.Interactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KCGameOn;
public class Program
{
    private static IConfiguration? _configuration;
    private static IServiceProvider? _services;

    private static async Task Main(string[] args)
    {
        _configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddJsonFile("appsettings.Development.json")
           .Build();

        var databaseConnectionString = _configuration.GetConnectionString("KCGameOnDatabase");
        if (databaseConnectionString is null)
            return;

        var botToken = _configuration.GetSection("Settings").GetValue<string>("DiscordBotToken");
        if (botToken is null)
            return;

        _services = new ServiceCollection()
         .AddSingleton(_configuration)
         .AddSingleton<DiscordSocketClient>()
         .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
         .AddSingleton<InteractionHandler>()
         .AddDbContext<KCGameOnContext>(options => options.UseMySQL())
         .BuildServiceProvider();

        var client = _services.GetRequiredService<DiscordSocketClient>();

        client.Log += LogAsync;

        await _services.GetRequiredService<InteractionHandler>().InitializeAsync();

        await client.LoginAsync(TokenType.Bot, botToken);
        await client.StartAsync();
        await Task.Delay(Timeout.Infinite);
    }

    private static Task LogAsync(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}