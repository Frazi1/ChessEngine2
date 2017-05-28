using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using chessengine.board;
using chessengine.board.moves;
using chessengine.pieces;

namespace chessengine.player {
    public abstract class Player {
        public bool IsInCheck { get; set; }
        protected Board Board { get; set; }
        public King PlayerKing { get; set; }
        public ICollection<Move> LegalMoves { get; set; }
        public abstract ICollection<Piece> ActivePieces { get; }
        public abstract Alliance.AllianceEnum PlayerAlliance { get; }
        public abstract Player Opponent { get; }

        public Player(Board board,
            ICollection<Move> legalMoves,
            ICollection<Move> opponentMoves) {
            Board = board;
            PlayerKing = GetKing();
            LegalMoves = ImmutableList.CreateRange(legalMoves.Concat(CalcucateKingCastles(legalMoves, opponentMoves)));
            IsInCheck = CalculateAttacksOnTile(PlayerKing.PiecePosition, opponentMoves).Count > 0;
        }

        public static ICollection<Move> CalculateAttacksOnTile(int piecePosition,
            ICollection<Move> opponentMoves) {
            List<Move> attackMoves = new List<Move>();
            foreach (Move opponentMove in opponentMoves) {
                if (piecePosition == opponentMove.DestinationCoordinate) {
                    attackMoves.Add(opponentMove);
                }
            }
            return ImmutableList.CreateRange(attackMoves);
        }

        protected abstract King GetKing();

        protected abstract ICollection<Move> CalcucateKingCastles(ICollection<Move> playerLegalMoves,
            ICollection<Move> opponentLegalMoves);

        public bool IsMoveLegal(Move move) {
            return LegalMoves.Contains(move);
        }

        public bool IsInCheckMate() {
            return IsInCheck && GetEscapeMoves().Count == 0;
        }

        public bool IsInStale() {
            return !IsInCheck && GetEscapeMoves().Count == 0;
        }

        public bool IsCastled() {
            throw new NotImplementedException();
        }


        protected ICollection<Move> GetEscapeMoves() {
            List<Move> escapeMoves = LegalMoves
                .Select(move => new {move, transition = MakeMove(move)})
                .Where(t => t.transition.MoveStatus == MoveStatus.Done)
                .Select(t => t.move).ToList();
            return ImmutableList.CreateRange(escapeMoves);
        }

        protected MoveTransition MakeMove(Move move) {
            if (!IsMoveLegal(move)) {
                return new MoveTransition(Board, move, MoveStatus.Illegal);
            }

            Board transitionBoard = move.Execute();

            ICollection<Move> kingAttacks =
                CalculateAttacksOnTile(transitionBoard.CurrentPlayer.Opponent.PlayerKing.PiecePosition,
                    transitionBoard.CurrentPlayer.LegalMoves);

            if (kingAttacks.Count != 0) {
                return new MoveTransition(Board, move, MoveStatus.LeavesPlayerInCheck);
            }

            return new MoveTransition(transitionBoard, move, MoveStatus.Done);
        }
    }
}