using chessengine.Extensions.EnumExtensions;

namespace chessengine.pieces {
    public enum PieceType {
        [Text("P")] [Value(100)] Pawn,
        [Text("N")] [Value(320)] Knight,
        [Text("B")] [Value(325)] Bishop,
        [Text("R")] [Value(500)] Rook,
        [Text("Q")] [Value(975)] Queen,
        [Text("K")] [Value(30000)] King
    }
}