using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shapkin_Task_8
{
    public class GridRenderer
    {
        private Panel _gridPanel;

        public GridRenderer(Panel gridPanel)
        {
            _gridPanel = gridPanel;
        }

        public void UpdateGridVisual(CellState[,] grid, int gridSize, Color healthyColor, Color infectedColor, Color immuneColor)
        {
            if (_gridPanel.InvokeRequired)
            {
                _gridPanel.Invoke(new Action(() => UpdateGridVisual(grid, gridSize, healthyColor, infectedColor, immuneColor)));
                return;
            }

            _gridPanel.Controls.Clear();
            int cellSize = _gridPanel.Width / gridSize;

            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                {
                    Panel cellPanel = new Panel
                    {
                        Location = new Point(j * cellSize, i * cellSize),
                        Size = new Size(cellSize, cellSize),
                        BackColor = GetCellColor(grid[i, j].State, healthyColor, infectedColor, immuneColor)
                    };
                    _gridPanel.Controls.Add(cellPanel);
                }
        }

        private Color GetCellColor(State state, Color healthyColor, Color infectedColor, Color immuneColor)
        {
            switch (state)
            {
                case State.Infected:
                    return infectedColor;
                case State.Immune:
                    return immuneColor;
                default:
                    return healthyColor;
            }
        }
    }
}
