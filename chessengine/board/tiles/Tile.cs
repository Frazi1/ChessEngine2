using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.pieces;

namespace chessengine.board.tiles {
    public abstract class Tile {
        private bool _isTileOccupied = false;
        private Piece _piece;

        public int Coordinate { get; private set; }
        public virtual bool IsTileOccupied { get { return _isTileOccupied; } }
        public virtual Piece Piece { get { return _piece; } }

        public static readonly IDictionary<int, EmptyTile> EmptyTilesCache = CreateAllPossibleEmptyTiles();

        protected Tile(int coordinate) {
            Coordinate = coordinate;
        }

        private static IDictionary<int, EmptyTile> CreateAllPossibleEmptyTiles() {
            Dictionary<int, EmptyTile> emptyTilesDictionary = new Dictionary<int, EmptyTile>();
            for (int i = 0; i < BoardUtils.NumTiles; i++) {
                emptyTilesDictionary.Add(i, new EmptyTile(i));
            }
            return ImmutableDictionary.CreateRange(emptyTilesDictionary);
        }

        public static Tile CreateTile(int coordinate, Piece piece) {
            return piece != null
                ? (Tile)new OccupiedTile(coordinate, piece)
                : EmptyTilesCache[coordinate];
        }
    }
}
