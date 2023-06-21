using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Fruit
    {
        Position fruitPos = new Position();

        Random random = new Random();
        Board board = new Board();

        public Fruit()
        {
            fruitPos.x = random.Next(2, board.width);
            fruitPos.y = random.Next(1, board.height);
        }

        public void WriteFruit()
        {
            
            Console.SetCursorPosition(fruitPos.x, fruitPos.y);
            Console.Write("F");
        }

        //public Position FruitLocation()
        //{
        //    return fruitPos;
        //}
        public void FruitNewLocation()
        {
            fruitPos.x = random.Next(2, board.width);
            fruitPos.y = random.Next(1, board.height);
        }
    }
}
