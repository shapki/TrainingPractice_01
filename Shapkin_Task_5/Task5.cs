using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Shapkin_Task_5
{
    public partial class Task5 : Form
    {
        private const int SudokuSize = 9;
        private const int SubgridSize = 3;
        private TextBox[,] _textBoxes = new TextBox[SudokuSize, SudokuSize];
        private int[,] _initialSudokuGrid = new int[SudokuSize, SudokuSize];
        private SudokuFacade _sudokuFacade;
        private string _baseDirectory;

        public Task5()
        {
            InitializeComponent();
            _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _baseDirectory = Path.Combine(_baseDirectory, "..", "..");
            _baseDirectory = Path.GetFullPath(_baseDirectory);

            _sudokuFacade = new SudokuFacade(tableLayoutPanel1, _textBoxes, _initialSudokuGrid);
            InitializeSudokuGrid();
            LoadInitialData("easy.txt");
            DisplaySudoku();

            difficultyComboBox.Items.AddRange(new string[] { "Легкий", "Средний", "Сложный" });
            difficultyComboBox.SelectedIndex = 0;
        }

        private void InitializeSudokuGrid()
        {
            _sudokuFacade.InitializeGrid();
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
                    MessageBox.Show("Неверный формат файла судоку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int row = 0; row < SudokuSize; row++)
                {
                    string[] values = lines[row].Split(' ');

                    if (values.Length != SudokuSize)
                    {
                        MessageBox.Show("Неверный формат файла судоку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int col = 0; col < SudokuSize; col++)
                        if (int.TryParse(values[col], out int value))
                        {
                            _initialSudokuGrid[row, col] = value;
                        }
                        else
                        {
                            MessageBox.Show("Неверное число в файле судоку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
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
            _sudokuFacade.DisplaySudoku();
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

                    _tableLayoutPanel.Controls.Add(_textBoxes[row, col], col, row);
                }

            _tableLayoutPanel.ResumeLayout(true);
        }

        public void DisplaySudoku()
        {
            for (int row = 0; row < 9; row++)
                for (int col = 0; col < 9; col++)
                {
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
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // 8 - это Backspace
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
        }
    }
}