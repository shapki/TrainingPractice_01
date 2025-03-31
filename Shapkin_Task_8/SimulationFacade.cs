namespace Shapkin_Task_8
{
    public class SimulationFacade
    {
        private SimulationManager _simulationManager;

        public SimulationFacade()
        {
            _simulationManager = new SimulationManager();
        }

        public void InitializeGrid(int gridSize)
        {
            _simulationManager.InitializeGrid(gridSize);
        }

        public CellState[,] GetGrid()
        {
            return _simulationManager.GetGrid();
        }

        public void SimulateInfection(double reinfectionProbability, int infectionDuration, int immuneDuration, double infectionProbability)
        {
            _simulationManager.SimulateInfection(reinfectionProbability, infectionDuration, immuneDuration, infectionProbability);
        }
    }
}
