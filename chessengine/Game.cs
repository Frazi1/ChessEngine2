using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.player;
using chessengine.player.AI;
using chessengine.player.AI.Minimax;

namespace chessengine {
    public class Game {
        private Board _currentBoard;
        private IStrategy _strategy = new Minimax(2);

        public List<Board> Boards { get; private set; }
        public Board CurrentBoard {
            get { return _currentBoard; }
            private set {
                if (ReferenceEquals(_currentBoard, value)) return;
                _currentBoard = value;
                OnBoardChanged();
            }
        }

        public event Action BoardChanged;

        public static int NumTilesPerRow { get { return BoardUtils.NumTilesPerRow; } }
        public static int NumTiles { get { return BoardUtils.NumTiles; } }

        public Game() {
            CurrentBoard = Board.CreateStandardBoard();
            Boards = new List<Board>() {
                CurrentBoard
            };
        }

        private void OnBoardChanged() {
            if (BoardChanged != null)
                BoardChanged();
        }

        public MoveStatus DoMove(int currentCoordinate, int destinationCoordinate) {
            Move move = MoveFactory.FindMove(CurrentBoard, currentCoordinate, destinationCoordinate);
            MoveTransition moveTransition = CurrentBoard.CurrentPlayer.MakeMove(move);
            CurrentBoard = moveTransition.TransitionBoard;
            return moveTransition.MoveStatus;
        }

        public MoveTransition DoStrategyMove() {
            Move move = _strategy.SelectMoveParallel(CurrentBoard, CurrentBoard.CurrentPlayer);
            MoveTransition moveTransition = CurrentBoard.CurrentPlayer.MakeMove(move);
            CurrentBoard = moveTransition.TransitionBoard;
            return moveTransition;
        }
    }
}