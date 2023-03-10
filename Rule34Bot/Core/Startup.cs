using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Rule34Bot.Core
{
    internal class Startup
    {
        public static IConfigurationRoot Config { get; set; }
        static Startup()
        {
            Config = new ConfigurationBuilder()
                                                            .SetBasePath(AppContext.BaseDirectory)
                                                            .AddJsonFile("appsettings.json")
            .Build();
        }

        public static async Task StartApplication()
        {
            //await InitDataBase();

            using IHost host = Host.CreateDefaultBuilder().ConfigureServices(
                 (context, services) => ConfirurateServices(services)).Build();

            await StartBot(host);
        }

        private static async Task StartBot(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            var client = provider.GetRequiredService<DiscordSocketClient>();

            client.Log += DiscordClientOnLog;

            client.Ready += async () =>
            {
                await commands.RegisterCommandsGloballyAsync(true);
            };

            client.JoinedGuild += async (SocketGuild arg) =>
            {
                await commands.RegisterCommandsGloballyAsync(true);
            };

            await client.LoginAsync(TokenType.Bot, Config["Token"]);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private static void ConfirurateServices(IServiceCollection services)
        {
            //services.AddDbContext<BotDbContext>(options => options.UseSqlServer(BotSettings.Config["ConnectionString"]));

            services.AddSingleton(Config);

            services.AddSingleton<InteractionHandler>();

            services.AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged,
                LogGatewayIntentWarnings = false,
                AlwaysDownloadUsers = true,
                LogLevel = LogSeverity.Debug,
            }));

            services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
        }

        //private static async Task InitDataBase()
        //{
        //    using (var databaseContext = new BotDbContext(new DbContextOptions<BotDbContext>()))
        //    {
        //        await databaseContext.Database.EnsureCreatedAsync();
        //    }
        //}

        private static Task DiscordClientOnLog(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}
