using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackJoon
{

    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            int[] input = new int[num];
            string[] input2 = Console.ReadLine().Split();
            int sum = 0;
            for (int i = 0; i < num; i++)
            {
                input[i] = int.Parse(input2[i]);
            }

            Array.Sort(input);

            for (int i = 1; i < num; i++)
            {
                input[i] = input[i - 1] + input[i];
                sum += input[i];
            }
            Console.WriteLine(sum+ input[0]);
        }
    }
}