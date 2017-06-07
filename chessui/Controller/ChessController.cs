using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.game;
using chessengine.game.events;
using chessengine.pieces;
using chessengine.player;

namespace chessui.Controller {
    public class ChessController {
        private Game Game { get; set; }
        private ChessBoard ChessBoard { get; set; }

        private Tile SelectedTile { get; set; }

        public ChessController(Game game, ChessBoard chessBoard) {
            Game = game;
            ChessBoard = chessBoard;
            ChessBoard.Loaded += ChessBoard_Loaded;
        }

        private MoveStatus InvokeMove(Tile fromTile, Tile toTile) {
            return Game.DoMove(fromTile.Coordinate, toTile.Coordinate);
        }

        private void ChessBoard_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                if (SelectedTile == null) {
                    SelectedTile = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard));
                } else {
                    Tile secondTile = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard));
                    MoveStatus invokeMove = InvokeMove(SelectedTile, secondTile);
                    if (Debugger.IsAttached) {
                        MessageBox.Show(invokeMove.ToString());
                    }
                    SelectedTile = null;
                }
            } else if (e.RightButton == MouseButtonState.Pressed) {
                Piece piece = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard)).Piece;
                var moves = Game.CurrentBoard.GetAllLegalMoves().Where(m => m.MovedPiece.Equals(piece));
                if (piece == null) return;
//                string res = Helper.ShowLegalMoves(piece, Game.CurrentBoard);
                StringBuilder s = new StringBuilder();
                foreach (Move move in moves) {
                    s.Append(move);
                    s.Append(Environment.NewLine);
                }
                MessageBox.Show(s.ToString());
            }
        }

        private void ChessBoard_Loaded(object sender, RoutedEventArgs e) {
            Game.BoardChanged += Game_BoardChanged;
            ChessBoard.MouseDown += ChessBoard_MouseDown;
            //TODO: fix
            ((Control) ((Grid) ChessBoard.Parent).Parent).KeyDown += ChessController_KeyDown;
            RenderHelper.RenderBoard(Game, ChessBoard);
        }

        private void Game_BoardChanged(object sender, BoardChangedArgs args) {
            RenderHelper.RenderBoard(Game, ChessBoard);
            if (args.IsGameOver) {
                MessageBox.Show("CheckMate");
            }
        }

        private void ChessController_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                MoveTransition moveTransition = Game.DoStrategyMove();
                if (Debugger.IsAttached) {
                    MessageBox.Show(moveTransition.ToString());
                }
            }
        }
    }
}