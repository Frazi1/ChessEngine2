using System;
using System.Collections.Generic;
using System.Linq;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.player;

namespace chessengine {
    public class Game {
        public event Action BoardChanged;
        private Board _currentBoard;

        public Game() {
            CurrentBoard = Board.CreateStandardBoard();
            Boards = new List<Board>() {
                CurrentBoard
            };
        }

        public Board CurrentBoard {
            get { return _currentBoard; }
            private set {
                _currentBoard = value;
                OnBoardChanged();
            }
        }

        private void OnBoardChanged() {
            if (BoardChanged != null)
                BoardChanged();
        }

        private List<Board> Boards { get; set; }
        public int NumTilesPerRow { get { return BoardUtils.NumTilesPerRow; } }
        public int NumTiles { get { return BoardUtils.NumTiles; } }

        public MoveStatus DoMove(Move move) {
            MoveTransition moveTransition = CurrentBoard.CurrentPlayer.MakeMove(move);
            CurrentBoard = moveTransition.TransitionBoard;
            return moveTransition.MoveStatus;
        }
    }
}