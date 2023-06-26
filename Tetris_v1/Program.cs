using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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

        static string ScoreFileName = "scores.txt";

        static int[] ScorePerLines = { 0, 40, 100, 300, 1200 };

        // State
        static int HighScore = 0;
        static int Score = 0;
        static int Frame = 0;
        static int FrameToMoveFigure = 15;



        static bool[,] CurrentFigure = null;

        static int CurrentFigureRow = 0; // 현재블록모양의 Y 값
        static int CurrentFigureCol = 0; // 현재블록모양의 X 값

        //                                       20           10
        static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];
        static Random random = new Random();

        static void Main(string[] args)
        {
            if (File.Exists("score.txt"))
            {
                var allScores = File.ReadAllLines("score.txt");
                foreach(var score in allScores)
                {
                    var match = Regex.Match(score, @"=> (?<score>[0-9]+)");
                    HighScore = Math.Max(HighScore, int.Parse(match.Groups["score"].Value));
                }
            }


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Title = "Tetris v1.0";

            Console.CursorVisible = false;

            Console.WindowHeight = ConsoleRows + 1; // 기본 창 높이 : 23
            Console.WindowWidth = ConsoleCols; // 기본 창 넓이 : 23

            Console.BufferHeight = ConsoleRows + 1; // 버퍼창 높이 : 23
            Console.BufferWidth = ConsoleCols; // 버퍼창 넓이 : 23
            CurrentFigure = TetrisFigures[random.Next(0, TetrisFigures.Count)];

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
                        if (CurrentFigureCol > 0)
                        {
                            CurrentFigureCol--;
                        }
                        

                    }

                    if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                    {
                        if (CurrentFigureCol < TetrisCols - CurrentFigure.GetLength(1))
                        {
                            // move current figure right
                            CurrentFigureCol++;
                        }
                    }

                    if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                    {
                        Frame = 0;
                        Score++;
                        CurrentFigureRow++;
                    }

                    if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                    {
                        RotateCurrentFigure();
                    }
                }

                // Update the game state
                if (Frame % FrameToMoveFigure == 0)
                {
                    CurrentFigureRow++;
                    Frame = 0;
                }

                if (Collision(CurrentFigure)) // 충돌이 발생했을때?
                {
                    AddCurrentFigureToTetrisField();

                    int lines = CheckForFullLines();
                    Score += ScorePerLines[lines];
                    CurrentFigure = TetrisFigures[random.Next(0, TetrisFigures.Count)];
                    CurrentFigureRow = 0;
                    CurrentFigureCol = 0;
                    if (Collision(CurrentFigure))
                    {
                        File.AppendAllLines("scores.txt", new List<string>
                        {
                            $"[{DateTime.Now.ToLongTimeString()}] {Environment.UserName} => {Score}"
                        });

                        var scoreAsString = Score.ToString();
                        scoreAsString += new string(' ', 7 - scoreAsString.Length);
                        Write("┌───┐", 5, 5);
                        Write("│ Game │", 6, 5);
                        Write("│ over!│", 7, 5);
                        Write($"│ {scoreAsString}│", 8, 5);
                        Write("└───┘", 9, 5);
                        Console.ReadKey();
                    }

                    CurrentFigure = TetrisFigures[random.Next(0, TetrisFigures.Count)];
                    CurrentFigureRow = 0;
                    CurrentFigureCol = 0;

                    
                }

                // Redraw UI
                DrawBorder();
                DrawInfo();
                DrawTetrisField();
                DrawCurrentFigure();

                Thread.Sleep(40);
            }
        }

        static void RotateCurrentFigure()
        {
            var newFigure = new bool[CurrentFigure.GetLength(1), CurrentFigure.GetLength(0)];
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    newFigure[col, CurrentFigure.GetLength(0) - row - 1] = CurrentFigure[row, col];
                }
            }
            if (!Collision(newFigure))
            {
                CurrentFigure = newFigure;
            }
            
        }

        static int CheckForFullLines()
        {
            int lines = 0;

            //                                 20
            for (int row = 0; row < TetrisField.GetLength(0); row++)
            {
                bool rowIsFull = true;

                //                              10
                for (int col = 0; col < TetrisField.GetLength(1); col++)
                {
                    if (TetrisField[row,col] == false)
                    {
                        rowIsFull = false;
                        break;
                    }
                }

                if (rowIsFull) // rowIsFull이 true라면
                {
                    for (int rowToMove = row; rowToMove >= 1; rowToMove--)
                    {
                        for (int col = 0; col < TetrisField.GetLength(1); col++) // 테트리스 필드의 열
                        {
                            TetrisField[rowToMove, col] = TetrisField[rowToMove - 1, col];
                        }
                    }

                    lines++;
                }
            }
            return lines;
        }

        static void AddCurrentFigureToTetrisField()
        {
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col]) // CurrentFigure[row, col] 가 true 이면 
                    {
                        TetrisField[CurrentFigureRow + row, CurrentFigureCol + col] = true; // 테트리스 필드의 [row, col]값도 true로 변환
                    }
                }
            }
        }

        static bool Collision(bool[,] figure)
        {
            //       블록의x값          10        현재 블록의 열길이
            if (CurrentFigureCol > TetrisCols - figure.GetLength(1))
            {
                return true; 
            }

            if (CurrentFigureRow + figure.GetLength(0) == TetrisRows) // 맨 밑바닥에 블록이 닿았을 때 
            {
                return true;
            }

            for (int row = 0; row < figure.GetLength(0); row++)
            {
                for (int col = 0; col < figure.GetLength(1); col++)
                {
                    if (figure[row, col] && TetrisField[CurrentFigureRow + row + 1, CurrentFigureCol + col]) // 밑에 쌓인 블록 위에 블록이 쌓였을 때
                    {
                        return true;
                    }
                }
            }

            return false;
        }
      
        static void DrawBorder()
        {
            #region new string에 관해
            //Initialized a new instance of the new string class to the value indicated by a specified Unicode character repeated a specified number of times
            // 새 문자열 클래스의 새 인스턴스를 지정된 횟수만큼 반복되는
            // 지정된 유니코드 문자로 표시된 값으로 초기화했습니다.
            #endregion
            Console.SetCursorPosition(0, 0);

            string line = "┌";
            line += new string('─', TetrisCols);
            line += "┬";
            line += new string('─', InfoCols);
            line += "┐";
            Console.Write(line);
            for (int i = 0; i < TetrisRows; i++)
            {
                string middleLine = "│";
                middleLine += new string(' ', TetrisCols);
                middleLine += "│";
                middleLine += new string(' ', InfoCols);
                middleLine += "│";
                Console.Write(middleLine);
            }
            string endLine = "└";
            endLine += new string('─', TetrisCols);
            endLine += "┴";
            endLine += new string('─', InfoCols);
            endLine += "┘";
            Console.Write(endLine);
        }

        static void DrawInfo()
        {

            if (Score > HighScore)
            {
                HighScore = Score;
            }
            Write("Score: ", 1, 3 + TetrisCols);
            Write(Score.ToString(), 2, 3 + TetrisCols);


            Write("HScore: ", 4, 3 + TetrisCols);
            Write(HighScore.ToString(), 5, 3 + TetrisCols);
            Write("Frame: ", 7, 3 + TetrisCols);
            Write(Frame.ToString(), 8, 3 + TetrisCols);
            Write("Pos: ", 10, 3 + TetrisCols);
            Write($"{CurrentFigureRow}, {CurrentFigureCol}", 11, 3 + TetrisCols);
            Write("Keys: ", 13, 3 + TetrisCols);
            Write($"  ^ ", 15, 3 + TetrisCols);
            Write($"<   >", 16, 3 + TetrisCols);
            Write($"  v  ", 17, 3 + TetrisCols);

        }

        static void DrawTetrisField() // OK
        {
            for (int row = 0; row < TetrisField.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < TetrisField.GetLength(1); col++)
                {
                    if (TetrisField[row, col]) // TetrisField[row, col] 가 true이면 TetrisField에 "*" 표현 
                    {
                        line += "*";
                    }
                    else
                    {
                        line += " ";    
                    }
                }
                Write(line, row + 1, 1);
            }
        }

        static void DrawCurrentFigure()
        {
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col]) // currentFigure[row, col] 안에 값이 true이면 Write 함수 실행, false면 넘어감
                    {
                        line += "*";
                    }
                    else
                    {
                        line += " ";
                    }
                }
                Write(line, row + 1 + CurrentFigureRow, 1 + CurrentFigureCol);
            }
        }

        static void Write(string text, int row, int col)
        {
            
            Console.SetCursorPosition(col, row);
            Console.Write(text);

        }
    }
};