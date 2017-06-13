using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Ship
    {

        public int X1 { get; set; }
        public int Y1 { get; set; }

        public bool Uncovered1 { get; set; }

        public bool IsFirst(int x, int y)
        {
            return x == X1 && y == Y1;
        }

        public int X2 { get; set; }

        public int Y2 { get; set; }

        public bool Uncovered2 { get; set; }

        public bool IsSecond(int x, int y)
        {
            return x == X2 && y == Y2;
        }
    }
}
