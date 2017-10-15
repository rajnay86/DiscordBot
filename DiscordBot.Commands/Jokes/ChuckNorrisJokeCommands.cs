using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands.Jokes
{

    public class ChuckNorrisJokeResult<T>
    {
        public string type { get; set; }
        public T value { get; set; }
    }

    public class ChuckNorrisJoke
    {
        public int id { get; set; }
        public string joke { get; set; }
        public List<string> categories { get; set; }
    }

    public class ChuckNorrisJokeCommands
    {
        [Command("chucknorrisjoke")]
        public async Task GetChuckNorrisJoke(CommandContext ctx)
        {
            var client = new RestClient("http://api.icndb.com/jokes");
            var request = new RestRequest("random");
            var response = client.Execute<ChuckNorrisJokeResult<ChuckNorrisJoke>>(request);
            var joke = response.Data.value;
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(joke.joke);
        }
    }
}
