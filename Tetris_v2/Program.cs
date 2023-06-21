using System;
using System.Collections.Generic;
using System.Text;


namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            TETRISSCREEN NewSC = new TETRISSCREEN(10, 15, true);

            AccScreen NewASC = new AccScreen(NewSC); 

            Block NewBlock = new Block(NewSC, NewASC);

            while (true)
            {
                for (int i = 0; i < 40000000; i++)
                {
                    int a = 0;
                }

                Console.Clear();
                NewSC.Render();
                NewSC.Clear();
                NewBlock.Move();

            }
        }
    }
}



