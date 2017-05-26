using System.Collections.Generic;
using chessengine.board;

// ReSharper disable once CheckNamespace
namespace chessengine.pieces {
    public abstract class Piece {
        protected int PiecePosition { get; set; }
        public Alliance PieceAlliance { get; }

        public Piece(int piecePosition, Alliance pieceAlliance) {
            PiecePosition = piecePosition;
            PieceAlliance = pieceAlliance;
        }
        public abstract ICollection<Move> CalculateLegalMoves(Board board);
    }
}