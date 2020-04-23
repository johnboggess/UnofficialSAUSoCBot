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
        public const string DevModeWarning = "**Requires developer mode (Users Settings > Appearance > Developer Mode)**";
        /*[Command("ping")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync(ctx.User.Mention+" Pong!");
        }*/

        [Command("Pin")]
        [Description("Pins the previous message. Example: !pin")]
        public async Task Pin(CommandContext ctx)
        {
            List<DiscordMessage> msg = (await ctx.Channel.GetMessagesAsync()).ToList();
            await msg[1].PinAsync();
        }

        [Command("PinID")]
        [Description("Pins the message with the given ID. Right click a message and click copy ID to get a message's ID. "+DevModeWarning+". Example: !pin 702212971455184967")]
        public async Task Pin(CommandContext ctx, ulong messageID)
        {
            DiscordMessage msg = await ctx.Channel.GetMessageAsync(messageID);
            await msg.PinAsync();
        }

        [Command("UnpinID")]
        [Description("Unpins the message with the given ID, requires developer mode. Right click a message and click copy ID to get a message's ID. " + DevModeWarning + ". Example: !unpin 702212971455184967")]
        public async Task Unpin(CommandContext ctx, ulong messageID)
        {

            DiscordMessage msg = await ctx.Channel.GetMessageAsync(messageID);
            await msg.UnpinAsync();
        }

        [Command("source")]
        [Description("Link to the source code on github.")]
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
