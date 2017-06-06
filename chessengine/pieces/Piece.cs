using System.Collections.Generic;
using System.Text;
using chessengine.board;
using chessengine.board.moves;
using chessengine.Extensions.EnumExtensions;

// ReSharper disable once CheckNamespace
namespace chessengine.pieces {
    public abstract class Piece {
        public int PiecePosition { get; private set; }
        public bool IsFirstMove { get; private set; }
        public Alliance.AllianceEnum PieceAlliance { get; private set; }
        public PieceType PieceType { get; private set; }

        protected Piece(int piecePosition, bool isFirstMove, Alliance.AllianceEnum pieceAlliance, PieceType pieceType) {
            PiecePosition = piecePosition;
            PieceAlliance = pieceAlliance;
            IsFirstMove = isFirstMove;
            PieceType = pieceType;
        }

        public abstract ICollection<Move> CalculateLegalMoves(Board board);
        public abstract Piece MovePiece(Move move);

        #region Equality

        protected bool Equals(Piece other) {
            return PiecePosition == other.PiecePosition 
                && IsFirstMove == other.IsFirstMove 
                && PieceAlliance == other.PieceAlliance 
                && PieceType == other.PieceType;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Piece) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = PiecePosition;
                hashCode = (hashCode * 397) ^ IsFirstMove.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) PieceAlliance;
                hashCode = (hashCode * 397) ^ (int) PieceType;
                return hashCode;
            }
        }

        #endregion

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            char c = PieceType.ToText()[0];
            builder.Append(c);
            return PieceAlliance == Alliance.AllianceEnum.Black
                ? builder.ToString().ToLower()
                : builder.ToString();
        }
    }

}