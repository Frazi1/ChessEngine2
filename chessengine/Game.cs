using System;
using System.Collections.Generic;
using chessengine.board;
using chessengine.board.moves;

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

        public void ExecuteMove(Move move) {
            Board board = move.Execute();
            Boards.Add(board);
            CurrentBoard = board;
        }

    }
}