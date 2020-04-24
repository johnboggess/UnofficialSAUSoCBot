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

        [Command(nameof(StartMaze))]
        [Description("Starts a maze.")]
        public async Task StartMaze(CommandContext ctx, int depth)
        {
            if(depth == 1 || depth > 10)
            {
                await ctx.RespondAsync("Depth must be between 2 and 10");
                return;
            }

            Maze.Room rm = new Maze.Room(depth);
            rm.CreateExit();
            SAUSoCBot._MazePlayer = new Maze.Player(rm);
            await ctx.RespondAsync(SAUSoCBot._MazePlayer.GetRoomDescription());
        }

        [Command(nameof(Maze))]
        [Description("Goto the given room.")]
        public async Task Maze(CommandContext ctx, int room)
        {
            if (SAUSoCBot._MazePlayer == null)
            {
                await ctx.RespondAsync("A maze has not yet been started, use !"+nameof(StartMaze)+" to start a maze.");
                return;
            }
            await ctx.RespondAsync(SAUSoCBot._MazePlayer.GotoRoom(room));
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
