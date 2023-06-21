using System;

namespace SnakeGame
{

    

    
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Snake snake = new Snake();
            Fruit fruit = new Fruit();


            while (true)
            {
                board.WriteBoard();
                fruit.WriteFruit();
                //snake.MoveDirection();
                snake.Move();
            }

        }
    }
}
