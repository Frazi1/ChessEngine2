using chessengine.pieces;

namespace chessengine.board.moves {
    public abstract class Move {
        protected Board Board { get; }
        public Piece MovedPiece { get; }
        public int DestinationCoordinate { get; }
        public int CurrentCoordinate { get { return MovedPiece.PiecePosition; }}

        public static readonly Move NullMove = new NullMove();

        protected Move(Board board, Piece movedPiece, int destinationCoordinate) {
            Board = board;
            MovedPiece = movedPiece;
            DestinationCoordinate = destinationCoordinate;
        }

        public virtual bool IsAttackMove() {
            return false;
        }

        public virtual bool IsCastlingMove() {
            return false;
        }

        public virtual Piece GetAttackedPiece() {
            return null;
        }

        public virtual Board Execute() {
            Builder builder = new Builder();

            foreach (Piece piece in Board.CurrentPlayer.ActivePieces) {
                //hashcode and equals
                if (!MovedPiece.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }

            foreach (Piece piece in Board.CurrentPlayer.Opponent.ActivePieces) {
                builder.SetPiece(piece);
            }

            //Fix
            builder.SetPiece(MovedPiece.MovePiece(this));
            builder.SetMoveMaker(Board.CurrentPlayer.Opponent.PlayerAlliance);
            return builder.Build();
        }

        #region Equality
        protected bool Equals(Move other) {
            return Equals(MovedPiece, other.MovedPiece) && DestinationCoordinate == other.DestinationCoordinate;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Move)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((MovedPiece != null ? MovedPiece.GetHashCode() : 0) * 397) ^ DestinationCoordinate;
            }
        } 
        #endregion
    }
}