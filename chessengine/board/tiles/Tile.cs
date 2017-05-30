using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.pieces;

namespace chessengine.board.tiles {
    public abstract class Tile {
        public static readonly IDictionary<int, EmptyTile> EmptyTilesCache = CreateAllPossibleEmptyTiles();

        protected Tile(int coordinate) {
            Coordinate = coordinate;
        }

        public int Coordinate { get; }

        public abstract bool IsTileOccupied { get; }

        public abstract Piece Piece { get; }

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
