namespace Shapkin_Task_8
{
    public class SimulationManager
    {
        private CellState[,] _grid;
        private int _gridSize;

        public CellState[,] GetGrid()
        {
            return _grid;
        }

        public void InitializeGrid(int gridSize)
        {
            _gridSize = gridSize;
            _grid = new CellState[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    _grid[i, j] = new CellState();
                }
            }

            int center = gridSize / 2;
            _grid[center, center].State = State.Infected;
            _grid[center, center].InfectionTime = 0;
        }

        public void SimulateInfection(double reinfectionProbability, int infectionDuration, int immuneDuration, double infectionProbability)
        {
            CellState[,] nextGrid = new CellState[_gridSize, _gridSize];

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    nextGrid[i, j] = _grid[i, j].Clone();
                }
            }

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    ProcessCellState(i, j, nextGrid, reinfectionProbability, infectionDuration, immuneDuration, infectionProbability);
                }
            }

            _grid = nextGrid;
        }

        private void ProcessCellState(int row, int col, CellState[,] nextGrid, double reinfectionProbability, int infectionDuration, int immuneDuration, double infectionProbability)
        {
            if (nextGrid[row, col].State == State.Infected)
            {
                nextGrid[row, col].InfectionTime++;
                if (nextGrid[row, col].InfectionTime >= infectionDuration)
                {
                    nextGrid[row, col].State = State.Immune;
                    nextGrid[row, col].ImmuneTime = 0;
                }
            }
            else if (nextGrid[row, col].State == State.Immune)
            {
                nextGrid[row, col].ImmuneTime++;
                if (nextGrid[row, col].ImmuneTime >= immuneDuration)
                {
                    nextGrid[row, col].State = State.Healthy;
                }
            }

            if (_grid[row, col].State == State.Infected)
            {
                TryInfectNeighbors(row, col, nextGrid, reinfectionProbability, infectionProbability);
            }
        }

        private void TryInfectNeighbors(int row, int col, CellState[,] nextGrid, double reinfectionProbability, double infectionProbability)
        {
            TryInfectCell(row - 1, col, nextGrid, reinfectionProbability, infectionProbability);
            TryInfectCell(row + 1, col, nextGrid, reinfectionProbability, infectionProbability);
            TryInfectCell(row, col - 1, nextGrid, reinfectionProbability, infectionProbability);
            TryInfectCell(row, col + 1, nextGrid, reinfectionProbability, infectionProbability);
        }

        private void TryInfectCell(int row, int col, CellState[,] nextGrid, double reinfectionProbability, double infectionProbability)
        {
            if (row >= 0 && row < _gridSize && col >= 0 && col < _gridSize)
            {
                if (_grid[row, col].State == State.Healthy)
                {
                    if (RandomGenerator.GetNextDouble() < infectionProbability)
                    {
                        nextGrid[row, col].State = State.Infected;
                        nextGrid[row, col].InfectionTime = 0;
                    }
                }
                else if (_grid[row, col].State == State.Immune)
                {
                    if (RandomGenerator.GetNextDouble() < reinfectionProbability)
                    {
                        nextGrid[row, col].State = State.Infected;
                        nextGrid[row, col].InfectionTime = 0;
                    }
                }
            }
        }
    }
}
