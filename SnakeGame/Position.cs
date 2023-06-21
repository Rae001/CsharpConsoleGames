using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public Position()
        {
            x = 5;
            y = 5;
        }
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
