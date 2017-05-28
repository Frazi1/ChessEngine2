using System;
using System.Text;
using System.Windows;
using chessengine.pieces;

namespace chessui.Controller {
    public static class Helper {

        public static string SelectPieceImage(Piece tilePiece) {
            string prefix = "Resources/";
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder
                .Append(tilePiece.PieceType)
                .Append("_")
                .Append(tilePiece.PieceAlliance)
                .Append(".png");
            return String.Concat(prefix, pathBuilder.ToString().ToLower());
        }

        public static Point ConvertToScreenPoint(Point tilePoint, double tileSize) {
            double x = tilePoint.X * tileSize;
            double y = tilePoint.Y * tileSize;
            return new Point(x, y);
        }

        public static Point ConvertTileCoordinateToPoint(int coordinate, int tilesPerRow) {
            int row = coordinate / tilesPerRow;
            int column = coordinate % tilesPerRow;
            return new Point(column, row);
        }

        public static int ConvertToTileCoordinate(Point screenPoint, double tileSize, int tilesPerRow) {
            int row = (int)(screenPoint.Y / tileSize);
            int column = (int)(screenPoint.X / tileSize);
            int coordinate = row * tilesPerRow + column;
            return coordinate;
        }

        public static double CalculateTileSize(double width, int tilesPerRow) {
            return width / tilesPerRow;
        }
    }
}