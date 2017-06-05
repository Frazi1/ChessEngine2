using System;
using chessengine.pieces;

namespace chessengine.board.moves {
    public class AttackMove : Move {
        protected Piece AttackedPiece { get; private set; }

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

        public override bool IsAttackMove() {
            return true;
        }

        public override Board Execute() {
            if (!CanExecute()) throw new Exception();
            Builder builder = new Builder();
            foreach (Piece piece in Board.CurrentPlayer.ActivePieces) {
                if (!MovedPiece.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }

            //TODO:Fix bug ??
            foreach (Piece piece in Board.CurrentPlayer.Opponent.ActivePieces) {
                if (!piece.Equals(AttackedPiece)) {
                    builder.SetPiece(piece);
                }
            }

            Piece movePiece = MovedPiece.MovePiece(this);
            builder.SetPiece(movePiece);
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