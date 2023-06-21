using System;
using System.Threading;


namespace _99Dan
{

    class Game
    {
        public void Process()
        {
            Console.WriteLine("99단 게임 레벨을 선택하세요.");
            Console.WriteLine("레벨1");
            Console.WriteLine("레벨2");
            Console.WriteLine("레벨3");

            string input = Console.ReadLine();

            while (true)
            {
                Console.Clear();
                switch (input)
                {
                    case "1":
                        LevelOne();
                        break;
                    case "2":
                        LevelTwo();
                        break;
                    case "3":
                        LevelThree();
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                        Process();
                        break;
                }
            }

        }
        public void Timer()
        {
            for (int i = 5; i >= 1; i--)
            {
                Console.Clear();
                Console.WriteLine("제한시간" + i + "초");
                Thread.Sleep(1000);
                if (i == 1)
                {
                    Console.Clear();
                    Console.WriteLine("GameOver!!");
                }
            }
        }
        public void LevelOne()
        {


            Console.Clear();
            while (true)
            {

                Random random = new Random();
                int X = random.Next(1, 9); // X의 값을 1부터 9까지 랜덤생성
                int Y = random.Next(1, 9); // X의 값을 1부터 9까지 랜덤생성
                int Z = X * Y; // X 와 Y 의 결과값을 Z에 할당
                Console.WriteLine("레벨1");

                Console.Write(X + " X " + Y + " = ");
                int intResult;
                intResult = int.Parse(Console.ReadLine());


                if (intResult == Z)
                {
                    Console.WriteLine("[정답]");
                }
                else
                {
                    Console.WriteLine("[오답]");
                }
            }
        }
        public void LevelTwo()
        {
            Console.Clear();
            while (true)
            {

                int Timer = 5; // 제한시간 5초
                Random random = new Random();
                int X = random.Next(1, 9); // X의 값을 1부터 9까지 랜덤생성
                int Y = random.Next(1, 9); // X의 값을 1부터 9까지 랜덤생성
                int Z = X * Y; // X 와 Y 의 결과값을 Z에 할당
                Console.WriteLine("레벨2");
                Console.WriteLine("제한시간 : " + Timer);
                Console.Write(X + " X " + Y + " = ");
                int intResult;
                intResult = int.Parse(Console.ReadLine());
                if (intResult == Z)
                {
                    Console.WriteLine("[정답]");
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("[오답]");
                }
            }
        }
        public void LevelThree()
        {
            Console.Clear();
            while (true)
            {

                int Timer = 5; // 제한시간 5초
                Random random = new Random();
                int X = random.Next(1, 9); // X의 값을 1부터 9까지 랜덤생성
                int Y = random.Next(1, 9); // X의 값을 1부터 9까지 랜덤생성
                int Z = X * Y; // X 와 Y 의 결과값을 Z에 할당
                Console.WriteLine("레벨3");
                Console.WriteLine("제한시간 : " + Timer);
                Console.Write(X + " X " + Y + " = ");
                int intResult;
                intResult = int.Parse(Console.ReadLine());
                if (intResult == Z)
                {
                    Console.WriteLine("[정답]");
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("[오답]");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Process();

        }
    }
}
