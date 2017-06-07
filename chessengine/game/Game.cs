using System.Collections.Generic;
using chessengine.board;
using chessengine.board.moves;
using chessengine.game.events;
using chessengine.player;
using chessengine.player.AI;
using chessengine.player.AI.Minimax;
using chessengine.Extensions.logger;
using chessengine.Extensions.logger.progressLogger;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace chessengine.game {
    public class Game {
        private Board _currentBoard;
        private readonly IStrategy _strategy;

        public List<Board> Boards { get; private set; }

        public Board CurrentBoard {
            get { return _currentBoard; }
            private set {
                if (ReferenceEquals(_currentBoard, value)) return;
                _currentBoard = value;
                OnBoardChanged();
            }
        }

        public event BoardChangedEventHadler BoardChanged;

        public static int NumTilesPerRow {
            get { return BoardUtils.NumTilesPerRow; }
        }

        public static int NumTiles {
            get { return BoardUtils.NumTiles; }
        }

        public Game(int depth) {
            CurrentBoard = Board.CreateStandardBoard();
            Boards = new List<Board>() {
                CurrentBoard
            };
            _strategy = new Minimax(depth);
        }

        public Game(int depth, IProgressLogger logger) {
            CurrentBoard = Board.CreateStandardBoard();
            Boards = new List<Board>() {
                CurrentBoard
            };
            _strategy = new Minimax(depth, logger);
        }

        private void OnBoardChanged() {
            if (BoardChanged != null) {
                BoardChanged(this, new BoardChangedArgs(CurrentBoard.CurrentPlayer.IsInCheckMate()));
            }
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

    public delegate void BoardChangedEventHadler(object sender, BoardChangedArgs args);
}