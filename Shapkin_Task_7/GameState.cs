namespace Shapkin_Task_7
{
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
}
