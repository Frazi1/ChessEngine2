using chessengine.Extensions.EnumExtensions;

namespace chessengine.pieces {
    public abstract partial class Piece {
        protected enum PieceType {
            [Text("P")] Pawn,
            [Text("R")] Rook,
            [Text("N")] Knigth,
            [Text("B")] Bishop,
            [Text("Q")] Queen,
            [Text("K")] King
        }
    }
}