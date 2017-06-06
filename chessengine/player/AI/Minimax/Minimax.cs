using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using chessengine.board;
using chessengine.board.moves;
using chessengine.Extensions.EnumExtensions;
using chessengine.Extensions.Logger;

namespace chessengine.player.AI.Minimax {
    public class Minimax : IStrategy {
        private readonly IBoardEvaluator _boardEvaluator;

        private readonly int _depth;

        //logging
        private readonly ILogger _logger;

        private int _counter = 0;

        //
        public int Depth {
            get { return _depth; }
        }

        public IBoardEvaluator BoardEvaluator {
            get { return _boardEvaluator; }
        }

        public Minimax(int depth) {
            _boardEvaluator = new BoardEvaluator();
            _depth = depth;
            _logger = new DebugLogger();
        }

        public Move SelectMove(Board board, Player player) {
            int maxValue = int.MinValue;
            Move bestMove = Move.NullMove;
            Alliance.AllianceEnum allianceToEvaluate = player.PlayerAlliance;
            //logging
            Stopwatch s = new Stopwatch();
            s.Start();
            //
            foreach (Move move in board.CurrentPlayer.LegalMoves) {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                if (moveTransition.MoveStatus != MoveStatus.Done) continue;
                int currentValue = Min(
                    moveTransition.TransitionBoard,
                    allianceToEvaluate,
                    Depth - 1);
                if (currentValue < maxValue) continue;
                maxValue = currentValue;
                bestMove = move;
            }

            //logging
            s.Stop();
            _logger.Log(string.Concat("Calc time:", s.Elapsed.ToString(), " Count:", _counter));
            //
            return bestMove;
        }

        public Move SelectMoveParallel(Board board, Player player) {
            object syncRoot = new object();
            int maxValue = int.MinValue;
            Move bestMove = Move.NullMove;
            Alliance.AllianceEnum allianceToEvaluate = player.PlayerAlliance;
            _counter = 0;
            //logging
            Stopwatch s = new Stopwatch();
            s.Start();
            //
            Parallel.ForEach(board.CurrentPlayer.LegalMoves, move => {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                if (moveTransition.MoveStatus != MoveStatus.Done) return;
                int currentValue = Min(
                    moveTransition.TransitionBoard,
                    allianceToEvaluate,
                    Depth - 1);
                if (currentValue <= maxValue) return;
                lock (syncRoot) {
                    maxValue = currentValue;
                    bestMove = move;
                }
            });

            //logging
            s.Stop();
            _logger.Log(string.Concat("Calc time:", s.Elapsed.ToString(), " Count:", _counter));
            //
            return bestMove;
        }

        private int Min(Board board, Alliance.AllianceEnum allianceToEvaluate, int depth) {
            if (depth == 0 || IsGameOver(board.CurrentPlayer)) {
                return BoardEvaluator.Evaluate(board, allianceToEvaluate);
            }
            int min = int.MaxValue;
            foreach (Move move in board.CurrentPlayer.LegalMoves) {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                //logging
                _counter++;
                //logging
                if (moveTransition.MoveStatus != MoveStatus.Done) continue;
                int currentValue = Max(
                    moveTransition.TransitionBoard,
                    allianceToEvaluate,
                    depth - 1);
                min = Math.Min(min, currentValue);
            }
            return min;
        }

        private int Max(Board board, Alliance.AllianceEnum allianceToEvaluate, int depth) {
            if (depth == 0 || IsGameOver(board.CurrentPlayer)) {
                return BoardEvaluator.Evaluate(board, allianceToEvaluate);
            }
            int max = int.MinValue;
            foreach (Move move in board.CurrentPlayer.LegalMoves) {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                //logging
                _counter++;
                //logging
                if (moveTransition.MoveStatus != MoveStatus.Done) continue;
                int currentValue = Min(
                    moveTransition.TransitionBoard,
                    allianceToEvaluate,
                    depth - 1);
                max = Math.Max(currentValue, max);
            }
            return max;
        }

        private static bool IsGameOver(Player player) {
            return player.IsInStale() || player.IsInCheckMate();
        }
    }
}