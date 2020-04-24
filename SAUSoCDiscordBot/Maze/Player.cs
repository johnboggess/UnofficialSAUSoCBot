using System;
using System.Collections.Generic;
using System.Text;

namespace SAUSoCDiscordBot.Maze
{
    class Player
    {
        public Room CurrentRoom;

        public Player(Room startRoom)
        {
            CurrentRoom = startRoom;
        }

        public string GetRoomDescription()
        {
            return CurrentRoom.GetDescription();
        }

        public string GotoRoom(int i)
        {
            Room rm = CurrentRoom.GotoRoom(i);

            if(rm.IsExit)
            {
                SAUSoCBot._MazePlayer = null;
                return rm.GetDescription();
            }

            bool wentBack = !CurrentRoom.IsEntrance && CurrentRoom.PreviousRoom.Previous == rm;

            if (rm == null) { return "Invalid Option+\n" + GetRoomDescription(); }
            CurrentRoom = rm;
            return "You " + (wentBack?"went back" : rm.PreviousRoom.Description.Action) + ".\n" + rm.GetDescription();
        }
    }
}
