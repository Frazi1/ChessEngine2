using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using chessengine;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.player;

namespace chessui.Controller {
    public class ChessController {
        private Game Game { get; set; }
        private ChessBoard ChessBoard { get; set; }

        public int TilesPerRow {
            get { return Game.NumTilesPerRow; }
        }

        public int TilesNumber {
            get { return Game.NumTiles; }
        }

        public Tile SelectedTile { get; set; }

        public ChessController(Game game, ChessBoard chessBoard) {
            Game = game;
            ChessBoard = chessBoard;
            ChessBoard.Loaded += ChessBoard_Loaded;
        }

        private void ChessBoard_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                if (SelectedTile == null) {
                    SelectedTile = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard));
                } else {
                    Tile secondTile = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard));
                    MessageBox.Show(InvokeMove(SelectedTile, secondTile).ToString());
                    SelectedTile = null;
                }
            } else if (e.RightButton == MouseButtonState.Pressed) {
                //MessageBox.Show(Game.DoStrategyMove().ToString());
                var piece = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard)).Piece;
                if(piece==null) return;
                var moves = piece.CalculateLegalMoves(Game.CurrentBoard);
                string res = string.Empty;
                if(moves==null) return;
                foreach (Move move in moves) {
                    res = String.Concat(res,move.ToString(), Environment.NewLine);
                }
                MessageBox.Show(res);
            }

        }

        private MoveStatus InvokeMove(Tile fromTile, Tile toTile) {
            return Game.DoMove(fromTile.Coordinate, toTile.Coordinate);
        }

        private void RenderBoard(Game game) {
            ChessBoard.ChessBoardCanvas.Children.Clear();
            for (int i = 0; i < TilesPerRow; i++) {
                bool colored = i % 2 != 0;
                for (int j = 0; j < TilesPerRow; j++) {

                    Tile tile = game.CurrentBoard.GetTile(i * TilesPerRow + j);
                    ChessBoard.RenderTile(tile, colored);
                    colored = !colored;
                }
            }
        }

        private void ChessBoard_Loaded(object sender, RoutedEventArgs e) {
            Game.BoardChanged += Game_BoardChanged;
            ChessBoard.MouseDown += ChessBoard_MouseDown;
            //TODO: fix
            ((Control)((Grid)ChessBoard.Parent).Parent).KeyDown += ChessController_KeyDown;
            RenderBoard(Game);

        }

        private void Game_BoardChanged(object sender, BoardChangedArgs args) {
            RenderBoard(Game);
            if (args.IsGameOver) {
                MessageBox.Show("CheckMate");
            }
        }

        private void ChessController_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                MessageBox.Show(Game.DoStrategyMove().ToString());
            }
        }
    }
}