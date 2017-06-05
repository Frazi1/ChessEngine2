using System;
using System.Collections.Generic;
using chessengine.pieces;

namespace chessengine.board {
    public class Builder {
        private bool _isMoveMakerSet = false;
        private Alliance.AllianceEnum _nextMoveMaker;
        
        public Dictionary<int, Piece> BoardConfig { get; private set; }

        public Alliance.AllianceEnum NextMoveMaker {
            get { return _nextMoveMaker; }
            private set {
                _nextMoveMaker = value;
                _isMoveMakerSet = true;
            }
        }

        public Pawn EnPassantPawn { get; private set; }

        public Builder() {
            BoardConfig = new Dictionary<int, Piece>();
        }

        public Board Build() {
            if(!_isMoveMakerSet) throw new Exception("Забыли устанивить игрока, который делает следующий ход");
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