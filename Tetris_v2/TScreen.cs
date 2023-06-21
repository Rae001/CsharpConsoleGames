using System;
using System.Collections.Generic;
using System.Text;


class TETRISSCREEN
{
    protected List<List<string>> BlockList = new List<List<string>>(); // 여기 부분 공부

    public int Y
    {
        get
        {
            return BlockList.Count;
        }
    }

    public int X
    {
        get
        {
            return BlockList[0].Count;
        }
    }

    public void SetBlock(int _y, int _x, string _Type)
    {
        BlockList[_y][_x] = _Type;
    }

    public bool IsBlock(int _y, int _x, string _Type)
    {
        return BlockList[_y][_x] == _Type;
    }

    public void Clear()
    {
        for (int y = 0; y < BlockList.Count; ++y)
        {
            for (int x = 0; x < BlockList[y].Count; ++x)
            {
                if (y==0 || y== BlockList.Count - 1)
                {
                    BlockList[y][x] = "▣";
                    continue;
                }
                BlockList[y][x] = "□";
            }
        }
    }
    public virtual void Render()
    {
        for (int y = 0; y < BlockList.Count; ++y)
        {
            for (int x = 0; x < BlockList[y].Count; ++x)
            {

                Console.Write(BlockList[y][x]);
            }
            Console.WriteLine();
        }
    }

    public TETRISSCREEN(int _X, int _Y, bool TopAndBotLine)
    {
        for (int y = 0; y < _Y; ++y)
        {
            BlockList.Add(new List<string>());
            for (int x = 0; x < _X; ++x)
            {
                BlockList[y].Add("□");
            }
        }// (10x15)의 빈 공간 생성

        if (false == TopAndBotLine)
        {
            return;
        }

        for (int i = 0; i < BlockList[0].Count; i++)
        {
            BlockList[0][i] = "▣";
        } // 0번째 줄만 TBLOCK.WALL 로 채움

        for (int i = 0; i < BlockList[BlockList.Count - 1].Count; i++)
        {
            BlockList[BlockList.Count - 1][i] = "▣";
        } // 마지막 줄만 TBLOCK.WALL 로 채움
    }
}


