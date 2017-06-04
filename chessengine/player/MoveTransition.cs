using System;
using chessengine.board;
using chessengine.board.moves;

namespace chessengine.player {
    public class MoveTransition {
        public Board TransitionBoard { get; private set; }
        public Move Move { get; private set; }
        public MoveStatus MoveStatus { get; private set; }

        public MoveTransition(Board transitionBoard, Move move, MoveStatus moveStatus) {
            TransitionBoard = transitionBoard;
            Move = move;
            MoveStatus = moveStatus;
        }

        public override string ToString() {
            return string.Concat(Move, Environment.NewLine, MoveStatus);
        }
    }
}