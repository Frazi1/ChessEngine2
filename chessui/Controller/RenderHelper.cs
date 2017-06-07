using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using chessengine.board.tiles;
using chessengine.game;
using chessengine.pieces;

namespace chessui.Controller {
    public static class RenderHelper {
        public static void RenderBoard(Game game, ChessBoard chessBoard) {
            chessBoard.ChessBoardCanvas.Children.Clear();
            for (int i = 0; i < Helper.Instance.TilesPerRow; i++) {
                bool colored = i % 2 != 0;
                for (int j = 0; j < Helper.Instance.TilesPerRow; j++) {
                    Tile tile = game.CurrentBoard.GetTile(i * Helper.Instance.TilesPerRow + j);
                    RenderTile(chessBoard, tile, colored);
                    colored = !colored;
                }
            }
        }

        private static void RenderTile(ChessBoard chessBoard, Tile tile, bool colored) {
            Point screenPoint = Helper.Instance.TileCoordinateToScreenPoint(tile.Coordinate);
            Brush brush = colored ? Brushes.Coral : Brushes.Beige;
            Rectangle tileRectangle = new Rectangle() {
                Fill = brush,
                Height = Helper.Instance.TileSize,
                Width = Helper.Instance.TileSize,
                Margin = new Thickness(screenPoint.X, screenPoint.Y, 0, 0)
            };
            chessBoard.ChessBoardCanvas.Children.Add(tileRectangle);

            TextBlock tb = new TextBlock {
                Text = tile.Coordinate.ToString(),
                Margin = new Thickness(screenPoint.X, screenPoint.Y, 0, 0)
            };
            chessBoard.ChessBoardCanvas.Children.Add(tb);

            if (tile.IsTileOccupied) {
                RenderPiece(chessBoard, tile.Piece, screenPoint);
            }
        }

        private static void RenderPiece(ChessBoard chessBoard, Piece tilePiece, Point screenPoint) {
            string imagePath = Helper.SelectPieceImage(tilePiece);
            Image pieceImage = new Image() {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                Margin = new Thickness(screenPoint.X, screenPoint.Y, 0, 0),
                Width = Helper.Instance.TileSize,
                Height = Helper.Instance.TileSize
            };
            chessBoard.ChessBoardCanvas.Children.Add(pieceImage);
        }
    }
}