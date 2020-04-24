using System;
using System.Collections.Generic;
using System.Text;

namespace SAUSoCDiscordBot.Maze
{

    public class PassageDescription
    {
        public string Description = "NoDescGiven";
        public string Action = "NoActionGiven";
    }

    public class RoomDescription
    {
        public string Description = "NoDescGiven";
        public string Modifer = "NoModiferGiven";

        public RoomDescription(List<string> potentialModifiers)
        {
            Modifer = potentialModifiers[SAUSoCBot.Random.Next(0, potentialModifiers.Count)];
        }
    }
}
