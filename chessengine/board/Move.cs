using chessengine.pieces;

namespace chessengine.board {
    public abstract class Move {
        public Board Board { get; }
        public Piece MovedPiece { get; }
        public int DestinationCoordinate { get; }

        public Move(Board board, Piece movedPiece, int destinationCoordinate) {
            Board = board;
            MovedPiece = movedPiece;
            this.DestinationCoordinate = destinationCoordinate;
        }

    }

    public class AttackMove : Move {
        public Piece AttackedPiece { get; }

        public AttackMove(Board board, Piece movedPiece, int destinationCoordinate, Piece pieceAtDestination)
            : base(board, movedPiece, destinationCoordinate) {
            AttackedPiece = pieceAtDestination;
        }
    }

    public class MajorMove : Move {
        public MajorMove(Board board, Piece movedPiece, int destinationCoordinate)
            : base(board, movedPiece, destinationCoordinate) {
        }
    }
}