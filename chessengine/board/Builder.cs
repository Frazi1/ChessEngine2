using System.Collections.Generic;
using chessengine.pieces;

namespace chessengine.board {
    public class Builder {
        public Dictionary<int, Piece> BoardConfig { get; }
        private Alliance.AllianceEnum _nextMoveMaker;

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
            _nextMoveMaker = nextMoveMaker;
            return this;
        }
    }
}