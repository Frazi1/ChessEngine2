using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.moves.pawn;

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
                    if (CanPrommote(candidateDestinationCoordinate)) {
                        legalMoves.Add(new PawnPromotion(board,
                            new PawnMove(board, this, candidateDestinationCoordinate)));
                    } else {
                        legalMoves.Add(new PawnMove(board, this, candidateDestinationCoordinate));
                    }
                } else if (currentCandidateOffset == 16
                           && IsFirstMove
                           && (BoardUtils.SecondRank[PiecePosition] && PieceAlliance == Alliance.AllianceEnum.Black
                           || BoardUtils.SeventhRank[PiecePosition] && PieceAlliance == Alliance.AllianceEnum.White)) {
                    int behindCandidateDestination = PiecePosition + Alliance.GetDirection(PieceAlliance) * 8;
                    if (!board.GetTile(behindCandidateDestination).IsTileOccupied
                        && !board.GetTile(candidateDestinationCoordinate).IsTileOccupied) {
                        legalMoves.Add(new PawnJump(board, this, candidateDestinationCoordinate));
                    }
                } else if (currentCandidateOffset == 7
                           && !BoardUtils.EighthColumn[PiecePosition] && PieceAlliance == Alliance.AllianceEnum.White
                           || !BoardUtils.FirstColumn[PiecePosition] && PieceAlliance == Alliance.AllianceEnum.Black) {
                    //EnPassantAttack
                    if (board.EnPassantPawn != null && board.EnPassantPawn.PiecePosition ==
                        PiecePosition + Alliance.GetOppositeDirection(PieceAlliance)) {
                        legalMoves.Add(new PawnEnPassantAttackMove(board, this, candidateDestinationCoordinate, board.EnPassantPawn));
                    }
                    //AttackMove
                    if (!board.GetTile(candidateDestinationCoordinate).IsTileOccupied) continue;
                    Piece pieceOnCandidate = board.GetTile(candidateDestinationCoordinate).Piece;
                    if (PieceAlliance == pieceOnCandidate.PieceAlliance) continue;
                    if (CanPrommote(candidateDestinationCoordinate)) {
                        legalMoves.Add(new PawnPromotion(board, new PawnAttackMove(board, this, candidateDestinationCoordinate, pieceOnCandidate)));
                    } else {
                        legalMoves.Add(new PawnAttackMove(board,
                            this,
                            candidateDestinationCoordinate,
                            board.GetTile(candidateDestinationCoordinate).Piece));
                    }
                } else if (currentCandidateOffset == 9
                           && !BoardUtils.FirstColumn[PiecePosition] && PieceAlliance == Alliance.AllianceEnum.White
                           || !BoardUtils.EighthColumn[PiecePosition] && PieceAlliance == Alliance.AllianceEnum.Black) {
                    //EnPassant
                    if (board.EnPassantPawn != null && board.EnPassantPawn.PiecePosition ==
                        PiecePosition - Alliance.GetOppositeDirection(PieceAlliance)) {
                        legalMoves.Add(new PawnEnPassantAttackMove(board, this, candidateDestinationCoordinate, board.EnPassantPawn));
                    }
                    //AttackMove
                    if (!board.GetTile(candidateDestinationCoordinate).IsTileOccupied) continue;
                    Piece pieceOnCandidate = board.GetTile(candidateDestinationCoordinate).Piece;
                    if (PieceAlliance == pieceOnCandidate.PieceAlliance) continue;
                    if (CanPrommote(candidateDestinationCoordinate)) {
                        legalMoves.Add(new PawnPromotion(board,
                            new PawnAttackMove(board, this, candidateDestinationCoordinate, pieceOnCandidate)));
                    } else {
                        legalMoves.Add(new PawnAttackMove(board,
                            this,
                            candidateDestinationCoordinate,
                            board.GetTile(candidateDestinationCoordinate).Piece));
                    }
                }
            }

            return ImmutableList.CreateRange(legalMoves);
        }

        private bool CanPrommote(int candidateDestinationCoordinate) {
            return BoardUtils.FirstRank[candidateDestinationCoordinate] && PieceAlliance == Alliance.AllianceEnum.White
                || BoardUtils.EigthRank[candidateDestinationCoordinate] && PieceAlliance == Alliance.AllianceEnum.Black;
        }

        public override Piece MovePiece(Move move) {
            return new Pawn(move.DestinationCoordinate, false, move.MovedPiece.PieceAlliance);
        }

        public Piece GetPrommotion(PieceType pieceType) {
            switch (pieceType) {
                case PieceType.Rook:
                    return new Rook(PiecePosition, IsFirstMove, PieceAlliance);
                case PieceType.Knight:
                    return new Knight(PiecePosition, IsFirstMove, PieceAlliance);
                case PieceType.Bishop:
                    return new Bishop(PiecePosition, IsFirstMove, PieceAlliance);
                case PieceType.Queen:
                    return new Queen(PiecePosition, IsFirstMove, PieceAlliance);
                default:
                    throw new ArgumentOutOfRangeException(nameof(pieceType), pieceType, null);
            }
        }
        //public override string ToString() {
        //    return PieceType.Pawn.ToText();
        //}
    }
}