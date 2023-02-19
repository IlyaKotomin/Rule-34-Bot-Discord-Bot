using Discord;
using Rule34Bot.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rule34Bot.Embeds
{
    internal class ErrorEmbeds
    {
        public static Embed GetNSFWErrorEmbed(BooruSite site)
        {
            var builder = new EmbedBuilder();
            builder.Color = EmbedsSettings.ErrorColor;

            builder.Title = "Use NSFW channel please!";

            builder.Description = $"Booru `{site}` is not safe!" +
                                  $"\nPlease, try to use command in NSFW channel or in DM.";

            return builder.Build();
        }
    }
}
