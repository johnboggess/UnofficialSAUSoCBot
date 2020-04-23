using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace SAUSoCDiscordBot
{
    public class Commands
    {
        /*[Command("ping")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync(ctx.User.Mention+" Pong!");
        }*/

        [Command("pin")]
        [Description("Pins the message with the given ID. Right click a message and Click copy ID to get a message's ID. Example: !pin 702212971455184967")]
        public async Task Pin(CommandContext ctx, ulong messageID)
        {
            DiscordMessage msg = await ctx.Channel.GetMessageAsync(messageID);
            await msg.PinAsync();
        }

        [Command("unpin")]
        [Description("Unpins the message with the given ID. Right click a message and click copy ID to get a message's ID. Example: !unpin 702212971455184967")]
        public async Task Unpin(CommandContext ctx, ulong messageID)
        {

            DiscordMessage msg = await ctx.Channel.GetMessageAsync(messageID);
            await msg.UnpinAsync();
        }

        [Command("source")]
        [Description("Link to the source code on github")]
        public async Task Source(CommandContext ctx)
        {
            await ctx.RespondAsync("https://github.com/johnboggess/UnofficialSAUSoCBot");
        }

        /*[Command("help")]
        [Description("Lists all commands and their description.")]
        public async Task Help(CommandContext ctx)
        {

            List<Command> commands = ctx.CommandsNext.RegisteredCommands.Values.ToList();
            string result = "```";
            foreach (Command command in commands)
            {
                string name = "**" + command.Name + "***";
                string desc = command.Description ?? "No description available";
                result += name + "/n" + desc + "/n";
                
            }
            result+="```";
            await ctx.RespondAsync(result);
        }*/
    }
}
