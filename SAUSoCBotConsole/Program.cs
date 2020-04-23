using System;
using SAUSoCDiscordBot;

namespace SAUSoCBotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SAUSoCBot bot = new SAUSoCBot();
            bot.Start();
        }
    }
}
