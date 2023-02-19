using Discord.Interactions;

namespace Rule34Bot.Bot.Modules
{
    public class PingModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("ping", "description og command")]
        public async Task PingModuleMethod()
        {
            var ping = Math.Abs(((DateTimeOffset)DateTime.Now - Context.Interaction.CreatedAt).Milliseconds);

            await RespondAsync($"Pong! Current bot ping: {ping} ms");
        }
    }
}
