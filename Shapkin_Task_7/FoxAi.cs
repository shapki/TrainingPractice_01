using System.Drawing;

namespace Shapkin_Task_7
{
    public class FoxAi
    {
        private readonly GameBoard _gameBoard;

        public FoxAi(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public (Point bestMove, Point targetCell, Point foxPosition) GetBestFoxMove()
        {
            Point bestMove = Point.Empty;
            Point targetCell = Point.Empty;
            Point foxPositionToMove = Point.Empty;

            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Point currentFoxPosition = new Point(col, row);
                    if (_gameBoard.GetCell(currentFoxPosition) == CellType.Fox)
                    {
                        (Point move, int eaten) move = GetLongestEatingMove(currentFoxPosition);
                        if (move.eaten > 0)
                        {
                            bestMove = currentFoxPosition;
                            targetCell = move.move;
                            foxPositionToMove = currentFoxPosition;
                            return (bestMove, targetCell, foxPositionToMove);
                        }
                    }
                }
            }

            return (bestMove, targetCell, foxPositionToMove);
        }

        private (Point move, int eaten) GetLongestEatingMove(Point foxPosition)
        {
            int[] deltaX = { 0, 0, 1, -1 };
            int[] deltaY = { 1, -1, 0, 0 };
            (Point move, int eaten) bestMove = (Point.Empty, 0);

            for (int i = 0; i < 4; i++)
            {
                int newX = foxPosition.X + deltaX[i];
                int newY = foxPosition.Y + deltaY[i];

                if (newX >= 0 && newX < 7 && newY >= 0 && newY < 7)
                {
                    // Проверяем, есть ли курица в соседней клетке
                    if (_gameBoard.GetCell(new Point(newX, newY)) == CellType.Chicken)
                    {
                        // Получаем координаты следующей клетки в том же направлении
                        int nextX = newX + deltaX[i];
                        int nextY = newY + deltaY[i];

                        // Проверяем, является ли следующая клетка пустой
                        if (nextX >= 0 && nextX < 7 && nextY >= 0 && nextY < 7 && _gameBoard.GetCell(new Point(nextX, nextY)) == CellType.Empty)
                        {
                            bestMove = (new Point(nextX, nextY), 1);
                            return bestMove;
                        }
                    }
                }
            }

            return bestMove;
        }
    }
}
