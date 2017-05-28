using chessengine.Extensions.EnumExtensions;

namespace chessengine.pieces {
    public enum PieceType {
        [Text("P")] Pawn,
        [Text("R")] Rook,
        [Text("N")] Knigth,
        [Text("B")] Bishop,
        [Text("Q")] Queen,
        [Text("K")] King
    }
}