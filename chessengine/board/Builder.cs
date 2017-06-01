using System.Collections.Generic;
using chessengine.pieces;

namespace chessengine.board {
    public class Builder {
        public Dictionary<int, Piece> BoardConfig { get; private set; }
        public Alliance.AllianceEnum NextMoveMaker { get; private set; }
        public Pawn EnPassantPawn { get; private set; }

        public Builder() {
            BoardConfig = new Dictionary<int, Piece>();
        }

        public Board Build() {
            return new Board(this);
        }

        public Builder SetPiece(Piece piece) {
            BoardConfig.Add(piece.PiecePosition, piece);
            return this;
        }

        public Builder SetMoveMaker(Alliance.AllianceEnum nextMoveMaker) {
            NextMoveMaker = nextMoveMaker;
            return this;
        }

        public Builder SetEnPassantPawn(Pawn movedPawn) {
            EnPassantPawn = movedPawn;
            return this;
        }
    }
}