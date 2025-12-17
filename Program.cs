using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    internal class Program
    {
        private const char PLAYER = 'P';        //플레이어
        private const char PLAYER_ON_GOAL = '@';//플레이어가 목표지점 위에 있는 상태
        private const char BOMB = 'B';          //폭탄
        private const char BOMB_ON_GOAL = '!';  //폭탄이 목표지점 위에 있는 상태
        private const char GOAL = 'G';          //목표지점
        private const char WALL = '#';          //벽
        private const char EMPTY = ' ';         //빈 공간

        static char[,] map = new char[,]         // 게임 필드(2차원 배열)
        {
           { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
           { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
           { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
           { '#', ' ', ' ', 'B', ' ', ' ', ' ', ' ', ' ', '#' },
           { '#', ' ', ' ', ' ', 'P', 'G', ' ', ' ', ' ', '#' },
           { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
           { '#', ' ', ' ', 'B', ' ', ' ', 'G', ' ', ' ', '#' },
           { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
           { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
           { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
            };
        static Position _playerPos = new Position()
        {
            X = 4,
            Y = 4
        };

        private static int _moveCount = 0;
        static void Main(string[] args)
        {
            Console.OutputEncoding= Encoding.UTF8;

            PrintGuideText();

            while (true)
            {
                PrintMoveCount();
                PrintMap();

                //클리어시 반복 종료
                if (IsGameClear())
                {
                    PrintClearText();
                    break;
                }

                //사용자 입력
                ConsoleKey inputKey;
                if (!TryGetInput(out inputKey)) continue;

                //종료 처리
                if (inputKey == ConsoleKey.Q)
                {
                    Console.WriteLine("\n게임을 종료합니다.");
                    break;
                }

                //로직수행

                //이동 가능한지 판단
                Position nextPos = GetNextPosition(inputKey);
                // 맵 밖인지?
                if (IsOutOfArray(nextPos)) continue;
                // 벽이진 않은지?
                char tragetTile = GetTile(nextPos);
                if (tragetTile == WALL) continue;

                //이동 구현(이동,폭탄밀기)
                //플레이어 단순 이동(Goal 위로 이동하는 것도 포함)
                if (tragetTile == EMPTY || tragetTile == GOAL)
                {
                    // 이동(함수로 구현)
                    Move(_playerPos, nextPos, PLAYER);
                    _playerPos = nextPos;
                    //현재 플레이어 위치를 다음 nextPos로 바꿈
                    _moveCount++;
                }
                //폭탄을 밀면서 이동
                else if (tragetTile == BOMB || tragetTile == BOMB_ON_GOAL)
                {
                    if (TryPushBomb(nextPos))
                    {
                        _playerPos = nextPos;
                        _moveCount++;
                    }
                }
            }

                Console.WriteLine("게임 끝");

            
            static void PrintGuideText()
            {
                Console.Clear();
                Console.WriteLine("W : 위로 / S : 아래로 / A : 왼쪽 / D : 오른쪽");
                Console.WriteLine("모든 폭탄을 목표지점으로 옮기세요.");
                Console.WriteLine();
            }
            static void PrintMoveCount()
            {
                Console.SetCursorPosition(left: 0, top: 4);
                Console.WriteLine($"이동 거리 : {_moveCount}");
                Console.WriteLine();
            }
            static bool IsGameClear()
            {
                for (int y = 0; y < map.GetLength(dimension: 1); y++)
                {
                    for (int x = 0; x < map.GetLength(dimension: 0); x++)
                    {
                        if (map[y, x] == BOMB || map[y, x] == GOAL)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            static void PrintClearText()
            {
                Console.WriteLine();
                Console.WriteLine("축하합니다.");
                Console.WriteLine("총 이동거리 : {_moveCount}");
                Console.WriteLine();

            }

            static bool TryGetInput(out ConsoleKey inputKey)
            {
                ConsoleKeyInfo KeyInfo = Console.ReadKey(true);
                inputKey = KeyInfo.Key;

                return inputKey == ConsoleKey.W ||
                       inputKey == ConsoleKey.A ||
                       inputKey == ConsoleKey.S ||
                       inputKey == ConsoleKey.D ||
                       inputKey == ConsoleKey.Q;
            }
            static Position GetNextPosition(ConsoleKey inputKey)
            {
                //현재 위치
                int newX = _playerPos.X;
                int newY = _playerPos.Y;
                //입력 키
                if (inputKey == ConsoleKey.W) newY--;
                else if (inputKey == ConsoleKey.S) newY++;
                else if (inputKey == ConsoleKey.A) newX--;
                else if (inputKey == ConsoleKey.D) newX++;

                return new Position()
                {
                    X = newX,
                    Y = newY
                };
                //결과(다음 위치)
            }

            static char GetTile(Position pos)
            {
                return map[pos.Y, pos.X];
            }
            static void SetTile(Position pos, char tile)
            {
                map[pos.Y, pos.X] = tile;
            }
            static bool IsOutOfArray(Position pos)
            {
                //배열의 인덱스 범위 밖인가??
                bool outX = pos.X < 0 || map.GetLength(dimension: 1) <= pos.X;
                bool outY = pos.Y < 0 || map.GetLength(dimension: 1) <= pos.Y;

                return outX || outY;
            }
            //플레이어 이동 먼저 구현, 나중에 박스도 함수 사용할 수 있도록 할거임
            static void Move(Position from, Position to, char target)
            {
                //출발 지점을 기본 타일로 바꾸기
                char originTile = GetOriginTile(GetTile(from));
                SetTile(from, originTile);
                SetTile(from, EMPTY);
                // 다음 위치에 'P'를 넣어야함.
                char targetTile = GetTile(to);
                char nextTile = GetConvertTile(PLAYER, targetTile);
                SetTile(to, nextTile);

            }
            static char GetConvertTile(char mover, char under)
            {
                if (mover == PLAYER)
                {
                    if (under == GOAL)
                    {
                        return PLAYER_ON_GOAL;
                    }
                    else
                    {
                        return PLAYER;
                    }
                }
                else if (mover == BOMB)
                {
                    if (under == GOAL)
                    {
                        return BOMB_ON_GOAL;
                    }
                    else
                    {
                        return BOMB;
                    }
                }
                return under;
            }
                //이동 대상의 밑에 있던 원본 타일 반환
                static char GetOriginTile(char tile)
                {
                    switch (tile)
                    {
                        case PLAYER:
                            return EMPTY;
                        case PLAYER_ON_GOAL:
                            return GOAL;
                        case BOMB:
                            return EMPTY;
                        case BOMB_ON_GOAL:
                            return GOAL;
                        default:
                            return tile;
                            /*
                             return tile switch
                             {
                                PLAYER => EMPTY,
                                PLAYER_ON_GOAL => GOAL,
                                BOMB => EMPTY,
                                BOMB_ON_GOAL => GOAL,
                                _ => tile
                              };
                            */
                    }
                }
                static bool TryPushBomb(Position bombPos)
                {
                    //방향
                    Position direction = GetDirection(_playerPos, bombPos);
                    //구해진 방향으로 한 칸 전진했을 때의 위치
                    Position nextPos = AddDirecrion(bombPos, direction);

                    //맵 밖으로 나가는지 확인
                    if (IsOutOfArray(nextPos)) return false;

                    char nextTile = GetTile(nextPos);
                    if (!(nextTile == EMPTY || nextTile == GOAL)) return false;
                    //-----------------------------------------------------

                    //폭탄 이동
                    Move(bombPos, nextPos, BOMB);
                    //플레이어를 폭탄이 있던 위치로 이동
                    Move(_playerPos, bombPos, PLAYER);

                    return true;
                }

                static Position GetDirection(Position from, Position to)
                {
                    return new Position()
                    {
                        X = to.X - from.X,
                        Y = to.Y - from.Y
                    };
                }
                static Position AddDirecrion(Position pos, Position direction)
                {
                    return new Position()
                    {
                        X = pos.X + direction.X,
                        Y = pos.Y + direction.Y
                    };
                }

                static void PrintMap()
                {
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            char tile = map[i, j];
                            if (tile == WALL) Console.Write("🧱");
                            else if(tile == PLAYER) Console.Write("😊");
                            else if(tile == PLAYER_ON_GOAL) Console.Write("😁");
                            else if (tile == BOMB) Console.Write("💣");
                            else if (tile == BOMB_ON_GOAL) Console.Write("✅");
                            else if (tile == GOAL) Console.Write("🕳️");
                            else Console.Write("  ");

                        }
                        Console.WriteLine();
                    }

                }
            }
        }
        public struct Position
        {
            public int X;
            public int Y;
        }
    }
