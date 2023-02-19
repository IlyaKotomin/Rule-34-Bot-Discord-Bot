using BooruSharp.Booru;
using Rule34Bot.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rule34Bot.Core
{
    internal static class BooruExtentions
    {
        public static ABooru GetClient(this BooruSite booruSite) => booruSite switch
        {
            BooruSite.AllTheFallen => new Atfbooru(),
            BooruSite.DanbooruDonmai => new DanbooruDonmai(),
            BooruSite.Derpibooru => new Derpibooru(),
            BooruSite.E621 => new E621(),
            BooruSite.E926 => new E926(),
            BooruSite.Konachan => new Konachan(),
            BooruSite.Lolibooru => new Lolibooru(),
            BooruSite.Ponybooru => new Ponybooru(),
            BooruSite.Realbooru => new Realbooru(),
            BooruSite.Rule34 => new Rule34(),
            BooruSite.Safebooru => new Safebooru(),
            BooruSite.Sakugabooru => new Sakugabooru(),
            BooruSite.SankakuComplex => new SankakuComplex(),
            BooruSite.Twibooru => new Twibooru(),
            BooruSite.Xbooru => new Xbooru(),
            BooruSite.Yandere => new Yandere(),
            _ => new Safebooru(),
        };
    }
}
