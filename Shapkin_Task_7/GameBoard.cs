using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shapkin_Task_7
{
    public class GameBoard
    {
        private readonly int _boardSize;
        private readonly CellType[,] _board;
        private readonly Random _random = new Random();

        public int ChickenCount { get; private set; } = 20;
        public int EatenChickensCount { get; private set; } = 0;

        public GameBoard(int boardSize)
        {
            _boardSize = boardSize;
            _board = new CellType[boardSize, boardSize];
        }

        public void InitializeBoard()
        {
            for (int row = 0; row < _boardSize; row++)
                for (int col = 0; col < _boardSize; col++)
                    _board[row, col] = CellType.Empty;

            // Начальное размещение кур
            _board[3, 0] = CellType.Chicken;
            _board[3, 1] = CellType.Chicken;
            _board[3, 2] = CellType.Chicken;
            _board[3, 3] = CellType.Chicken;
            _board[3, 4] = CellType.Chicken;
            _board[3, 5] = CellType.Chicken;
            _board[3, 6] = CellType.Chicken;
            _board[4, 0] = CellType.Chicken;
            _board[4, 1] = CellType.Chicken;
            _board[4, 2] = CellType.Chicken;
            _board[4, 3] = CellType.Chicken;
            _board[4, 4] = CellType.Chicken;
            _board[4, 5] = CellType.Chicken;
            _board[4, 6] = CellType.Chicken;
            _board[5, 2] = CellType.Chicken;
            _board[5, 3] = CellType.Chicken;
            _board[5, 4] = CellType.Chicken;
            _board[6, 2] = CellType.Chicken;
            _board[6, 3] = CellType.Chicken;
            _board[6, 4] = CellType.Chicken;

            // Начальное размещение лис
            _board[2, 2] = CellType.Fox;
            _board[2, 4] = CellType.Fox;
        }

        public CellType GetCell(Point point)
        {
            if (point.X < 0 || point.X >= _boardSize || point.Y < 0 || point.Y >= _boardSize)
                return CellType.Empty;

            return _board[point.Y, point.X];
        }

        public bool IsValidChickenMove(Point from, Point to)
        {
            if (to.X < 0 || to.X >= _boardSize || to.Y < 0 || to.Y >= _boardSize)
                return false;

            if (_board[to.Y, to.X] != CellType.Empty)
                return false;

            if ((to.Y == from.Y - 1 && to.X == from.X) ||
                (to.Y == from.Y && Math.Abs(to.X - from.X) == 1))
                return true;

            return false;
        }

        public bool IsValidFoxMove(Point from, Point to)
        {
            if (to.X < 0 || to.X >= _boardSize || to.Y < 0 || to.Y >= _boardSize)
                return false;

            if (_board[to.Y, to.X] != CellType.Empty)
                return false;

            if ((to.X == from.X && Math.Abs(to.Y - from.Y) == 1) ||
                (to.Y == from.Y && Math.Abs(to.X - from.X) == 1))
            {
                // Проверка на серую клетку
                if ((to.Y < 2 && (to.X < 2 || to.X > 4)) || (to.Y > 4 && (to.X < 2 || to.X > 4)))
                    return false;

                return true;
            }

            return false;
        }

        public void MoveChicken(Point from, Point to)
        {
            _board[to.Y, to.X] = CellType.Chicken;
            _board[from.Y, from.X] = CellType.Empty;
        }

        public void MoveFox(Point from, Point to)
        {
            EatChickens(from, to);
            _board[to.Y, to.X] = CellType.Fox;
            _board[from.Y, from.X] = CellType.Empty;
        }

        private void EatChickens(Point from, Point to)
        {
            int deltaX = Math.Sign(to.X - from.X);
            int deltaY = Math.Sign(to.Y - from.Y);

            int chickenX = from.X + deltaX;
            int chickenY = from.Y + deltaY;

            if (chickenX >= 0 && chickenX < _boardSize && chickenY >= 0 && chickenY < _boardSize)
            {
                if (_board[chickenY, chickenX] == CellType.Chicken)
                {
                    _board[chickenY, chickenX] = CellType.Empty;
                    ChickenCount--;
                    EatenChickensCount++;
                }
            }
        }

        public bool CheckChickenWin()
        {
            int count = 0;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (_board[row, col] == CellType.Chicken)
                        count++;
                }
            }
            return count == 9;
        }

        public bool CanChickensMove()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    if (_board[row, col] == CellType.Chicken)
                    {
                        if (CanChickenMove(new Point(col, row)))
                            return true;
                    }
                }
            }
            return false;
        }

        public bool CanChickenMove(Point position)
        {
            if (position.Y > 0 && _board[position.Y - 1, position.X] == CellType.Empty)
                return true;

            if (position.X > 0 && _board[position.Y, position.X - 1] == CellType.Empty)
                return true;

            if (position.X < _boardSize - 1 && _board[position.Y, position.X + 1] == CellType.Empty)
                return true;

            return false;
        }

        public bool CanFoxesMove()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    if (_board[row, col] == CellType.Fox)
                    {
                        Point currentPosition = new Point(col, row);
                        if (HasEatingMove(currentPosition) || GetPossibleFoxMoves(currentPosition).Count > 0)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool HasEatingMove(Point foxPosition)
        {
            int[] deltaX = { 0, 0, 1, -1 };
            int[] deltaY = { 1, -1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int newX = foxPosition.X + deltaX[i];
                int newY = foxPosition.Y + deltaY[i];

                if (newX >= 0 && newX < _boardSize && newY >= 0 && newY < _boardSize)
                {
                    if (_board[newY, newX] == CellType.Chicken)
                    {
                        int nextX = newX + deltaX[i];
                        int nextY = newY + deltaY[i];

                        if (nextX >= 0 && nextX < _boardSize && nextY >= 0 && nextY < _boardSize &&
                            _board[nextY, nextX] == CellType.Empty)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Point GetRandomFoxPosition()
        {
            List<Point> foxPositions = new List<Point>();

            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    if (_board[row, col] == CellType.Fox)
                        foxPositions.Add(new Point(col, row));
                }
            }

            if (foxPositions.Count > 0)
            {
                int randomIndex = _random.Next(foxPositions.Count);
                return foxPositions[randomIndex];
            }

            return Point.Empty;
        }

        public List<Point> GetPossibleFoxMoves(Point foxPosition)
        {
            List<Point> possibleMoves = new List<Point>();
            int[] deltaX = { 0, 0, 1, -1 };
            int[] deltaY = { 1, -1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int newX = foxPosition.X + deltaX[i];
                int newY = foxPosition.Y + deltaY[i];

                if (newX >= 0 && newX < _boardSize && newY >= 0 && newY < _boardSize &&
                    _board[newY, newX] == CellType.Empty)
                {
                    // Проверка на серую клетку
                    if (!(newY < 2 && (newX < 2 || newX > 4)) && !(newY > 4 && (newX < 2 || newX > 4)))
                        possibleMoves.Add(new Point(newX, newY));
                }
            }

            return possibleMoves;
        }
    }
}