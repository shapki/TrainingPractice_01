using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Shapkin_Task_8
{
    public partial class Task8 : Form
    {
        private int _gridSize;
        private int _timeInterval;
        private double _reinfectionProbability;
        private CellState[,] _grid;
        private const int ImmuneDuration = 4;
        private const int InfectionDuration = 6;
        private const double InfectionProbability = 0.5;
        public Color HealthyColor = Color.White;
        public Color InfectedColor = Color.Red;
        public Color ImmuneColor = Color.Green;

        private SimulationFacade _simulationFacade;
        private GridRenderer _gridRenderer;

        public Task8()
        {
            InitializeComponent();
            timer1.Tick += TimerTick;
            _simulationFacade = new SimulationFacade();
            _gridRenderer = new GridRenderer(gridPanel);

            HelpButtonClicked += ShapedForm1_HelpButtonClicked;
        }

        private void ShapedForm1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Симуляция инфекций.\n\nБелые клетки - здоровые\nКрасные клетки - инфицированные\nЗеленые клетки - с иммунитиетом", "Инфекция гйда - Информация", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                e.Cancel = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                _gridSize = int.Parse(gridSizeTextBox.Text);
                if (_gridSize % 2 == 0)
                {
                    MessageBox.Show("Размер сетки должен быть нечетным!");
                    return;
                }

                _timeInterval = int.Parse(timeIntervalTextBox.Text);
                _reinfectionProbability = double.Parse(reinfectionProbabilityTextBox.Text, CultureInfo.InvariantCulture);

                _simulationFacade.InitializeGrid(_gridSize);
                _grid = _simulationFacade.GetGrid();

                StopInfection(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода: {ex.Message}");
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _simulationFacade.SimulateInfection(_reinfectionProbability, InfectionDuration, ImmuneDuration, InfectionProbability);
            _grid = _simulationFacade.GetGrid();
            UpdateGridVisual();
        }

        private void UpdateGridVisual()
        {
            _gridRenderer.UpdateGridVisual(_grid, _gridSize, HealthyColor, InfectedColor, ImmuneColor);
        }

        private void TextBoxKeyPress_DigitsOnly(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TextBoxKeyPress_BinaryOnly(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void gridSizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxKeyPress_DigitsOnly(e);
        }

        private void timeIntervalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxKeyPress_DigitsOnly(e);
        }

        private void reinfectionProbabilityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxKeyPress_BinaryOnly(e);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopInfection(true);
        }

        private void StopInfection(bool val)
        {
            gridSizeTextBox.Enabled = val;
            timeIntervalTextBox.Enabled = val;
            reinfectionProbabilityTextBox.Enabled = val;
            startButton.Enabled = val;
            stopButton.Enabled = !val;

            if (val)
                timer1.Stop();
            else
            {
                timer1.Interval = _timeInterval;
                timer1.Start();
            }
        }
    }

    public enum State
    {
        Healthy,
        Infected,
        Immune
    }
}