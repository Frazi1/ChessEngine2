using chessengine.pieces;

namespace chessengine.board.moves.pawn {
    public class PawnMove : Move {
        public PawnMove(Board board, Piece movedPiece, int destinationCoordinate)
            : base(board, movedPiece, destinationCoordinate) {
        }
    }
}