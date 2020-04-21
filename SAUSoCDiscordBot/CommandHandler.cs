using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SAUSoCDiscordBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private static CommandService _commands;
        char _commandPrefix;

        public static List<CommandInfo> Commands { get { return _commands.Commands.ToList(); } }

        public CommandHandler(DiscordSocketClient client, CommandService commands, char commandPrefix)
        {
            _commands = commands;
            _client = client;
            _commandPrefix = commandPrefix;
        }

        public async Task InstallCommandsAsync(Assembly assembly)
        {
            _client.MessageReceived += HandleCommandAsync;
            IEnumerable<ModuleInfo> moduleInfos = await _commands.AddModulesAsync(assembly: assembly,
                                            services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasCharPrefix(_commandPrefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}
