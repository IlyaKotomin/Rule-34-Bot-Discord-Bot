using BooruSharp.Booru;
using Discord;
using Discord.Interactions;
using Rule34Bot.Core;
using Rule34Bot.Core.Enums;
using Rule34Bot.Embeds;
using System.Linq;

namespace Rule34Bot.Bot.Modules
{
    public class RandomImagesModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("images", "Get random images from selected site by tags")]
        public async Task RandomImagesModuleMethod([Summary("booru-site", "Target site where to choose pictures")]
                                                   BooruSite booruSite,
                                                   [Summary("tags", "Desired tags. Example: thuge_butt cum")]
                                                   string tags = "",
                                                   [Summary("posts-limit", "Limit of posts in the respond")]
                                                   int limit = 3)
        {
            var booruClient = booruSite.GetClient();

            if (IsNotSafeForWork(Context, booruClient, booruSite))
                return;

            var formatedTags = tags.Split(' ');

            var posts = booruClient.HasMultipleRandomAPI ?
                await booruClient.GetRandomPostsAsync(limit, formatedTags) :
                await booruClient.GetLastPostsAsync(limit, formatedTags);

            await RespondAsync(embed: PostEmbeds.GetSearchResultEmbed(booruSite, limit, formatedTags));

            foreach (var post in posts)
            {
                try
                {
                    await ReplyAsync(embed: PostEmbeds.GetPostDetales(post));
                    await ReplyAsync(post.PostUrl.AbsoluteUri);
                }
                catch(Exception) { }
            }
        }
        public static bool IsNotSafeForWork(SocketInteractionContext context, ABooru booruClient, BooruSite site)
        {
            var channel = context.Channel as ITextChannel;

            if (channel != null && !channel!.IsNsfw && !booruClient.IsSafe)
            {
                context.Interaction.RespondAsync(embed: ErrorEmbeds.GetNSFWErrorEmbed(site));
                return true;
            }
            else return false;
        }
    }
}
