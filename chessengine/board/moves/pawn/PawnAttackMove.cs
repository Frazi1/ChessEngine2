using chessengine.pieces;

namespace chessengine.board.moves.pawn {
    public class PawnAttackMove : AttackMove {
        public PawnAttackMove(Board board,
            Piece movedPiece,
            int destinationCoordinate,
            Piece pieceAtDestination)
            : base(board, movedPiece, destinationCoordinate, pieceAtDestination) {
        }
    }
}