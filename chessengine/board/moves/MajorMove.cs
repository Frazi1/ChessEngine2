using System;
using chessengine.pieces;

namespace chessengine.board.moves {
    public class MajorMove : Move {
        public MajorMove(Board board, Piece movedPiece, int destinationCoordinate)
            : base(board, movedPiece, destinationCoordinate) {
        }

        public override string ToString() {
            return String.Concat(this.MovedPiece.ToString(), CurrentCoordinate, DestinationCoordinate);
        }
    }
}