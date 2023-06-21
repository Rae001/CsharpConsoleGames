using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;    

class Program
{

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
    static void Main(string[] args)
    {
        var current = TetrisFigures[1];
    }
}

