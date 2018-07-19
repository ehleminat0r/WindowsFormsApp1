using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.game
{
    public class Missile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int AccX { get; set; }
        public int AccY { get; set; }

        public Missile(int x, int y, int accx, int accy)
        {
            X = x;
            Y = y;
            AccX = accx;
            AccY = accy;
        }
    }
}
