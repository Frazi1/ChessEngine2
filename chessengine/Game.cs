using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.player;

namespace chessengine {
    public class Game {
        private Board _currentBoard;

        public List<Board> Boards { get; private set; }
        public event Action BoardChanged;
        public Board CurrentBoard {
            get { return _currentBoard; }
            private set {
                if (ReferenceEquals(_currentBoard, value)) return;
                _currentBoard = value;
                OnBoardChanged();
            }
        }

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
            Move move = MoveFactory.CreateMove(CurrentBoard, currentCoordinate, destinationCoordinate);
            MoveTransition moveTransition = CurrentBoard.CurrentPlayer.MakeMove(move);
            CurrentBoard = moveTransition.TransitionBoard;
            return moveTransition.MoveStatus;
        }
    }
}