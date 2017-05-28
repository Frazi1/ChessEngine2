using chessengine.board;
using chessengine.board.moves;

namespace chessengine.player {
    public class MoveTransition {
        public Board TransitionBoard { get; }
        public Move Move { get; }
        public MoveStatus MoveStatus { get; }

        public MoveTransition(Board transitionBoard, Move move, MoveStatus moveStatus) {
            TransitionBoard = transitionBoard;
            Move = move;
            MoveStatus = moveStatus;
        }
    }

    public enum MoveStatus {
        Done,
        Illegal,
        LeavesPlayerInCheck
    }
}