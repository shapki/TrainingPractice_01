using System;
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

            KeyPreview = true;
            KeyDown += Task7_KeyDown;
        }

        private void Task7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && _selectedPiece != Point.Empty)
            {
                ClearSelection();
                UpdateUi();
            }
        }

        private void ClearSelection()
        {
            ClearHighlighting();
            _selectedPiece = Point.Empty;
            _selectedLabel = null;
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
            if (MessageBox.Show("Цель игры: куры должны занять верхний квадрат 3х3, а лисы должны съесть столько кур, чтобы курам не хватило места для победы.\nКуры ходят первыми, перемещаясь на 1 клетку вверх, влево или вправо. Лисы ходят после кур и обязаны есть, если есть такая возможность. Если у лисы есть несколько вариантов поедания, она выбирает наиболее длинный вариант.\n\nЕсли вы выбрали курицу, но хотите отменить выбор - нажмите пробел", "Лисы и куры - Информация", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
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
                {
                    MessageBox.Show("Выберите курицу для перемещения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (_gameBoardFacade.IsValidChickenMove(_selectedPiece, cellLocation))
                {
                    _gameBoardFacade.MoveChicken(_selectedPiece, cellLocation);
                    ClearHighlighting();
                    _selectedPiece = Point.Empty;
                    _selectedLabel = null;
                    MakeFoxMove();
                }
                else
                {
                    MessageBox.Show("Недопустимый ход.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
