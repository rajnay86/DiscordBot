using DiscordBot.Commands.GW2.Authentication;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using GW2API.V2.Authenticated.Account.Repository;
using GW2API.V2.Misc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands.GW2.Account
{
    public class WalletCommands
    {
        [Command("get-wallet")]
        public async Task GetWallet(CommandContext ctx)
        {
            var apiKey = DatabaseAccess.GetApiKey(ctx.User.Id);
            WalletRepository walletRepo = new WalletRepository(apiKey);
            CurrencyRepository currencyRepo = new CurrencyRepository();
            var wallet = walletRepo.GetAllItems().Result;
            var currencies = currencyRepo.GetAllItems().Result;
            List<string> walletItems = new List<string>();
            foreach(var walletItem in wallet)
            {
                var name = currencies.FirstOrDefault(c => c.Id == walletItem.CurrencyId);
                walletItems.Add($"{name}: {walletItem.AmountDisplay}");
            }

            string walletValuesList = string.Join(Environment.NewLine, walletItems);
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(walletValuesList);
        }
    }
}
