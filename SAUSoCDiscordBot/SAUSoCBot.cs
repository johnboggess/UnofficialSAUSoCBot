using System;
using System.Threading.Tasks;
using System.Reflection;

using Discord;
using Discord.WebSocket;

namespace SAUSoCDiscordBot
{
    public class SAUSoCBot
    {
        char _commandPrefix;
        DiscordSocketClient _discordSocketClient = new DiscordSocketClient();
        CommandHandler _commandHandler;
        
        public void Start(char commandPrefix)
        {
            _commandPrefix = commandPrefix;
            Main().GetAwaiter().GetResult();
        }

        private async Task Main()
        {
            if(!System.IO.File.Exists("secret.txt"))
            {
                throw new Exception("secret.txt was not found. Create secret.txt and put your bot's token in there. https://discord.foxbot.me/stable/guides/getting_started/first-bot.html#creating-a-discord-bot");
            }

            _discordSocketClient = new DiscordSocketClient();
            _discordSocketClient.Log += Log;
            await _discordSocketClient.LoginAsync(TokenType.Bot, System.IO.File.ReadAllLines("secret.txt")[0]);
            await _discordSocketClient.StartAsync();

            _commandHandler = new CommandHandler(_discordSocketClient, new Discord.Commands.CommandService(), _commandPrefix);
            await _commandHandler.InstallCommandsAsync(Assembly.GetExecutingAssembly());

            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
