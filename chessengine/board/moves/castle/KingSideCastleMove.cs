﻿using chessengine.pieces;

namespace chessengine.board.moves.castle {
    public class KingSideCastleMove : CastleMove {
        public KingSideCastleMove(Board board,
            Piece movedPiece,
            int destinationCoordinate,
            Rook castleRook,
            int castleRookStart,
            int castleRookDestination)
            : base(board, movedPiece, destinationCoordinate, castleRook, castleRookStart, castleRookDestination) {
        }

        public override string ToString() {
            return "0-0";
        }
    }
}