using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using GW2API.V2.Misc.Repository;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Commands.GW2.Misc
{
    public class QuagganCommands
    {
        QuagganRepository repo = new QuagganRepository();
        [Command("list-quaggans")]
        [Description("Gets list of available quaggan images")]
        [Aliases("lq")]
        public async Task ListQuaggans(CommandContext ctx)
        {
            var quaggans = repo.GetIds().Result;
            string quaglist = string.Join(Environment.NewLine, quaggans);
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(quaglist);
        }

        [Command("show-quaggan")]
        [Description("Shows the selected quaggan")]
        [Aliases("sq")]
        public async Task ShowQuaggan(CommandContext ctx, [Description("Name of quaggan")] string name)
        {
            var quaggan = repo.GetSingleItem(name).Result;
            var text = quaggan.Url;
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(text);
        }

        [Command("random-quaggan")]
        [Description("Shows the selected quaggan")]
        [Aliases("rq")]
        public async Task RandomQuaggan(CommandContext ctx)
        {
            var quaggans = repo.GetAllItems().Result;
            var random = new Random();
            int r = random.Next(quaggans.Count - 1);
            var quaggan = quaggans[r];
            var text = quaggan.Url;
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(text);
        }
    }
}
