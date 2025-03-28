using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shapkin_Task_7
{
    public class GameBoardFacade
    {
        private readonly GameBoard _gameBoard;
        private readonly GameState _gameState;
        private readonly FoxAi _foxAi;
        private readonly Random _random = new Random();

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
            {
                return false;
            }
        }

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
}