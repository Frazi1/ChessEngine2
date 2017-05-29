using System;
using System.Text;
using System.Windows;
using chessengine;
using chessengine.board.tiles;
using chessengine.pieces;

namespace chessui.Controller {
    public class Helper {

        public int NumTiles { get; set; }
        public int TilesPerRow { get; set; }
        public double TileSize { get; set; }

        public static Helper Instance { get; private set; }

        public static Helper CreateInstance(int numTiles, int tilesPerRow, double tileSize) {
            Instance = new Helper(numTiles, tilesPerRow, tileSize);
            return Instance;
        }

        public Helper(int numTiles, int tilesPerRow, double tileSize) {
            NumTiles = numTiles;
            TilesPerRow = tilesPerRow;
            TileSize = tileSize;
        }

        public string SelectPieceImage(Piece tilePiece) {
            string prefix = "Resources/";
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder
                .Append(tilePiece.PieceType)
                .Append("_")
                .Append(tilePiece.PieceAlliance)
                .Append(".png");
            return String.Concat(prefix, pathBuilder.ToString().ToLower());
        }

        public Point ConvertTileCoordinateToScreenPoint(int coordinate) {
            double x = coordinate % TilesPerRow * TileSize;
            double y = coordinate / TilesPerRow * TileSize;
            return new Point(x, y);
        }

        public int ConvertToTileCoordinate(Point screenPoint) {
            int row = (int)(screenPoint.Y / TileSize);
            int column = (int)(screenPoint.X / TileSize);
            int coordinate = row * TilesPerRow + column;
            return coordinate;
        }

        public static double CalculateTileSize(double width, int tilesPerRow) {
            return width / tilesPerRow;
        }

        public Tile GetTileFromScreenPoint(Game game, Point screenPoint) {
            int coordinate = ConvertToTileCoordinate(screenPoint);
            return game.CurrentBoard.GetTile(coordinate);
        }
    }
}