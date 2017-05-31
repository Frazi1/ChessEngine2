using chessengine.pieces;

namespace chessengine.board.moves {
    public class AttackMove : Move {
        public Piece AttackedPiece { get; }

        public AttackMove(Board board,
            Piece movedPiece,
            int destinationCoordinate,
            Piece pieceAtDestination)
            : base(board, movedPiece, destinationCoordinate) {
            AttackedPiece = pieceAtDestination;
        }

        public override Piece GetAttackedPiece() {
            return AttackedPiece;
        }

        public override Board Execute() {
            Builder builder = new Builder();
            foreach (Piece piece in this.Board.CurrentPlayer.ActivePieces) {
                if (!MovedPiece.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }

            foreach (Piece piece in this.Board.CurrentPlayer.Opponent.ActivePieces) {
                if (!piece.Equals(GetAttackedPiece())) {
                    builder.SetPiece(piece);
                }
            }
            builder.SetPiece(MovedPiece.MovePiece(this));
            builder.SetMoveMaker(Board.CurrentPlayer.Opponent.PlayerAlliance);
            return builder.Build();
        }

        #region Equality
        protected bool Equals(AttackMove other) {
            return base.Equals(other) && Equals(AttackedPiece, other.AttackedPiece);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AttackMove)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (base.GetHashCode() * 397) ^ (AttackedPiece != null ? AttackedPiece.GetHashCode() : 0);
            }
        }
        #endregion
    }
}