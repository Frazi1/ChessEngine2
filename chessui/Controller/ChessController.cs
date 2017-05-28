using chessengine;

namespace chessui.Controller {
    public class ChessController {

        public Game Game { get; set; }
        public ChessBoard ChessBoard { get; set; }
        public ChessController(Game game, ChessBoard chessBoard) {
            Game = game;
            ChessBoard = chessBoard;
            Game.BoardChanged += Game_BoardChanged;
        }

        private void Game_BoardChanged() {
            ChessBoard.Render();
        }
    }
}