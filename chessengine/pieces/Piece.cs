using System;
using System.Collections.Generic;
using chessengine.board;
using chessengine.board.moves;

// ReSharper disable once CheckNamespace
namespace chessengine.pieces {
    public abstract class Piece {
        public int PiecePosition { get; }
        public bool IsFirstMove { get; }
        public Alliance.AllianceEnum PieceAlliance { get; }
        public PieceType PieceType { get; }

        protected Piece(int piecePosition, Alliance.AllianceEnum pieceAlliance, PieceType pieceType) {
            PiecePosition = piecePosition;
            PieceAlliance = pieceAlliance;
            //TODO: more work
            IsFirstMove = false;
            PieceType = pieceType;
        }

        public abstract ICollection<Move> CalculateLegalMoves(Board board);
        public abstract Piece MovePiece(Move move);

        #region Equality
        protected bool Equals(Piece other) {
            return PiecePosition == other.PiecePosition
                   && PieceAlliance == other.PieceAlliance
                   && PieceType == other.PieceType
                   && IsFirstMove == other.IsFirstMove;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Piece)obj);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = PiecePosition;
                hashCode = (hashCode * 397) ^ (int)PieceAlliance;
                hashCode = (hashCode * 397) ^ (int)PieceType;
                return hashCode;
            }
        } 
        #endregion
    }
}