using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
   public class Player
    {
        public readonly string Name;
        public readonly int Number;
        public Player(string name,int number)
        {
            Name = name;
            Number = number;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
