using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Board
    {
        public int height { get; private set; } 
        public int width { get; private set; }

        public Board()
        {
            height = 20;
            width = 30;
            Console.CursorVisible = false;
        }
        public void WriteBoard()
        {
            WriteChar(0, 0, '┌');
            WriteChar(width, 0, '┐');
            WriteChar(0, height, '└');
            WriteChar(width, height, '┘');

            for (int i = 1; i < width; i++)
            {
                WriteChar(i, 0, '-');
                WriteChar(i, height, '-');
            }
            for (int i = 1; i < height; i++)
            {
                WriteChar(0, i, '│');
                WriteChar(width, i, '│');
            }
        }
        public void WriteChar(int x, int y, char z)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(z);
        }
    }
}
