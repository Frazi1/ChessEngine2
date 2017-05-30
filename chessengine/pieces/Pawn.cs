﻿using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.moves.pawn;
using chessengine.Extensions.EnumExtensions;

namespace chessengine.pieces {
    public class Pawn : Piece {
        private static readonly int[] CandidateMoveOffsets = { 7, 8, 9, 16 };

        public Pawn(int piecePosition, bool isFirstMove, Alliance.AllianceEnum pieceAlliance)
            : base(piecePosition, isFirstMove, pieceAlliance, PieceType.Pawn) {
        }

        public override ICollection<Move> CalculateLegalMoves(Board board) {
            List<Move> legalMoves = new List<Move>();

            foreach (int currentCandidateOffset in CandidateMoveOffsets) {
                int candidateDestinationCoordinate = PiecePosition +
                                                     Alliance.GetDirection(PieceAlliance) * currentCandidateOffset;

                if (!BoardUtils.IsValidCoordinate(candidateDestinationCoordinate))
                    continue;

                if (currentCandidateOffset == 8 && !board.GetTile(candidateDestinationCoordinate).IsTileOccupied) {
                    //TODO:Promotion
                    legalMoves.Add(new MajorMove(board, this, candidateDestinationCoordinate));
                } else if (currentCandidateOffset == 16
                           && IsFirstMove
                           && (BoardUtils.SecondRank[PiecePosition] && Alliance.IsBlack(PieceAlliance)
                           || BoardUtils.SeventhRank[PiecePosition] && Alliance.IsWhite(PieceAlliance))) {
                    int behindCandidateDestination = PiecePosition + Alliance.GetDirection(PieceAlliance) * 8;
                    if (!board.GetTile(behindCandidateDestination).IsTileOccupied
                        && !board.GetTile(candidateDestinationCoordinate).IsTileOccupied) {
                        legalMoves.Add(new /*MajorMove*/PawnJump(board, this, candidateDestinationCoordinate));
                    }
                } else if (currentCandidateOffset == 7
                           && !BoardUtils.EighthColumn[PiecePosition] && Alliance.IsWhite(PieceAlliance)
                           || !BoardUtils.FirstColumn[PiecePosition] && Alliance.IsBlack(PieceAlliance)) {
                    if (!board.GetTile(candidateDestinationCoordinate).IsTileOccupied) continue;
                    Piece pieceOnCandidate = board.GetTile(candidateDestinationCoordinate).Piece;
                    if (PieceAlliance != pieceOnCandidate.PieceAlliance) {
                        //TODO more
                        legalMoves.Add(new PawnAttackMove(board,
                            this,
                            candidateDestinationCoordinate,
                            board.GetTile(candidateDestinationCoordinate).Piece));
                    }
                } else if (currentCandidateOffset == 9
                           && !BoardUtils.FirstColumn[PiecePosition] && Alliance.IsWhite(PieceAlliance)
                           || !BoardUtils.EighthColumn[PiecePosition] && Alliance.IsBlack(PieceAlliance)) {
                    if (!board.GetTile(candidateDestinationCoordinate).IsTileOccupied) continue;
                    Piece pieceOnCandidate = board.GetTile(candidateDestinationCoordinate).Piece;
                    if (PieceAlliance != pieceOnCandidate.PieceAlliance) {
                        //TODO more
                        legalMoves.Add(new PawnAttackMove(board,
                            this,
                            candidateDestinationCoordinate,
                            board.GetTile(candidateDestinationCoordinate).Piece));
                    }
                }
            }

            return ImmutableList.CreateRange(legalMoves);
        }

        public override Piece MovePiece(Move move) {
            return new Pawn(move.DestinationCoordinate,false, move.MovedPiece.PieceAlliance);
        }

        //public override string ToString() {
        //    return PieceType.Pawn.ToText();
        //}
    }
}