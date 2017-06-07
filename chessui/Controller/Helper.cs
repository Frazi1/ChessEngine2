using System;
using System.Text;
using System.Windows;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.game;
using chessengine.pieces;

namespace chessui.Controller {
    public class Helper {

        public int NumTiles { get;  private set; }
        public int TilesPerRow { get; private set; }
        public double TileSize { get; private set; }

        public static Helper Instance { get; private set; }

        public static void CreateInstance(int numTiles, int tilesPerRow, double tileSize) {
            Instance = new Helper(numTiles, tilesPerRow, tileSize);
        }

        private Helper(int numTiles, int tilesPerRow, double tileSize) {
            NumTiles = numTiles;
            TilesPerRow = tilesPerRow;
            TileSize = tileSize;
        }

        public Point TileCoordinateToScreenPoint(int coordinate) {
            double x = coordinate % TilesPerRow * TileSize;
            double y = coordinate / TilesPerRow * TileSize;
            return new Point(x, y);
        }

        private int ScreenPointToTileCoordinate(Point screenPoint) {
            int row = (int)(screenPoint.Y / TileSize);
            int column = (int)(screenPoint.X / TileSize);
            int coordinate = row * TilesPerRow + column;
            return coordinate;
        }

        public Tile GetTileFromScreenPoint(Game game, Point screenPoint) {
            int coordinate = ScreenPointToTileCoordinate(screenPoint);
            return game.CurrentBoard.GetTile(coordinate);
        }
        
        public static double CalculateTileSize(double width, int tilesPerRow) {
            return width / tilesPerRow;
        }
        
        public static string SelectPieceImage(Piece tilePiece) {
            const string prefix = "Resources/";
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder
                .Append(tilePiece.PieceType)
                .Append("_")
                .Append(tilePiece.PieceAlliance)
                .Append(".png");
            return string.Concat(prefix, pathBuilder.ToString().ToLower());
        }

        public static string ShowLegalMoves(Piece piece, Board board) {
            var moves = piece.CalculateLegalMoves(board);
            string res = string.Empty;
            if (moves == null) return string.Empty;
            foreach (Move move in moves) {
                res = string.Concat(res, move.ToString(), Environment.NewLine);
            }
            return res;
        }
    }
}