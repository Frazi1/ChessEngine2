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

        protected Player(Board board,
            ICollection<Move> legalMoves,
            ICollection<Move> opponentMoves) {
            Board = board;
            PlayerKing = GetKing();
            LegalMoves = ImmutableList.CreateRange(legalMoves.Concat(CalcucateKingCastles(legalMoves, opponentMoves)));
            IsInCheck = CalculateAttacksOnTile(PlayerKing.PiecePosition, opponentMoves).Count > 0;
        }

        protected static ICollection<Move> CalculateAttacksOnTile(int piecePosition,
            ICollection<Move> opponentMoves) {
            List<Move> attackMoves = new List<Move>();
            foreach (Move opponentMove in opponentMoves) {
                if (piecePosition == opponentMove.DestinationCoordinate)
                    attackMoves.Add(opponentMove);
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

        private ICollection<Move> GetEscapeMoves() {
            List<Move> escapeMoves = new List<Move>();
            foreach (Move move in LegalMoves) {
                if (move.MovedPiece.PieceType == PieceType.Bishop && move.DestinationCoordinate == 40) {
                    object _break = 100;
                }
                MoveTransition transition = MakeMove(move);
                if (transition.MoveStatus == MoveStatus.Done) escapeMoves.Add(move);
            }
            return ImmutableList.CreateRange(escapeMoves);
        }

        public MoveTransition MakeMove(Move move) {
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