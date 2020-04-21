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
            _discordSocketClient = new DiscordSocketClient();
            _discordSocketClient.Log += Log;
            await _discordSocketClient.LoginAsync(TokenType.Bot, System.IO.File.ReadAllText("secret.txt"));
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
