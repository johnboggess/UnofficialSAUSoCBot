using System;
using System.Collections.Generic;
using System.Text;

namespace SAUSoCDiscordBot.Maze
{
    class Room
    {
        public Passage PreviousRoom = null;
        public RoomDescription Description;
        public List<Passage> Rooms = new List<Passage>();
        public bool IsExit = false;
        public bool IsEntrance { get { return PreviousRoom is null; } }
        Guid guid = Guid.NewGuid();

        public Room(int depth, int minRooms = 1, int maxRooms = 3)
        {
            Description = Descriptions[SAUSoCBot.Random.Next(0, Descriptions.Count)];
            if(depth <= 1) { return; }
            
            int rooms = SAUSoCBot.Random.Next(minRooms, maxRooms + 1);
            for (int i = 0; i < rooms; i++)
            {
                Room rm = new Room(depth - 1, minRooms, maxRooms);

                Passage passage = new Passage();
                passage.Previous = this;
                passage.Next = rm;
                rm.PreviousRoom = passage;

                Rooms.Add(passage);
            }
        }

        public string GetDescription()
        {
            if(IsExit) { return "You found the exit."; }
            string result = "You enter a " + Description.Modifer + " " + Description.Description + ".\n";
            if(!IsEntrance) { result += "Go back (0).\n"; }

            if (Rooms.Count > 0)
                result += "There is ";
            else
                result += "Its a dead end";

            for (int i = 0; i < Rooms.Count; i++)
            {
                Passage p = Rooms[i];
                string desc = Utilities.FormatString(p.Description.Description, false, (i<Rooms.Count-1 || Rooms.Count == 1)? "a " : "and a ", "");
                result += desc + " (" + (i + (IsEntrance ? 0 : 1)) + ")";

                if(i != Rooms.Count-1) { result += ", "; }
            }
            result += ".";
            return result;
        }

        public Room GotoRoom(int i)
        {
            if (!IsEntrance)
            {
                if (i == 0) { return PreviousRoom.Previous; }
                else if (i - 1 < Rooms.Count) { return Rooms[i - 1].Next; }
                return null;
            }
            if (i < Rooms.Count) { return Rooms[i].Next; }
            return null;
        }

        public void CreateExit()
        {
            if(IsEntrance)
            {
                Rooms[SAUSoCBot.Random.Next(0,Rooms.Count)].Next.CreateExit();
            }
            else
            {
                if(Rooms.Count == 0) { IsExit = true; return; }
                if(SAUSoCBot.Random.Next(0, 10) == 1) { IsExit = true; return; }
                Rooms[SAUSoCBot.Random.Next(0, Rooms.Count)].Next.CreateExit();
            }
        }

        static List<string> _roomDescModifiers = new List<string>()
        {
            "dark",
            "long",
            "tall",
            "narrow",
            "wide",
            "damp",
            "flooded",
            "short",
            "sloped",
            "empty",
            "winding",
            "underground",
            "deep",
            "shallow"
        };

        static List<RoomDescription> Descriptions = new List<RoomDescription>()
        {
            new RoomDescription(_roomDescModifiers) { Description = "cavern" },
            new RoomDescription(_roomDescModifiers) { Description = "passage" },
            new RoomDescription(_roomDescModifiers) { Description = "chasm" },
            new RoomDescription(_roomDescModifiers) { Description = "ravine" },
        };
    }
}
