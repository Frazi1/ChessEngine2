using System;
using chessengine.pieces;

namespace chessengine.board.moves {
    public class NullMove : Move {
        public NullMove()
            : base(null, null, -1) {
        }

        public override Board Execute() {
            throw new Exception("Cannot execute Null move");
        }
    }
}