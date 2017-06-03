using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using chessengine.board;
using chessengine.board.moves;
using chessengine.Extensions.EnumExtensions;

namespace chessengine.player.AI.Minimax {
    public class Minimax : IStrategy {
        private readonly IBoardEvaluator _boardEvaluator;
        private readonly int _depth;

        public int Depth {
            get {
                return _depth;
            }
        }
        public IBoardEvaluator BoardEvaluator {
            get {
                return _boardEvaluator;
            }
        }

        public Minimax(int depth) {
            _boardEvaluator = new BoardEvaluator();
            _depth = depth;
        }

        public Move SelectMove(Board board, Player player) {
            int maxValue = int.MinValue;
            Move bestMove = Move.NullMove;
            foreach (Move move in board.CurrentPlayer.LegalMoves) {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                if (moveTransition.MoveStatus != MoveStatus.Done) continue;
                int currentValue = Max(moveTransition.TransitionBoard, board.CurrentPlayer.PlayerAlliance, Depth);
                if (currentValue < maxValue) continue;
                maxValue = currentValue;
                bestMove = move;
            }
            return bestMove;
        }

        private int Min(Board board, Alliance.AllianceEnum alliance, int depth) {
            if (depth == 0 /*|| gameover*/) {
                return BoardEvaluator.Evaluate(board, alliance);
            }
            int min = int.MaxValue;
            foreach (Move move in board.CurrentPlayer.LegalMoves) {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                if (moveTransition.MoveStatus != MoveStatus.Done) continue;
                int currentValue = Max(moveTransition.TransitionBoard, alliance, depth - 1);
                min = Math.Min(min, currentValue);
            }
            return min;
        }

        private int Max(Board board, Alliance.AllianceEnum alliance, int depth) {
            if (depth == 0) {
                return BoardEvaluator.Evaluate(board, alliance);
            }
            int max = int.MinValue;
            foreach (Move move in board.CurrentPlayer.LegalMoves) {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                if (moveTransition.MoveStatus != MoveStatus.Done) continue;
                int currentValue = Min(moveTransition.TransitionBoard, alliance, depth - 1);
                max = Math.Max(currentValue, max);
            }
            return max;
        }
    }
}