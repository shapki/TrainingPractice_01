using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Shapkin_Task_7
{
    public partial class Task7 : Form
    {
        private const int BoardSize = 7;
        private const int FirstRow = 3;

        private GameBoardFacade _gameBoardFacade;
        private Point _selectedPiece;
        private Label _selectedLabel;

        public Task7()
        {
            InitializeComponent();
            CreateBoard();
            InitializeGame();

            foreach (Control control in tableLayoutPanel1.Controls)
                if (control is Label cellLabel)
                    cellLabel.Click += Cell_Click;

            HelpButtonClicked += ShapedForm1_HelpButtonClicked;
        }

        private void CreateBoard()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.ColumnCount = BoardSize;
            tableLayoutPanel1.RowCount = BoardSize;

            for (int i = 0; i < BoardSize; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / BoardSize));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / BoardSize));
            }

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    Label cellLabel = new Label
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                        BackColor = Color.White
                    };

                    // Закрашиваем угловые ячейки серым и делаем их недоступными
                    if ((row < 2 && (col < 2 || col > 4)) || (row > 4 && (col < 2 || col > 4)))
                    {
                        cellLabel.BackColor = Color.Gray;
                        cellLabel.Enabled = false;
                    }

                    tableLayoutPanel1.Controls.Add(cellLabel, col, row);
                }
            }
        }

        private void InitializeGame()
        {
            _gameBoardFacade = new GameBoardFacade(BoardSize);
            _selectedPiece = Point.Empty;
            _selectedLabel = null; // Очищаем выбранный лейбл
            UpdateUi();
        }

        private void ShapedForm1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Цель игры: куры должны занять верхний квадрат 3х3, а лисы должны съесть столько кур, чтобы курам не хватило места для победы.\nКуры ходят первыми, перемещаясь на 1 клетку вверх, влево или вправо. Лисы ходят после кур и обязаны есть, если есть такая возможность. Если у лисы есть несколько вариантов поедания, она выбирает наиболее длинный вариант.", "Лисы и куры - Информация", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                e.Cancel = true;
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            if (!(sender is Label clickedCell)) return;

            Point cellLocation = GetCellLocation(clickedCell);

            if (_gameBoardFacade.CurrentPlayer == PlayerType.Chicken)
                HandleChickenMove(cellLocation, clickedCell);
            else
                HandleFoxMove(cellLocation, clickedCell);
        }

        private Point GetCellLocation(Label cell)
        {
            TableLayoutPanelCellPosition position = tableLayoutPanel1.GetPositionFromControl(cell);
            return new Point(position.Column, position.Row);
        }

        private void HandleChickenMove(Point cellLocation, Label clickedCell)
        {
            if (_selectedPiece == Point.Empty)
            {
                if (_gameBoardFacade.GetCellType(cellLocation) == CellType.Chicken)
                {
                    if (cellLocation.Y >= FirstRow) // Проверка, что курица находится в первом ряду или ниже
                    {
                        if (_gameBoardFacade.CanChickenMove(cellLocation))
                        {
                            _selectedPiece = cellLocation;
                            _selectedLabel = clickedCell;
                            HighlightSelectedPiece();
                        }
                        else
                        {
                            MessageBox.Show("У выбранной курицы нет доступных ходов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _selectedPiece = Point.Empty;
                            _selectedLabel = null;
                        }
                    }
                    else
                        MessageBox.Show("Выбранная курица не может ходить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Выберите курицу для перемещения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Перемещение выбранной курицы
                if (_gameBoardFacade.IsValidChickenMove(_selectedPiece, cellLocation))
                {
                    _gameBoardFacade.MoveChicken(_selectedPiece, cellLocation);
                    ClearHighlighting();
                    _selectedPiece = Point.Empty;
                    _selectedLabel = null;

                    MakeFoxMove();
                }
                else
                    MessageBox.Show("Недопустимый ход.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateUi();
            CheckForGameOver();
        }

        private void HandleFoxMove(Point cellLocation, Label clickedCell)
        {
            if (_selectedPiece == Point.Empty)
            {
                // Выбор лисы для перемещения
                if (_gameBoardFacade.GetCellType(cellLocation) == CellType.Fox)
                {
                    _selectedPiece = cellLocation;
                    _selectedLabel = clickedCell;
                    HighlightSelectedPiece();
                }
                else
                    MessageBox.Show("Выберите лису для перемещения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Перемещение выбранной лисы
                if (_gameBoardFacade.IsValidFoxMove(_selectedPiece, cellLocation))
                {
                    _gameBoardFacade.MoveFox(_selectedPiece, cellLocation);
                    ClearHighlighting();
                    _selectedPiece = Point.Empty;
                    _selectedLabel = null;

                    _gameBoardFacade.SwitchPlayer();
                }
                else
                {
                    MessageBox.Show("Недопустимый ход.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateUi();
            CheckForGameOver();
        }

        private void MakeFoxMove()
        {
            bool foxMoved = true;
            while (foxMoved && _gameBoardFacade.CurrentPlayer == PlayerType.Fox)
            {
                foxMoved = _gameBoardFacade.MakeFoxMove();
                UpdateUi();
                CheckForGameOver();

                if (!foxMoved && _gameBoardFacade.CurrentPlayer == PlayerType.Fox)
                {
                    foxMoved = _gameBoardFacade.MakeRandomFoxMove(); //пытаемся сделать случайный ход, если не удалось съесть
                }

                if (!foxMoved && _gameBoardFacade.CurrentPlayer == PlayerType.Fox)
                {
                    MessageBox.Show("Куры победили! Лисы не могут больше ходить.", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetGame();
                    return;
                }

            }
        }

        private void UpdateUi()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (!(tableLayoutPanel1.GetControlFromPosition(col, row) is Label cell)) continue;

                    CellType cellType = _gameBoardFacade.GetCellType(new Point(col, row));

                    string cellText = "";
                    if (cellType == CellType.Chicken)
                        cellText = "К";
                    else if (cellType == CellType.Fox)
                        cellText = "Л";

                    cell.Text = cellText;
                }
            }

            eatenChickenLabel.Text = "Съедено кур: " + _gameBoardFacade.EatenChickensCount; // Обновляем кол-во съеденных кур
            chickenCountLabel.Text = "Кур: " + _gameBoardFacade.ChickenCount; // Обновляем кол-во кур
        }


        private void CheckForGameOver()
        {
            if (_gameBoardFacade.ChickenCount <= 8) //условие выигрыша лис
            {
                MessageBox.Show("Лисы победили! Курам не хватает места для победы.", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
            if (_gameBoardFacade.CheckChickenWin()) // условие победы кур
            {
                MessageBox.Show("Куры победили! Им удалось занять верхний квадрат.", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
            if (_gameBoardFacade.CurrentPlayer == PlayerType.Chicken && !_gameBoardFacade.CanChickensMove())
            {
                MessageBox.Show("Лисы победили! Куры не могут ходить.", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
            if (_gameBoardFacade.CurrentPlayer == PlayerType.Fox && !_gameBoardFacade.CanFoxesMove())
            {
                MessageBox.Show("Куры победили! Лисы не могут ходить.", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
        }

        private void ResetGame()
        {
            InitializeGame();
            UpdateUi();
        }

        private void HighlightSelectedPiece()
        {
            if (_selectedLabel != null)
                _selectedLabel.ForeColor = (_gameBoardFacade.CurrentPlayer == PlayerType.Chicken) ? Color.Green : Color.Red;
        }

        private void ClearHighlighting()
        {
            if (_selectedLabel != null)
                _selectedLabel.ForeColor = Color.Black;
        }

    }

    public class GameBoardFacade
    {
        private readonly GameBoard _gameBoard;
        private readonly GameState _gameState;
        private readonly FoxAi _foxAi;
        private readonly Random _random = new Random(); // Добавляем Random для случайных ходов

        public GameBoardFacade(int boardSize)
        {
            _gameBoard = new GameBoard(boardSize);
            _gameState = new GameState(_gameBoard);
            _foxAi = new FoxAi(_gameBoard);

            _gameBoard.InitializeBoard();
        }

        public int EatenChickensCount => _gameBoard.EatenChickensCount;
        public int ChickenCount => _gameBoard.ChickenCount;
        public PlayerType CurrentPlayer => _gameState.CurrentPlayer;

        public CellType GetCellType(Point point)
        {
            return _gameBoard.GetCell(point);
        }

        public bool IsValidChickenMove(Point from, Point to)
        {
            return _gameBoard.IsValidChickenMove(from, to);
        }

        public bool IsValidFoxMove(Point from, Point to)
        {
            return _gameBoard.IsValidFoxMove(from, to);
        }

        public void MoveChicken(Point from, Point to)
        {
            _gameBoard.MoveChicken(from, to);
            _gameState.SwitchPlayer();
        }

        public void MoveFox(Point from, Point to)
        {
            _gameBoard.MoveFox(from, to);
            _gameState.SwitchPlayer();
        }

        public bool MakeFoxMove()
        {
            var (bestMove, targetCell, foxPosition) = _foxAi.GetBestFoxMove();

            if (bestMove != Point.Empty)
            {
                _gameBoard.MoveFox(foxPosition, targetCell);
                _gameState.SwitchPlayer();
                return true;
            }
            else
                return false;
        }

        // Метод для случайного хода лисы, если нет возможности съесть
        public bool MakeRandomFoxMove()
        {
            Point foxPosition = _gameBoard.GetRandomFoxPosition();

            if (foxPosition == Point.Empty) { return false; }

            List<Point> possibleMoves = _gameBoard.GetPossibleFoxMoves(foxPosition);

            if (possibleMoves.Count > 0)
            {
                int randomIndex = _random.Next(possibleMoves.Count);
                Point targetCell = possibleMoves[randomIndex];
                _gameBoard.MoveFox(foxPosition, targetCell);
                _gameState.SwitchPlayer();
                return true;
            }
            return false;
        }

        public bool CheckChickenWin()
        {
            return _gameBoard.CheckChickenWin();
        }

        public void SwitchPlayer()
        {
            _gameState.SwitchPlayer();
        }

        public bool CanChickensMove()
        {
            return _gameBoard.CanChickensMove();
        }

        public bool CanFoxesMove()
        {
            return _gameBoard.CanFoxesMove();
        }

        public bool CanChickenMove(Point position)
        {
            return _gameBoard.CanChickenMove(position);
        }
    }

    public class GameBoard
    {
        private readonly int _boardSize;
        private readonly CellType[,] _board;
        private readonly Random _random = new Random(); // Random для случайных ходов

        public int ChickenCount { get; private set; } = 20;
        public int EatenChickensCount { get; private set; } = 0;

        public GameBoard(int boardSize)
        {
            _boardSize = boardSize;
            _board = new CellType[boardSize, boardSize];
        }

        public void InitializeBoard()
        {
            // Инициализация пустой доски
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
                return CellType.Empty; // Выход за границы
            return _board[point.Y, point.X];
        }

        public bool IsValidChickenMove(Point from, Point to)
        {
            // Проверка, что ячейка назначения находится в пределах доски
            if (to.X < 0 || to.X >= _boardSize || to.Y < 0 || to.Y >= _boardSize)
                return false;

            // Проверка, что ячейка назначения пуста
            if (_board[to.Y, to.X] != CellType.Empty)
                return false;

            if ((to.Y == from.Y - 1 && to.X == from.X) ||
                (to.Y == from.Y && Math.Abs(to.X - from.X) == 1))
                return true;

            return false;
        }

        public bool IsValidFoxMove(Point from, Point to)
        {
            // Проверка, что ячейка назначения находится в пределах доски
            if (to.X < 0 || to.X >= _boardSize || to.Y < 0 || to.Y >= _boardSize)
                return false;

            // Проверка, что ячейка назначения пуста
            if (_board[to.Y, to.X] != CellType.Empty)
            {
                return false;
            }

            // Проверка, что перемещение выполнено на одну клетку вверх, вниз, влево или вправо
            if (Math.Abs(to.X - from.X) + Math.Abs(to.Y - from.Y) == 1)
                return true;

            return false;
        }

        public void MoveChicken(Point from, Point to)
        {
            _board[to.Y, to.X] = CellType.Chicken;
            _board[from.Y, from.X] = CellType.Empty;
        }

        public void MoveFox(Point from, Point to)
        {
            // Вызываем метод для поедания кур
            EatChickens(from, to);

            // Перемещение лисы на новую позицию
            _board[to.Y, to.X] = CellType.Fox;
            _board[from.Y, from.X] = CellType.Empty;
        }

        // Метод поедания кур
        private void EatChickens(Point from, Point to)
        {
            // Получаем направление движения лисы
            int deltaX = Math.Sign(to.X - from.X);
            int deltaY = Math.Sign(to.Y - from.Y);

            int currentX = from.X + deltaX;
            int currentY = from.Y + deltaY;

            // Проходим по направлению движения лисы
            while (currentX >= 0 && currentX < _boardSize && currentY >= 0 && currentY < _boardSize)
            {
                // Если на пути курица, то съедаем ее
                if (_board[currentY, currentX] == CellType.Chicken)
                {
                    _board[currentY, currentX] = CellType.Empty;
                    ChickenCount--;
                    EatenChickensCount++;
                    break; // После съедания курицы заканчиваем проверку
                }
                else if (_board[currentY, currentX] != CellType.Empty)
                {
                    break; // Если на пути не курица, и не пустая клетка, заканчиваем проверку
                }

                currentX += deltaX;
                currentY += deltaY;
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
                    {
                        count++;
                    }
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
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CanChickenMove(Point position)
        {
            // Проверяем возможность хода вверх
            if (position.Y > 0 && _board[position.Y - 1, position.X] == CellType.Empty)
                return true;

            // Проверяем возможность хода влево
            if (position.X > 0 && _board[position.Y, position.X - 1] == CellType.Empty)
                return true;

            // Проверяем возможность хода вправо
            if (position.X < _boardSize - 1 && _board[position.Y, position.X + 1] == CellType.Empty)
                return true;

            return false;
        }

        public bool CanFoxesMove()
        {
            for (int row = 0; row < _boardSize; row++)
                for (int col = 0; col < _boardSize; col++)
                    if (_board[row, col] == CellType.Fox)
                    {
                        Point currentPosition = new Point(col, row);
                        if (HasEatingMove(currentPosition) || GetPossibleFoxMoves(currentPosition).Count > 0)
                            return true;
                    }

            return false;
        }

        private bool HasEatingMove(Point foxPosition)
        {
            // Проверяем все направления на возможность съесть
            int[] deltaX = { 0, 0, 1, -1 };
            int[] deltaY = { 1, -1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int newX = foxPosition.X + deltaX[i];
                int newY = foxPosition.Y + deltaY[i];

                if (newX >= 0 && newX < _boardSize && newY >= 0 && newY < _boardSize)
                {
                    // Проверяем, есть ли курица в соседней клетке
                    if (_board[newY, newX] == CellType.Chicken)
                    {
                        // Получаем координаты следующей клетки в том же направлении
                        int nextX = newX + deltaX[i];
                        int nextY = newY + deltaY[i];

                        // Проверяем, является ли следующая клетка пустой
                        if (nextX >= 0 && nextX < _boardSize && nextY >= 0 && nextY < _boardSize && _board[nextY, nextX] == CellType.Empty)
                        {
                            return true; // Найдена возможность съесть
                        }
                    }
                }
            }

            return false;
        }

        // Метод для получения случайной позиции лисы на доске
        public Point GetRandomFoxPosition()
        {
            List<Point> foxPositions = new List<Point>();

            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    if (_board[row, col] == CellType.Fox)
                    {
                        foxPositions.Add(new Point(col, row));
                    }
                }
            }

            if (foxPositions.Count > 0)
            {
                int randomIndex = _random.Next(foxPositions.Count);
                return foxPositions[randomIndex];
            }

            return Point.Empty;
        }

        // Метод для получения возможных ходов лисы (без поедания)
        public List<Point> GetPossibleFoxMoves(Point foxPosition)
        {
            List<Point> possibleMoves = new List<Point>();

            int[] deltaX = { 0, 0, 1, -1 };
            int[] deltaY = { 1, -1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int newX = foxPosition.X + deltaX[i];
                int newY = foxPosition.Y + deltaY[i];

                if (newX >= 0 && newX < _boardSize && newY >= 0 && newY < _boardSize && _board[newY, newX] == CellType.Empty)
                {
                    possibleMoves.Add(new Point(newX, newY));
                }
            }

            return possibleMoves;
        }

    }

    public class GameState
    {
        public PlayerType CurrentPlayer { get; private set; } = PlayerType.Chicken;
        private readonly GameBoard _gameBoard;

        public GameState(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == PlayerType.Chicken) ? PlayerType.Fox : PlayerType.Chicken;
        }
    }

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

    public enum CellType
    {
        Empty,
        Chicken,
        Fox
    }

    public enum PlayerType
    {
        Chicken,
        Fox
    }
}
