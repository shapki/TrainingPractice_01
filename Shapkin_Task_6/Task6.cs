using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Shapkin_Task_6
{
    public partial class Task6 : Form
    {
        private const int SudokuSize = 9;
        private const int SubgridSize = 3;
        private TextBox[,] _textBoxes = new TextBox[SudokuSize, SudokuSize];
        private int[,] _initialSudokuGrid = new int[SudokuSize, SudokuSize];
        private SudokuFacade _sudokuFacade;
        private string _baseDirectory;
        private string _saveFilePath;
        private Timer _saveTimer;
        private bool _validating = false; // Для предотвращения рекурсивной валидации

        public Task6()
        {
            InitializeComponent();
            _baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Shapkin_Task_5");
            _baseDirectory = Path.GetFullPath(_baseDirectory);

            _saveFilePath = Path.Combine(_baseDirectory, "sudoku_save.txt");

            _sudokuFacade = new SudokuFacade(tableLayoutPanel1, _textBoxes, _initialSudokuGrid);
            _sudokuFacade.InitializeGrid();

            if (File.Exists(_saveFilePath))
                LoadSavedData();
            else
                LoadInitialData("easy.txt");

            DisplaySudoku();

            difficultyComboBox.Items.AddRange(new string[] { "Легкий", "Средний", "Сложный" });
            difficultyComboBox.SelectedIndex = 0;

            _saveTimer = new Timer();
            _saveTimer.Interval = 60000; // 1 минута
            _saveTimer.Tick += SaveTimer_Tick;
            _saveTimer.Start();
        }

        private void LoadInitialData(string fileName)
        {
            string filePath = Path.Combine(_baseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл не найден: {filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length != SudokuSize)
                {
                    MessageBox.Show("Неверный формат файла судоку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int row = 0; row < SudokuSize; row++)
                {
                    string[] values = lines[row].Split(' ');

                    if (values.Length != SudokuSize)
                    {
                        MessageBox.Show("Неверный формат файла судоку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int col = 0; col < SudokuSize; col++)
                    {
                        if (int.TryParse(values[col], out int value))
                        {
                            _initialSudokuGrid[row, col] = value;
                        }
                        else
                        {
                            MessageBox.Show("Неверное число в файле судоку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplaySudoku()
        {
            _sudokuFacade.DisplaySudoku(_initialSudokuGrid);
            ValidateSudoku();
        }

        private void LoadSavedData()
        {
            try
            {
                string[] lines = File.ReadAllLines(_saveFilePath);

                if (lines.Length != SudokuSize)
                {
                    MessageBox.Show("Неверный формат файла сохранения судоку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int row = 0; row < SudokuSize; row++)
                {
                    string[] values = lines[row].Split(' ');

                    if (values.Length != SudokuSize)
                    {
                        MessageBox.Show("Неверный формат файла сохранения судоку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int col = 0; col < SudokuSize; col++)
                    {
                        if (int.TryParse(values[col], out int value))
                        {
                            _initialSudokuGrid[row, col] = value;
                            _textBoxes[row, col].Text = value == 0 ? "" : value.ToString();
                            _textBoxes[row, col].ReadOnly = value != 0;
                            _textBoxes[row, col].ForeColor = value != 0 ? Color.Black : Color.Blue;
                        }
                        else
                        {
                            MessageBox.Show("Неверное число в файле сохранения судоку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_saveFilePath))
                {
                    for (int row = 0; row < SudokuSize; row++)
                    {
                        for (int col = 0; col < SudokuSize; col++)
                        {
                            // Если поле для ввода можно редактировать, то берем введенное значение, иначе берем значение из initialSudokuGrid
                            int value = _textBoxes[row, col].ReadOnly ? _initialSudokuGrid[row, col] : (string.IsNullOrEmpty(_textBoxes[row, col].Text) ? 0 : int.Parse(_textBoxes[row, col].Text));
                            writer.Write(value + (col == SudokuSize - 1 ? "" : " "));
                        }
                        writer.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void difficultyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLevel = difficultyComboBox.SelectedItem.ToString();
            string fileName = "";

            switch (selectedLevel)
            {
                case "Легкий":
                    fileName = "easy.txt";
                    break;
                case "Средний":
                    fileName = "medium.txt";
                    break;
                case "Сложный":
                    fileName = "hard.txt";
                    break;
                default:
                    fileName = "easy.txt";
                    break;
            }

            LoadInitialData(fileName);
            DisplaySudoku();
        }

        private void SaveTimer_Tick(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Task6_FormClosing(object sender, FormClosingEventArgs e)
        {
            _saveTimer.Stop();
            SaveData();
        }

        public void ValidateSudoku()
        {
            if (_validating) return; // Предотвращение рекурсивной валидации
            _validating = true;
            try
            {
                // Сначала очищаем все ошибки
                ClearAllErrors();

                bool isValid = true;

                // Проверка строк
                for (int row = 0; row < SudokuSize; row++)
                {
                    var rowValues = Enumerable.Range(0, SudokuSize)
                        .Select(col => GetCellValue(row, col))
                        .Where(value => value != 0);

                    if (rowValues.Count() != rowValues.Distinct().Count())
                    {
                        isValid = false;
                        HighlightErrorInRow(row);
                    }
                }

                // Проверка столбцов
                for (int col = 0; col < SudokuSize; col++)
                {
                    var colValues = Enumerable.Range(0, SudokuSize)
                        .Select(row => GetCellValue(row, col))
                        .Where(value => value != 0);

                    if (colValues.Count() != colValues.Distinct().Count())
                    {
                        isValid = false;
                        HighlightErrorInColumn(col);
                    }
                }

                // Проверка блоков 3x3
                for (int blockRow = 0; blockRow < SubgridSize; blockRow++)
                {
                    for (int blockCol = 0; blockCol < SubgridSize; blockCol++)
                    {
                        var blockValues = Enumerable.Range(0, SudokuSize)
                            .Select(i => GetCellValue(blockRow * SubgridSize + i / SubgridSize, blockCol * SubgridSize + i % SubgridSize))
                            .Where(value => value != 0);

                        if (blockValues.Count() != blockValues.Distinct().Count())
                        {
                            isValid = false;
                            HighlightErrorInBlock(blockRow, blockCol);
                        }
                    }
                }

                if (isValid && IsSudokuComplete())
                {
                    MessageBox.Show("Судоку решено верно!", "Поздравляем!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    for (int row = 0; row < SudokuSize; row++)
                        for (int col = 0; col < SudokuSize; col++)
                            SetCellColor(row, col, Color.Green);
                }
                else if (isValid)
                {
                    RestoreDefaultColors();
                }

            }
            finally
            {
                _validating = false;
            }
        }

        private int GetCellValue(int row, int col)
        {
            if (string.IsNullOrEmpty(_textBoxes[row, col].Text))
                return 0;
            else
            {
                if (int.TryParse(_textBoxes[row, col].Text, out int value))
                    return value;
                else
                    return 0;
            }
        }

        private void SetCellColor(int row, int col, Color color)
        {
            _textBoxes[row, col].BackColor = color;
        }

        private bool IsSudokuComplete()
        {
            for (int row = 0; row < SudokuSize; row++)
                for (int col = 0; col < SudokuSize; col++)
                    if (string.IsNullOrEmpty(_textBoxes[row, col].Text))
                    {
                        return false;
                    }
            return true;
        }

        private void saveButton_Click_1(object sender, EventArgs e)
        {
            SaveData();
        }

        private void ClearAllErrors()
        {
            for (int row = 0; row < SudokuSize; row++)
            {
                for (int col = 0; col < SudokuSize; col++)
                {
                    if (_textBoxes[row, col].BackColor == Color.Red)
                    {
                        if ((row / 3 + col / 3) % 2 == 0)
                            _textBoxes[row, col].BackColor = Color.White;
                        else
                            _textBoxes[row, col].BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void HighlightErrorInRow(int row)
        {
            for (int col = 0; col < SudokuSize; col++)
            {
                if (IsConflictInRow(row, col))
                {
                    _textBoxes[row, col].BackColor = Color.Red;
                }
            }
        }

        private void HighlightErrorInColumn(int col)
        {
            for (int row = 0; row < SudokuSize; row++)
            {
                if (IsConflictInColumn(row, col))
                {
                    _textBoxes[row, col].BackColor = Color.Red;
                }
            }
        }

        private void HighlightErrorInBlock(int blockRow, int blockCol)
        {
            for (int i = 0; i < SudokuSize; i++)
            {
                int row = blockRow * SubgridSize + i / SubgridSize;
                int col = blockCol * SubgridSize + i % SubgridSize;
                if (IsConflictInBlock(blockRow, blockCol, row, col))
                    _textBoxes[row, col].BackColor = Color.Red;
            }
        }

        private bool IsConflictInRow(int row, int col)
        {
            int value = GetCellValue(row, col);
            if (value == 0) return false;

            for (int c = 0; c < SudokuSize; c++)
                if (c != col && GetCellValue(row, c) == value)
                    return true;

            return false;
        }

        private bool IsConflictInColumn(int row, int col)
        {
            int value = GetCellValue(row, col);
            if (value == 0) return false;

            for (int r = 0; r < SudokuSize; r++)
                if (r != row && GetCellValue(r, col) == value)
                    return true;

            return false;
        }

        private bool IsConflictInBlock(int blockRow, int blockCol, int row, int col)
        {
            int value = GetCellValue(row, col);
            if (value == 0) return false;

            for (int i = 0; i < SudokuSize; i++)
            {
                int r = blockRow * SubgridSize + i / SubgridSize;
                int c = blockCol * SubgridSize + i % SubgridSize;
                if ((r != row || c != col) && GetCellValue(r, c) == value)
                    return true;
            }

            return false;
        }
        private void RestoreDefaultColors()
        {
            for (int row = 0; row < SudokuSize; row++)
                for (int col = 0; col < SudokuSize; col++)
                    if ((row / 3 + col / 3) % 2 == 0)
                        _textBoxes[row, col].BackColor = Color.White;
                    else
                        _textBoxes[row, col].BackColor = Color.LightGray;
        }
    }

    public class SudokuFacade
    {
        private TableLayoutPanel _tableLayoutPanel;
        private TextBox[,] _textBoxes;
        private int[,] _initialSudokuGrid;

        public SudokuFacade(TableLayoutPanel tableLayoutPanel, TextBox[,] textBoxes, int[,] initialSudokuGrid)
        {
            _tableLayoutPanel = tableLayoutPanel;
            _textBoxes = textBoxes;
            _initialSudokuGrid = initialSudokuGrid;
        }

        public void InitializeGrid()
        {
            _tableLayoutPanel.SuspendLayout();

            for (int row = 0; row < 9; row++)
                for (int col = 0; col < 9; col++)
                {
                    _textBoxes[row, col] = new TextBox();
                    _textBoxes[row, col].TextAlign = HorizontalAlignment.Center;
                    _textBoxes[row, col].Dock = DockStyle.Fill;
                    _textBoxes[row, col].Multiline = true;
                    _textBoxes[row, col].MaxLength = 1;
                    _textBoxes[row, col].Font = new Font("Arial", 18, FontStyle.Bold);
                    _textBoxes[row, col].Margin = new Padding(1);

                    if ((row / 3 + col / 3) % 2 == 0)
                        _textBoxes[row, col].BackColor = Color.White;
                    else
                        _textBoxes[row, col].BackColor = Color.LightGray;

                    _textBoxes[row, col].TextChanged += TextBox_TextChanged;
                    _textBoxes[row, col].KeyPress += TextBox_KeyPress;
                    _textBoxes[row, col].Tag = new Tuple<int, int>(row, col);  // Store row and col in Tag

                    _tableLayoutPanel.Controls.Add(_textBoxes[row, col], col, row);
                }

            _tableLayoutPanel.ResumeLayout(true);
        }

        public void DisplaySudoku(int[,] initialSudokuGrid)
        {
            _initialSudokuGrid = initialSudokuGrid;
            for (int row = 0; row < 9; row++)
                for (int col = 0; col < 9; col++)
                    if (_initialSudokuGrid[row, col] != 0)
                    {
                        _textBoxes[row, col].Text = _initialSudokuGrid[row, col].ToString();
                        _textBoxes[row, col].ReadOnly = true;
                        _textBoxes[row, col].ForeColor = Color.Black;
                    }
                    else
                    {
                        _textBoxes[row, col].Text = "";
                        _textBoxes[row, col].ReadOnly = false;
                        _textBoxes[row, col].ForeColor = Color.Blue;
                    }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // 8 - это Backspace
                e.Handled = true;
            if (e.KeyChar == '0')
                e.Handled = true;   
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length > 1)
            {
                textBox.Text = textBox.Text.Substring(0, 1);
                textBox.SelectionStart = 1;
                textBox.SelectionLength = 0;
            }

            if (textBox.Parent is TableLayoutPanel tableLayoutPanel && tableLayoutPanel.Parent is Task6 form)
                form.ValidateSudoku();
        }
    }
}