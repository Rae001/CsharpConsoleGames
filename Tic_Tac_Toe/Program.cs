using System;
using System.Threading;

namespace Tic_Tac_Toe
{
    class Program
    {

        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int choice;
        static int player = 1;
        static int flag = 0;

        static void Main(string[] args)
        {
            Game();
        }

        static int Check()
        {
            if (arr[1]== arr[2] && arr[2]== arr[3])
            {
                return 1;
            }
            else if (arr[4] == arr[5] && arr[5] == arr[6])
            {
                return 1;
            }
            else if (arr[7] == arr[8] && arr[8] == arr[9])
            {
                return 1;
            }
            else if (arr[1] == arr[4] && arr[4] == arr[7])
            {
                return 1;
            }
            else if (arr[2] == arr[5] && arr[5] == arr[8])
            {
                return 1;
            }
            else if (arr[3] == arr[6] && arr[6] == arr[9])
            {
                return 1;
            }
            else if (arr[1] == arr[5] && arr[5] == arr[9])
            {
                return 1;
            }
            else if (arr[3] == arr[5] && arr[5] == arr[7])
            {
                return 1;
            }
            else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9')
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        static void Game()
        {
            try
            {
                while (flag != 1 && flag != -1)
                {

                    Console.Clear();
                    Console.WriteLine("[Player1 : X] || [Player2 : O]");
                    Console.WriteLine("\n");
                    Board();
                    Console.WriteLine("\n");
                    #region Player
                    if (player % 2 == 0)
                    {
                        Console.WriteLine("Player2 Choice");
                    }
                    else
                    {
                        Console.WriteLine("Player1 Choice");
                    }
                    #endregion
                    choice = int.Parse(Console.ReadLine());

                    if (arr[choice] != 'X' && arr[choice] != 'O')
                    {
                        if (player % 2 == 0)
                        {
                            arr[choice] = 'O';
                            player++;
                        }
                        else
                        {
                            arr[choice] = 'X';
                            player++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sorry the row {choice} is already marked with {arr[choice]}");
                        Console.WriteLine("\n");
                        Console.WriteLine("Please wait 2 second board is loading again.....");
                        Thread.Sleep(2000);
                    }
                    flag = Check();

                    if (flag == 1)
                    {
                        Console.WriteLine($"{(player % 2) + 1} Win!!");
                    }
                }
                Console.Clear();
                Board();

                if (flag == 1)
                {
                    Console.WriteLine($"{(player % 2) + 1} Win!!");
                    Reset();
                }
                else
                {
                    Console.WriteLine("Draw");
                    Reset();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("No String Input.");
                Thread.Sleep(2000);
                Game();
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Over Number.");
                Thread.Sleep(2000);
                Game();
            }

        }

        static void Reset()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Try Again?");
            Console.WriteLine("1. [Yes]");
            Console.WriteLine("2. [No]");
            Console.WriteLine("Choose Number");
            int input = int.Parse(Console.ReadLine());

            switch (input)  
            {
                case 1:
                    player = 1;
                    flag = 0;
                    arr[0] = '0';
                    arr[1] = '1';
                    arr[2] = '2';
                    arr[3] = '3';
                    arr[4] = '4';
                    arr[5] = '5';
                    arr[6] = '6';
                    arr[7] = '7';
                    arr[8] = '8';
                    arr[9] = '9';
                    Game();
                    break;

                case 2:
                    Console.WriteLine("Bye!");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("You pressed another one. Try Again");
                    Reset();
                    break;
            }
        }

        static void Board()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);
            Console.WriteLine("     |     |      ");
        }
    }
}
