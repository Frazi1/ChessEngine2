using chessengine.pieces;

namespace chessengine.board.tiles {
    public class EmptyTile : Tile {
        public EmptyTile(int coordinate) : base(coordinate) {
        }

        public override bool IsTileOccupied {
            get { return false; }
        }

        public override Piece Piece {
            get { return null; }
        }

        public override string ToString() {
            return "-";
        }
    }
}