using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Net.Rest;

namespace SAUSoCDiscordBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        /*[Command("ping")]
        public Task PingPong()
        {
            ReplyAsync(Context.User.Mention + " Pong!").Wait();
            return Task.CompletedTask;
        }*/

        [Command("pin")]
        [Summary("Pins the message with the given ID. Right click a message and Click copy ID to get a message's ID. Example: !pin 702212971455184967")]
        public async Task Pin(ulong messageID)
        {
            IMessage m = await Context.Channel.GetMessageAsync(messageID);
            await ((IUserMessage)m).PinAsync();
            int i = 0;
        }

        [Command("unpin")]
        [Summary("Unpins the message with the given ID. Right click a message and Click copy ID to get a message's ID. Example: !unpin 702212971455184967")]
        public async Task Unpin(ulong messageID)
        {
            IMessage m = await Context.Channel.GetMessageAsync(messageID);
            await ((IUserMessage)m).UnpinAsync();
            int i = 0;
        }

        //https://stackoverflow.com/a/52856282
        [Command("help")]
        [Summary("Lists all commands and their description.")]
        public async Task Help()
        {
            List<CommandInfo> commands = CommandHandler.Commands;
            EmbedBuilder embedBuilder = new EmbedBuilder();

            foreach (CommandInfo command in commands)
            {
                // Get the command Summary attribute information
                string embedFieldText = command.Summary ?? "No description available\n";

                embedBuilder.AddField(command.Name, embedFieldText);
            }

            await ReplyAsync("", false, embedBuilder.Build());
        }
    }
}
