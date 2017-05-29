using chessengine.pieces;

namespace chessengine.board.tiles {
    public class OccupiedTile : Tile {
        public override Piece Piece { get; }

        public OccupiedTile(int coordinate, Piece piece) : base(coordinate) {
            Piece = piece;
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