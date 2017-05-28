using chessengine.pieces;

namespace chessengine.board.moves.castle {
    public abstract class CastleMove : Move {
        public Rook CastleRook { get; }
        public int CastleRookStart { get; }
        public int CastleRookDestination { get; }

        public CastleMove(Board board, Piece movedPiece, int destinationCoordinate, Rook castleRook,
            int castleRookStart, int castleRookDestination)
            : base(board, movedPiece, destinationCoordinate) {
            CastleRook = castleRook;
            CastleRookStart = castleRookStart;
            CastleRookDestination = castleRookDestination;
        }

        public override bool IsCastlingMove() {
            return true;
        }

        public override Board Execute() {
            Builder builder = new Builder();
            foreach (Piece piece in Board.CurrentPlayer.ActivePieces) {
                if (!MovedPiece.Equals(piece) && !CastleRook.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }
            foreach (Piece piece in Board.CurrentPlayer.Opponent.ActivePieces) {
                builder.SetPiece(piece);
            }
            builder.SetPiece(MovedPiece.MovePiece(this));
            builder.SetPiece(new Rook(CastleRookDestination, CastleRook.PieceAlliance));
            builder.SetMoveMaker(Board.CurrentPlayer.Opponent.PlayerAlliance);
            return builder.Build();
        }
    }
}