using System.Windows;
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
            Game.BoardChanged += Game_BoardChanged;
            ChessBoard.MouseDown += ChessBoard_MouseDown;
        }

        private void ChessBoard_MouseDown(object sender, MouseButtonEventArgs e) {
            if (SelectedTile == null) {
                SelectedTile = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard));
            } else {
                Tile secondTile = Helper.Instance.GetTileFromScreenPoint(Game, e.GetPosition(ChessBoard));
                MessageBox.Show(InvokeMove(SelectedTile, secondTile).ToString());
                SelectedTile = null;

            }
        }

        private MoveStatus InvokeMove(Tile selectedTile, Tile secondTile) {
            Move move = FindMove(selectedTile, secondTile);
            return Game.DoMove(move);
        }

        private Move FindMove(Tile from, Tile to) {
            Move move = null;
            foreach (Move m in Game.CurrentBoard.CurrentPlayer.LegalMoves) {
                if (m.MovedPiece.PiecePosition ==
                    from.Coordinate && m.DestinationCoordinate == to.Coordinate) {
                    move = m;
                    break;
                }
            }
            return move ?? Move.NullMove;
        }


        private void Game_BoardChanged() {
            ChessBoard.Render(Game);
        }

    }
}