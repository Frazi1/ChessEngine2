using chessengine.pieces;

namespace chessengine.board.tiles {
    public class OccupiedTile : Tile {
        private Piece _piece;
        public override Piece Piece { get { return _piece; } }

        public OccupiedTile(int coordinate, Piece piece) : base(coordinate) {
            _piece = piece;
        }

        public override bool IsTileOccupied {
            get { return true; }
        }

        public override string ToString() {
            //return Alliance.IsBlack(Piece.PieceAlliance)
            //    ? Piece.ToString().ToLower()
            //    : Piece.ToString();
            return Piece.ToString();
        }
    }
}