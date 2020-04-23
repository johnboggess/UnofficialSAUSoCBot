using System;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
namespace SAUSoCDiscordBot
{
    public class SAUSoCBot
    {
        public DiscordClient DiscordClient;
        public CommandsNextModule Commands;
        public void Start()
        {
            MainAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        async Task MainAsync()
        {
            if (!System.IO.File.Exists("secret.txt"))
            {
                throw new Exception("secret.txt was not found. Create secret.txt and put your bot's token in there. https://discord.foxbot.me/stable/guides/getting_started/first-bot.html#creating-a-discord-bot");
            }

            DiscordConfiguration discordConfiguration = new DiscordConfiguration()
            {
                Token = System.IO.File.ReadAllLines("secret.txt")[0],
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            };

            CommandsNextConfiguration commandsNext = new CommandsNextConfiguration()
            {
                StringPrefix = "!",
                CaseSensitive = false
            };
            

            DiscordClient = new DiscordClient(discordConfiguration);
            Commands = DiscordClient.UseCommandsNext(commandsNext);

            Commands.RegisterCommands<Commands>();
            
            await DiscordClient.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
