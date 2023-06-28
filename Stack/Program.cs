using System;
using System.Threading;

namespace Stack
{
    class Program
    {

        static int width = 30;
        static int height = 20;

        static int movingBlockX = 14;
        static int movingBlockY = 17;

        static int fixedBlockX = 14;
        static int fixedBlockY = 18;

        static int score = 0;

        static bool isRight = true;
        static bool isSame = false;

        //                                     20      30
        static bool[,] BlockField = new bool[height, width];

        static Random random = new Random();



        static void Main(string[] args)
        {
            Console.CursorVisible = false;


            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        if (movingBlockX == fixedBlockX)
                        {
                            isSame = true;

                            if (isSame == true)
                            {
                                DrawFixBlock();
                                if (movingBlockY == 15)
                                {
                                    CheckHigher();
                                }
                                else
                                {
                                    movingBlockY--;
                                }
                            }
                            score += 100;

                        }
                    }
                }
                DrawBoard();
                DrawBlockField();
                DrawInfo();
                MoveBock();
                Thread.Sleep(60);
            }

        }

        static void DrawBoard()
        {
            Console.SetCursorPosition(0, 0);

            //├
            string line = "┌";
            line += new string('─', (width - 2));
            line += "┐";
            Console.WriteLine(line);

            for (int i = 0; i < (height - 2); i++)
            {
                string middleLine = "│";
                middleLine += new string(' ', (width - 2));
                middleLine += "│";
                Console.WriteLine(middleLine);
            }

            string endLine = "└";
            endLine += new string('─', (width - 2));
            endLine += "┘";
            Console.Write(endLine);
        }
        static void DrawInfo()
        {
            Console.SetCursorPosition(2, 1);
            Console.Write("Score : " + score);

            Console.SetCursorPosition(7, 4);
            Console.Write("Press the SpaceBar");
        }
        static void Write(string shape, int x, int y)
        {
            Console.ForegroundColor = DrawRandomColor();
            Console.SetCursorPosition(x, y);
            Console.Write(shape);
            Console.ResetColor();
        }
        static void MoveBock()
        {
            if (isRight == true)
            {
                movingBlockX++;
            }
            else
            {
                movingBlockX--;
            }

            if (movingBlockX == 2 || movingBlockX == (width - 2))
            {
                isRight = !isRight;
            }
            Write("#", movingBlockX, movingBlockY);
        }
        static void DrawBlockField()
        {
            BlockField[fixedBlockY, fixedBlockX] = true;
            //                            1 ~ 19
            for (int y = 1; y < BlockField.GetLength(0) - 1; y++)
            {
                string block = "";
                //                            1 ~ 28
                for (int x = 1; x < BlockField.GetLength(1) - 2; x++)
                {
                    if (BlockField[y, x])
                    {
                        block += "#";
                    }
                    else
                    {
                        block += " ";
                    }
                }
                Write(block, 2, y);
            }
        }
        static void DrawFixBlock()
        {
            BlockField[movingBlockY, movingBlockX] = true;
            for (int y = 1; y < BlockField.GetLength(0) - 1; y++)
            {
                string block = "";
                for (int x = 1; x < BlockField.GetLength(1) - 2; x++)
                {
                    if (BlockField[y, x])
                    {
                        block += "#";
                    }
                    else
                    {
                        block += " ";
                    }
                }
                Write(block, 2, y);
            }
        }
        static void CheckHigher()
        {
            // 19 ~ 1          
            for (int rowToMove = BlockField.GetLength(0) - 1; rowToMove >= 1; rowToMove--)
            {
                // 1 ~ 28
                for (int col = 1; col < BlockField.GetLength(1) - 2; col++) // 테트리스 필드의 열
                {
                    BlockField[rowToMove, col] = BlockField[rowToMove - 1, col];
                }
            }
        }

        static ConsoleColor DrawRandomColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(random.Next(consoleColors.Length));
        }
    }
}
