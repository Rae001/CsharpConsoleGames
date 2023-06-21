using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    enum BLOCKDIR // 테트리스 블록 방향
    {
        BD_T, // 위
        BD_R, // 오른쪽
        BD_B, // 아래
        BD_L, // 왼쪽
        BD_MAX,
    }

    enum BLOCKTYPE // 테트리스 블록 형태
    {
        BT_I,
        BT_L,
        BT_J,
        BT_Z,
        BT_S,
        BT_T,
        BT_O,
        BT_MAX,
    }



    partial class Block
    {
        int X = 0;
        int Y = 0;
        
        string[][] Arr = null;
        //List<List<string>> BlockData = new List<List<string>>();

        BLOCKTYPE CurBlockType = BLOCKTYPE.BT_T; // enum 클래스를 변수로 선언
        BLOCKDIR CurDirType = BLOCKDIR.BD_T;
        TETRISSCREEN Screen = null;
        AccScreen AccScreen = null;
        Random NewRandom = new Random();
        public Block(TETRISSCREEN _Screen, AccScreen _AccScreen)
        {
            if (null == _Screen || null == _AccScreen)
            {
                return; // 이 리턴은 무슨 의미일까?
            }
            Screen = _Screen;
            AccScreen = _AccScreen;
            DataInit();
            // 바꿀 수 있는 인터페이스는 이거 하나
            RandomBlockType();
            SettingBlock(CurBlockType, CurDirType);
        }
        public void RandomBlockType()
        {

            int RandomIndex = NewRandom.Next((int)BLOCKTYPE.BT_I, (int)BLOCKTYPE.BT_MAX);
            CurBlockType = (BLOCKTYPE)RandomIndex;
        }
        private void SettingBlock(BLOCKTYPE _Type, BLOCKDIR _Dir)
        {
            Arr = AllBlock[(int)_Type][(int)_Dir];
        }


        public void SetAccScreen()
        {
            for (int y = 0; y < 4; ++y)
            {
                for (int x = 0; x < 4; ++x)
                {
                    if ("■" == Arr[y][x])
                    {
                        AccScreen.SetBlock(Y + y -1, X + x, "■");
                    }
                        
                }
            }
        }
        public bool DownCheck()
        {
            for (int y = 0; y < 4; ++y)
            {
                for (int x = 0; x < 4; ++x)
                {
                    if ("■" == Arr[y][x])
                    {
                        if (AccScreen.Y == Y + y || AccScreen.IsBlock(Y + y, X + x, "■"))
                        {
                            SetAccScreen();
                            Reset();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Reset()
        {
            RandomBlockType();
            X = 0;
            Y = 1;
            SettingBlock(CurBlockType, CurDirType);
        }

        public void Down()
        {
            // 블록이 내려가다가
            // 더 이상 내려갈 수 없으면 AccScreen에 쌓인다.
            if (true ==  DownCheck())
            {
                return;
            }
            Y += 1;
        }
        

        public void Input()
        {
            Y += 1;
            if (false == Console.KeyAvailable)
            {
                return;
            }
            switch (Console.ReadKey().Key) // 방향키를 입력하여 블록을 조작
            {
                case ConsoleKey.A:
                    X -= 1;
                    break;
                case ConsoleKey.D:
                    X += 1;
                    break;
                case ConsoleKey.Q: // 왼쪽 회전
                    --CurDirType;
                    if (0>CurDirType)
                    {
                        CurDirType = BLOCKDIR.BD_L;
                    }
                    break;
                case ConsoleKey.E: // 오른쪽 회전
                    if (CurDirType == BLOCKDIR.BD_MAX)
                    {
                        CurDirType = BLOCKDIR.BD_T;
                    }
                    ++CurDirType;
                    break;
                case ConsoleKey.S:
                    Down();
                    break;
                default:
                    break;
            }
        }

        public void Move()
        {
            // 내가 어떤 키를 눌렀을때만 Input() 함수 실행 
            Input();

            for (int y = 0; y < 4; ++y)
            {
                for (int x = 0; x < 4; ++x)
                {
                    if (Arr[y][x] == "□")
                    {
                        continue;
                    }
                    Screen.SetBlock(Y + y, X + x, Arr[y][x]);
                }
            }
            
        }
    }
}


