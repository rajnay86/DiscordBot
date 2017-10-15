using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.Commands.Jokes
{
    public class DadJoke
    {
        public string id { get; set; }
        public string joke { get; set; }
        public int status { get; set; }
    }

    public class JokeCommands
    {
        [Command("dadjoke")]
        public async Task GetDadJoke(CommandContext ctx)
        {
            var client = new RestClient("https://icanhazdadjoke.com");
            var request = new RestRequest();
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var response = client.Execute< DadJoke>(request);
            var dadJoke = response.Data;
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(dadJoke.joke);
        }
    }
}
