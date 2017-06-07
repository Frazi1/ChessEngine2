using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using chessengine.board;
using chessengine.board.moves;
using chessengine.Extensions.logger;
using chessengine.Extensions.logger.debugLogger;
using chessengine.Extensions.logger.progressLogger;

namespace chessengine.player.AI.Minimax {
    public class Minimax : IStrategy {
        private readonly IBoardEvaluator _boardEvaluator;
        private readonly int _depth;

        //logging
        private readonly ILogger _logger;

        private readonly IProgressLogger _progressLogger;

        private int _counter = 0;

        //
        public int Depth {
            get { return _depth; }
        }

        public IBoardEvaluator BoardEvaluator {
            get { return _boardEvaluator; }
        }

        #region Constructors

        public Minimax(int depth) {
            _boardEvaluator = new BoardEvaluator();
            _depth = depth;
            _logger = new DebugLogger();
        }

        public Minimax(int depth, IProgressLogger logger) {
            _boardEvaluator = new BoardEvaluator();
            _depth = depth;
            _progressLogger = logger;
        }

        #endregion

        public Move SelectMove(Board board, Player player) {
            int maxValue = int.MinValue;
            Move bestMove = Move.NullMove;
            Alliance.AllianceEnum allianceToEvaluate = player.PlayerAlliance;
            //logging
            Stopwatch s = new Stopwatch();
            if (_logger != null) {
                _logger.Log(string.Format("Depth:{0}", _depth));
                s.Start();
            }
            //
            foreach (Move move in board.CurrentPlayer.LegalMoves) {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                if (moveTransition.MoveStatus != MoveStatus.Done)
                    continue;
                int currentValue = Min(
                    moveTransition.TransitionBoard,
                    allianceToEvaluate,
                    Depth - 1);
                if (currentValue < maxValue)
                    continue;
                maxValue = currentValue;
                bestMove = move;
            }

            //logging
            if (_logger != null) {
                s.Stop();
                _logger.Log(string.Concat("Calc time:", s.Elapsed.ToString(), " Count:", _counter));
                //}
            }
            return bestMove;
        }

        public Move SelectMoveParallel(Board board, Player player) {
            object syncRoot = new object();
            int maxValue = int.MinValue;
            Move bestMove = Move.NullMove;
            Alliance.AllianceEnum allianceToEvaluate = player.PlayerAlliance;

            _counter = 0;
            Stopwatch s = new Stopwatch();
            //logging
            if (_progressLogger != null) {
                _progressLogger.Log(string.Format("Depth:{0}", _depth));
                _progressLogger.JobCount = (ulong) board.CurrentPlayer.LegalMoves.Count;
                _progressLogger.CurrentPosition = 0;
                s.Start();
            }
            //
            Parallel.ForEach(board.CurrentPlayer.LegalMoves, move => {
                MoveTransition moveTransition = board.CurrentPlayer.MakeMove(move);
                if (moveTransition.MoveStatus != MoveStatus.Done)
                    return;
                int currentValue = Min(
                    moveTransition.TransitionBoard,
                    allianceToEvaluate,
                    Depth - 1);
                if (currentValue <= maxValue)
                    return;
                lock (syncRoot) {
                    maxValue = currentValue;
                    bestMove = move;
                }
                _progressLogger.CurrentPosition++;
            });

            //logging
            s.Stop();
            if (_progressLogger != null) {
                _progressLogger.Log(string.Concat("Calc time:", s.Elapsed.ToString(), " Count:", _counter));
                _progressLogger.Log(bestMove.ToString());
                _progressLogger.Log("=============================");
            }
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
                if (moveTransition.MoveStatus != MoveStatus.Done)
                    continue;

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
                if (moveTransition.MoveStatus != MoveStatus.Done)
                    continue;
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