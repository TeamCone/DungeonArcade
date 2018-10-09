using System;
using System.Collections.Generic;
using Game.Player;

namespace Game.Scripts.Player
{
    [Serializable]
    public class Players
    {
        public List<EnumPlayer> list;

        public Players()
        {
            list = new List<EnumPlayer>();
        }
        
    }
}