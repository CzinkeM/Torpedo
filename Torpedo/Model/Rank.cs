using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Model
{
    public class Rank
    {
        public string name;
        public int wins;
        public int loses;

        public Rank(string player, int playerWins, int playerLoses)
        {
            name = player;
            wins = playerWins;
            loses = playerLoses;
        }
    }
}
