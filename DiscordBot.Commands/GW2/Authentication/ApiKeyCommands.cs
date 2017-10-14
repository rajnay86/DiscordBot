using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using SQLite;

namespace DiscordBot.Commands.GW2.Authentication
{
    public class ApiKeyCommands
    {
        [Command("add-key")]
        [Description("Add your api key")]
        public async Task AddKey(CommandContext ctx, string key)
        {
            bool success = DatabaseAccess.AddApiKey(ctx.User.Id, key);
            await ctx.TriggerTypingAsync();
            string msg;
            if (success)
            {
                msg = "Key added";
            }
            else
            {
                msg = "You already have a key setup. Delete it with delete-key if you want to add a new one";
            }
            await ctx.RespondAsync(msg);
        }

        [Command("delete-key")]
        [Description("Delete your api key")]
        public async Task DeleteKey(CommandContext ctx)
        {
            DatabaseAccess.DeleteApiKey(ctx.User.Id);
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("Key deleted");
        }
    }

    public class DatabaseAccess
    {
        private static string dbPath = "data.db";
        public static void Init()
        {
            if (!System.IO.File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<User>();
                db.Close();
            }
        }
        public static bool AddApiKey(ulong id, string apiKey)
        {
            Init();
            var db = new SQLiteConnection(dbPath);
            try
            {
                db.Insert(new User
                {
                    Id = id.ToString(),
                    ApiKey = apiKey
                });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string GetApiKey(ulong id)
        {
            Init();
            var db = new SQLiteConnection(dbPath);
            var query = db.Table<User>().Where(u => u.Id == id.ToString());
            var user = query.FirstOrDefault();
            return user?.ApiKey;
        }

        public static bool DeleteApiKey(ulong id)
        {
            Init();
            var db = new SQLiteConnection(dbPath);
            try
            {
                db.Delete<User>(id.ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string ApiKey { get; set; }
    }
}
