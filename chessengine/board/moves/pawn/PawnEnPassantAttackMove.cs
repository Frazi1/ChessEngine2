using chessengine.pieces;

namespace chessengine.board.moves.pawn {
    public class PawnEnPassantAttackMove :PawnAttackMove {
        public PawnEnPassantAttackMove(Board board, Piece movedPiece, int destinationCoordinate, Piece pieceAtDestination)
            : base(board, movedPiece, destinationCoordinate, pieceAtDestination) {
        }
    }
}