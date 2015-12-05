using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesLib
{
    public class Game
    {
        private int[,] _gameBoard;
        private Random _random;
        private Player _player;
        private int _bombCount;
        private int _length;
        private int _height;


        public Game(int length, int height, int bombCount, string playerName)
        {
            _random = new Random();
            _gameBoard = new int[length, height];
            _player = new Player { PlayerName = playerName };
            _bombCount = bombCount;
            _length = length;
            _height = height;
            initTable();
#if DEBUG
            writeTable();
#endif
        }

        private void initTable ()
        {
            #region bombaları yerleştir
            while (_bombCount != 0)
            {
                int x = _random.Next(_length);
                int y = _random.Next(_height);

                if(_gameBoard[x, y] != 9)
                {
                    _gameBoard[x, y] = 9;
                    _bombCount--;
                }
            }
            #endregion

            #region sayıları yerleştir
            for(int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if(_gameBoard[i, j] != 9)
                        _gameBoard[i, j] = countNeighbours(i, j);
                }
            }

            #endregion
        }

        private int countNeighbours(int x, int y)
        {
            int bombs = 0;
            for(int i = x-1; i <= x+1; i++)
                for (int j = y-1; j <= y+1; j++)
                {
                    if (i < 0 || i > _length - 1 || j < 0 || j > _height - 1)
                        continue;
                    if (_gameBoard[i, j] == 9)
                        bombs++;
                }
            return bombs;
        }

        private void writeTable()
        {
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Console.Write("{0} ", _gameBoard[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
