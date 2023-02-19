using BooruSharp.Search.Post;
using Discord;
using Rule34Bot.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rule34Bot.Embeds
{
    internal class PostEmbeds
    {
        public static Embed GetPostDetales(SearchResult searchResult)
        {
            var builder = new EmbedBuilder();

            builder.AddField("Original post", searchResult.PostUrl);
            builder.AddField("Raiting", searchResult.Rating);
            builder.AddField("Size", $"{searchResult.Width}x{searchResult.Height}\n");
            builder.AddField("Tags", string.Join(' ', searchResult.Tags));

            builder.Timestamp = searchResult.Creation;
            builder.Color = EmbedsSettings.StandartColor;

            return builder.Build();
        }

        public static Embed GetSearchResultEmbed(BooruSite booruSite, int count, string[]? tags)
        {
            var builder = new EmbedBuilder();

            builder.Description = $"Found: {count} posts\n" +
                                  $"From site: {booruSite}";

            builder.Description += $"\nSearch tags: {string.Join(", ", tags)}";

            builder.Color = EmbedsSettings.StandartColor;

            return builder.Build();
        }
    }
}
