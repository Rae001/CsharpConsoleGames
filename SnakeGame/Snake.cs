using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    class Snake
    {
        public int length { get; set; } = 4;
        public Direction direction { get; set; } = Direction.Right;

        Position headPosition = new Position();
        List<Position> tail { get; set; } = new List<Position>();
        public enum Direction
        { Left, Right, Up, Down }

        public void MoveDirection()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    
                    case ConsoleKey.LeftArrow:
                        direction = Direction.Left;
                        break;
                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Direction.Right;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = Direction.Down;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Move()
        {
           MoveDirection();
            switch (direction)
            {
                case Direction.Left:
                    headPosition.x--;
                    break;
                case Direction.Right:
                    headPosition.x++;
                    break;
                case Direction.Up:
                    headPosition.y--;
                    break;
                case Direction.Down:
                    headPosition.y++;
                    break;
                default:
                    break;
            }
            Console.SetCursorPosition(headPosition.x, headPosition.y);
            Console.Write("@");
            Thread.Sleep(100);
            tail.Add(new Position(headPosition.x, headPosition.y));
            if (tail.Count > this.length)
            {
                var endtail = tail.First();
                Console.SetCursorPosition(endtail.x, endtail.y);
                Console.Write(" ");
                tail.Remove(endtail);
            }
            {

            }
        }
    }
}
