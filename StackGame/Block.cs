using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StackGame
{
    class Block
    {
        //int fixedBlockX = 10;
        //int fixedBlockY = 10;

        //int movingBlockX;
        //int movingBlockY;


        //Board block = null;

        //public Block(Board board)
        //{
        //    block = board;
        //    block.SetBlock(fixedBlockY, fixedBlockX);
            
        //}

        Board bd = new Board();

        public int fixedBlockX;
        public int fixedBlockY;

        public int movingBlockX;
        public int movingBlockY;

        public string[,] block = new string[20, 30];

        // score
        int score = 0;

        // sign

        bool isBlockRight = true;

        bool isFixBock = false;

        bool isGameOver = false;

        bool isColorChange = false;


        Random random = new Random();

        public Block()
        {
            
            // movingBlock
            fixedBlockX = bd.width / 2;
            fixedBlockY = bd.height - 1;

            block[fixedBlockY, fixedBlockX] = "X";

            // fixedBlock set Position
            Console.ForegroundColor = GetRandomConsoleColor();
            Console.SetCursorPosition(fixedBlockX, fixedBlockY);
            Console.Write("#");
        }

        public ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(random.Next(1, consoleColors.Length));
        }

        public void UI()
        {
            //Console.ResetColor();

            // score ui set Positon
            Console.SetCursorPosition(2, 1);
            Console.Write("Score : " + score);

            // how to play
            Console.SetCursorPosition(7, 3);
            Console.Write("Press the SpaceBar");
        }


        public void MoveBlock()
        {

            while (true)
            {
             
                if (isBlockRight == true)
                {
                    movingBlockX++;
                }
                else
                {
                    movingBlockX--;
                }

                if (movingBlockX == 2 || movingBlockX == 28)
                {
                    // 부정연산자(!)를 사용하여 처음 bool 값이 true여서 blockPositionX 값이 증가하여 27이 되는 경우 true를 false로 변경하여 blockPositionX 값이 감소한다.
                    // 그리고 다시 blockPositionX 값이 2가 되는 경우 부정연산자에 의해 false값이 true로 바뀌어 blockPositionX 가 좌우 반복 이동을 한다. 
                    isBlockRight = !isBlockRight;
                }

                

            }
        }
        //    public bool PressStop()
        //    {
        //        if (Console.KeyAvailable == true) // 어떤 Key 값이던 간에 입력된게 있는가
        //        {
        //            // 아무 키나 누르면 바로 반응하는 메서드는 Console.ReadKey 
        //            // 입력 인자로 bool 형식 인자를 받는다.
        //            // true를 전달하면 키를 눌렀을 때 콘솔 화면에 출력X, false를 누르면 콘솔 화면에 누른 키 출력.
        //            ConsoleKeyInfo input = Console.ReadKey(true);

        //            if (input.Key == ConsoleKey.Spacebar) // Spacebar가 맞으면 true로 반환.
        //            {
        //                isFixBock = true;
        //                isColorChange = true;
        //            }
        //        }
        //        return isFixBock;
        //    }
        //}
    }
}
