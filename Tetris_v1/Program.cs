using System;
using System.Collections.Generic;
using System.Threading;

namespace Tetris_v1
{
    class Program
    {
        // Settings
        static int TetrisRows = 20; // 테트리스창 행 (높이)
        static int TetrisCols = 10; // 테트리스창 열 (넓이)
        static int InfoCols = 10; // 정보창 열 (넓이)
        static int ConsoleRows = 1 + TetrisRows + 1; // 콘솔창 행 = 22
        static int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1; // 콘솔창 열 = 23


        static List<bool[,]> TetrisFigures = new List<bool[,]>()
        {
            new bool[,] // I 
            {
                { true, true, true, true } 
            }, 
            new bool[,] // O
            {
                {true, true },
                {true, true }
            }, 
            new bool[,] // T
            {
                {false, true, false },
                {true, true, true }
            }, 
            new bool[,] // S
            {
                {false, true, true },
                {true, true, false }
            }, 
            new bool[,] // Z
            {
                {true, true, false },
                {false, true, true }
            }, 
            new bool[,] // J
            {
                {true, false, false },
                {true, true, true }
            },
            new bool[,] // L
            {
                {false, false, true },
                {true,  true, true }
            },
        };

        // State
        static int Score = 0;
        static int Frame = 0;
        static int FrameToMoveFigure = 15;

        static int CurrentFigureIndex = 2;

        static int CurrentFigureRow = 0; // 현재블록모양의 Y 값
        static int CurrentFigureCol = 0; // 현재블록모양의 X 값


        //                                       20           10
        static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];

        static void Main(string[] args)
        {
            Console.Title = "Tetris v1.0";

            Console.CursorVisible = false;

            Console.WindowHeight = ConsoleRows + 1; // 기본 창 높이 : 23
            Console.WindowWidth = ConsoleCols; // 기본 창 넓이 : 23

            Console.BufferHeight = ConsoleRows + 1; // 버퍼창 높이 : 23
            Console.BufferWidth = ConsoleCols; // 버퍼창 넓이 : 23

            while (true)
            {
                Frame++;
                // Read user input
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        //Environment.Exit(0);
                        return;
                    }

                    if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                    {
                        // move current figure left
                        CurrentFigureCol--;
                    }

                    if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                    {
                        // move current figure right
                        CurrentFigureCol++;
                    }

                    if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                    {
                        Frame = 0;
                        Score++;
                        CurrentFigureRow++;
                    }

                    if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                    {
                        // Implement 90-degree rotation of current figure
                    }
                }

                // Update the game state
                if (Frame % FrameToMoveFigure == 0)
                {
                    CurrentFigureRow++;
                    Frame = 0;
                    
                }

                //if (Collision()) // 충돌이 발생했을때?
                //{
                //    // AddCurrentFigureToTetrisField()
                //    // CheckForFullLines()
                //    // if (lines remove)  Score++;
                //}

                // Redraw UI
                DrawBorder();
                DrawInfo();

                DrawCurrentFigure();

                
                Thread.Sleep(40);
            }
        }

        

        static void DrawBorder()
        {
            #region new string에 관해
            //Initialized a new instance of the new string class to the value indicated by a specified Unicode character repeated a specified number of times
            // 새 문자열 클래스의 새 인스턴스를 지정된 횟수만큼 반복되는
            // 지정된 유니코드 문자로 표시된 값으로 초기화했습니다.
            #endregion
            // SetCursorPosition(0, 0); 을 설정하면 화면이 움직이지 않는다??
            Console.SetCursorPosition(0, 0);

            string line = "┌";
            //                          10
            line += new string('─', TetrisCols);
            //for (int i = 0; i < TetrisCols; i++)
            //{
            //    line += "─";
            //}


            line += "┬";
            //                         10
            line += new string('─', InfoCols);
            //for (int i = 0; i < InfoCols; i++)
            //{
            //    line += "─";
            //}
            line += "┐";
            Console.Write(line); // line = 23
            //                       20
            for (int i = 0; i < TetrisRows; i++)
            {
                string middleLine = "│";
                //                                10
                middleLine += new string(' ', TetrisCols);

                middleLine += "│";
                //                               10
                middleLine += new string(' ', InfoCols);

                middleLine += "│";
                Console.Write(middleLine); // middleLine = 23
            }

            string endLine = "└";
            //                             10
            endLine += new string('─', TetrisCols);

            endLine += "┴";
            //                             10
            endLine += new string('─', InfoCols);

            endLine += "┘";
            Console.Write(endLine); // endLine = 23
        }

        static void DrawInfo()
        {
            Write("Score: ", 1, 3 + TetrisCols);
            Write(Score.ToString(), 2, 3 + TetrisCols);
            Write("Frame: ", 4, 3 + TetrisCols);
            Write(Frame.ToString(), 5, 3 + TetrisCols);
        }

        static void DrawCurrentFigure()
        {
            var currentFigure = TetrisFigures[CurrentFigureIndex];
            //                      2차원배열 행 길이만큼 = 2
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                //                      2차원배열의 1차원배열 길이만큼 = 3
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    if (currentFigure[row, col]) // currentFigure[row, col] 안에 값이 true이면 Write 함수 실행, false면 넘어감
                    {
                        Write("*", row + 1 + CurrentFigureRow, col + 2+ CurrentFigureCol);
                    }
                }
            }
        }


        static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.ResetColor();

            // 업데이트 확인
        }
    }
}
