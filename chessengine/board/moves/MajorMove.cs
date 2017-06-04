using System;
using chessengine.pieces;

namespace chessengine.board.moves {
    public class MajorMove : Move {
        public MajorMove(Board board, Piece movedPiece, int destinationCoordinate)
            : base(board, movedPiece, destinationCoordinate) {
        }

        //public override string ToString() {
        //    return string.Concat(MovedPiece.ToString(), CurrentCoordinate," - ", DestinationCoordinate);
        //}
    }
}