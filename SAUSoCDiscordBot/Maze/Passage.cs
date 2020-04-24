using System;
using System.Collections.Generic;
using System.Text;

namespace SAUSoCDiscordBot.Maze
{
    class Passage
    {
        public PassageDescription Description;
        public Room Previous;
        public Room Next;

        public Passage()
        {
            Description = Descriptions[SAUSoCBot.Random.Next(0, Descriptions.Count)];
        }

        static List<PassageDescription> Descriptions = new List<PassageDescription>()
        {
            new PassageDescription() { Description = "hole in the ground", Action="jumped down the hole"},
            new PassageDescription() { Description = "ladder", Action = "climbed the ladder"},
            new PassageDescription() { Description = "crack in the wall", Action = "squeezed through the crack"},
            new PassageDescription() { Description = "passage", Action = "walk through the passage"}
        };
    }
}
