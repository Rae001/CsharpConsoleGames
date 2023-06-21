using System;
using System.Collections.Generic;
using System.Text;

namespace StackGame
{
    class Board
    {
        //public char[,] wall;


        //public Board(int x, int y)
        //{
        //    wall = new char[x, y];
        //   //wall[0, 0] = '┌';

        //}

        //public void MakeBoard()
        //{
        //    for (int x = 1; x < 29; x++)
        //    {
        //        //■
        //        //□
        //        wall[0, x] = '-';
        //        wall[19, x] = '-';
        //    }
        //    for (int y = 1; y < 19; y++)
        //    {
        //        wall[y, 0] = '|';
        //        wall[y, 29] = '|';
        //    }

        //    for (int y = 0; y < 20; y++)
        //    {
        //        for (int x = 0; x < 30; x++)
        //        {
        //            Console.Write(wall[y, x]);
        //        }
        //        Console.WriteLine();
        //    }
        //}

        //public void SetBlock(int x, int y)
        //{
        //    wall[x, y] = '#';
        //}

        public int height { get; private set; }
        public int width { get; private set; }


        public Board()
        {
            height = 20;
            width = 30;

            Console.CursorVisible = false;
        }
        public void MakeBoard()
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
