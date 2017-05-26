using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.pieces;

namespace chessengine.board {
    public abstract class Tile {
        private readonly int _coordinate;
        private static readonly IDictionary<int, EmptyTile> EmptyTilesCache = CreateAllPossibleEmptyTiles();

        protected Tile(int coordinate) {
            _coordinate = coordinate;
        }

        protected int Coordinate {
            get { return _coordinate; }
        }

        public abstract bool IsTileOccupied();

        public abstract Piece GetPiece();

        public static IDictionary<int, EmptyTile> CreateAllPossibleEmptyTiles() {
            Dictionary<int, EmptyTile> emptyTilesDictionary = new Dictionary<int, EmptyTile>();
            for (int i = 0; i < BoardUtils.NumTiles; i++) {
                emptyTilesDictionary.Add(i, new EmptyTile(i));
            }
            return ImmutableDictionary.CreateRange(emptyTilesDictionary);
        }

        public static Tile CreateTile(int coordinate, Piece piece) {
            return piece != null ? (Tile)new OccupiedTile(coordinate, piece) : EmptyTilesCache[coordinate];
        }
    }

    public class EmptyTile : Tile {
        public EmptyTile(int coordinate) : base(coordinate) {
        }

        public override bool IsTileOccupied() {
            return false;
        }

        public override Piece GetPiece() {
            return null;
        }
    }

    public class OccupiedTile : Tile {
        private readonly Piece _pieceOnTile;

        public Piece PieceOnTile {
            get { return _pieceOnTile; }
        }

        public OccupiedTile(int coorditane, Piece piece) : base(coorditane) {
            _pieceOnTile = piece;
        }

        public override bool IsTileOccupied() {
            return true;
        }

        public override Piece GetPiece() {
            return PieceOnTile;
        }
    }
}
