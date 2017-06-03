using System;
using System.Threading.Tasks;
using chessengine.board;
using chessengine.board.moves;
using chessengine.Extensions.EnumExtensions;

namespace chessengine.player.AI.Minimax {
    public class Minimax : IStrategy {
        private IBoardEvaluator _boardEvaluator;
        private int _depth;
        public Minimax(int depth) {
            _boardEvaluator = new BoardEvaluator();
            _depth = depth;
        }

        public Move SelectMove(Board board, Alliance.AllianceEnum alliance) {
            return MinMax(board, alliance);
        }

        private Move MinMax(Board board, Alliance.AllianceEnum alliance) {
            int max = int.MinValue;
            int min = int.MaxValue;
            Move move = Move.NullMove;

            foreach (Move currentMove in board.CurrentPlayer.LegalMoves) {
                //TODO: more work
            }

            return move;
        }
    }
}