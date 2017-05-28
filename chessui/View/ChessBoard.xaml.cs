using chessengine.board.tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using chessengine;
using chessengine.pieces;
using chessui.Controller;

namespace chessui {
    /// <summary>
    /// Логика взаимодействия для ChessBoard.xaml
    /// </summary>
    public partial class ChessBoard : UserControl {

        #region Dependecy Properties
        public Game Game {
            get { return (Game)GetValue(ChessGameProperty); }
            set { SetValue(ChessGameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Game.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChessGameProperty =
            DependencyProperty.Register("Game", typeof(Game), typeof(ChessBoard), new FrameworkPropertyMetadata(default(Game)));



        public Tile SelectedTile {
            get { return ()GetValue(SelectedTileProperty); }
            set { SetValue(SelectedTileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTileProperty =
            DependencyProperty.Register("SelectedTile", typeof(), typeof(ChessBoard), new FrameworkPropertyMetadata(default(Tile)));



        public double TileSize {
            get { return (double)GetValue(TileSizeProperty); }
            set { SetValue(TileSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TileSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TileSizeProperty =
            DependencyProperty.Register("TileSize", typeof(double), typeof(ChessBoard), new FrameworkPropertyMetadata(100.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public int TilesNumber {
            get { return (int)GetValue(TilesNumberProperty); }
            set { SetValue(TilesNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TilesNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TilesNumberProperty =
            DependencyProperty.Register("TilesNumber", typeof(int), typeof(ChessBoard), new PropertyMetadata(64));

        public int TilesPerRow {
            get { return (int)GetValue(TilesPerRowProperty); }
            set { SetValue(TilesPerRowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TilesPerRow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TilesPerRowProperty =
            DependencyProperty.Register("TilesPerRow", typeof(int), typeof(ChessBoard), new PropertyMetadata(8));

        #endregion

        public ChessBoard() {
            InitializeComponent();
        }

        #region Render Methods
        public void Render() {
            if (DataContext != null) {
                Game game = (Game)this.DataContext;

                RenderBoard(game);
            }
        }

        private void RenderBoard(Game game) {
            for (int i = 0; i < TilesPerRow; i++) {
                bool colored = i % 2 != 0;
                for (int j = 0; j < TilesPerRow; j++) {

                    Tile tile = game.CurrentBoard.GetTile(i * TilesPerRow + j);
                    RenderTile(tile, colored);
                    colored = !colored;
                }
            }
        }

        private void RenderTile(Tile tile, bool colored) {
            Point tilePoint = Helper.ConvertTileCoordinateToPoint(tile.Coordinate, TilesPerRow);
            Point screenPoint = Helper.ConvertToScreenPoint(tilePoint, TileSize);
            Brush brush = colored ? Brushes.Coral : Brushes.Beige;
            Rectangle tileRectangle = new Rectangle() {
                Fill = brush,
                Height = TileSize,
                Width = TileSize,
                Margin = new Thickness(screenPoint.X, screenPoint.Y, 0, 0)
            };
            ChessBoardCanvas.Children.Add(tileRectangle);

            if (tile.IsTileOccupied) {
                RenderPiece(tile.Piece, screenPoint);
            }
        }

        private void RenderPiece(Piece tilePiece, Point screenPoint) {
            string imagePath = Helper.SelectPieceImage(tilePiece);
            Image pieceImage = new Image() {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                Margin = new Thickness(screenPoint.X, screenPoint.Y, 0, 0),
                Width = TileSize,
                Height = TileSize
            };
            this.ChessBoardCanvas.Children.Add(pieceImage);

        }

        #endregion

        private void ChessBoardCanvas_Loaded(object sender, RoutedEventArgs e) {
            Game = (Game)DataContext;
            Render();
        }

        private void ChessBoardCanvas_MouseDown(object sender, MouseButtonEventArgs e) {
            if (SelectedTile != null) {

            }
        }
    }
}
