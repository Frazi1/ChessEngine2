using chessengine.pieces;

namespace chessengine.board.moves.pawn {
    public class PawnEnPassantAttackMove : PawnAttackMove {
        public PawnEnPassantAttackMove(Board board, Piece movedPiece, int destinationCoordinate,
            Piece pieceAtDestination)
            : base(board, movedPiece, destinationCoordinate, pieceAtDestination) {
        }

        public override Board Execute() {
            Builder builder = new Builder();
            foreach (Piece piece in Board.CurrentPlayer.ActivePieces) {
                if (!MovedPiece.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }
            foreach (Piece piece in Board.CurrentPlayer.Opponent.ActivePieces) {
                if (!AttackedPiece.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }
            builder.SetPiece(MovedPiece.MovePiece(this));
            builder.SetMoveMaker(Board.CurrentPlayer.Opponent.PlayerAlliance);
            return builder.Build();
        }
    }
}