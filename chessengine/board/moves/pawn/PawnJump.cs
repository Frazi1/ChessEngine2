using chessengine.pieces;

namespace chessengine.board.moves.pawn {
    public class PawnJump : Move {
        public PawnJump(Board board, Piece movedPiece, int destinationCoordinate)
            : base(board, movedPiece, destinationCoordinate) {
        }

        public override Board Execute() {
            Builder builder = new Builder();
            foreach (Piece piece in Board.CurrentPlayer.ActivePieces) {
                if (!MovedPiece.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }
            foreach (Piece piece in Board.CurrentPlayer.Opponent.ActivePieces) {
                builder.SetPiece(piece);
            }
            Pawn movedPawn = (Pawn)MovedPiece.MovePiece(this);
            builder.SetPiece(movedPawn);
            builder.SetEnPassantPawn(movedPawn);
            builder.SetMoveMaker(Board.CurrentPlayer.Opponent.PlayerAlliance);
            return builder.Build();
        }
    }
}