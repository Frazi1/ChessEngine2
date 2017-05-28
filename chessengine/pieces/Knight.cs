﻿using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.board;

namespace chessengine.pieces {
    public class Knight : Piece {

        private static readonly int[] CandidateMoveCoordinates = { -17, -15, -10, -6, 6, 10, 15, 17 };

        public Knight(int piecePosition, Alliance pieceAlliance)
            : base(piecePosition, pieceAlliance) {
        }

        public override ICollection<Move> CalculateLegalMoves(Board board) {
            List<Move> legasMoves = new List<Move>();
            foreach (int currentCandidatetOffset in CandidateMoveCoordinates) {
                int candidateDestinationCoordinate = currentCandidatetOffset;
                if (BoardUtils.IsValidCoordinate(candidateDestinationCoordinate)) {

                    if (IsFirstColumnExlusion(this.PiecePosition, currentCandidatetOffset)
                        || IsSecondColumnExclusion(PiecePosition, candidateDestinationCoordinate)
                        || IsSeventhColumnExclusion(PiecePosition, candidateDestinationCoordinate)
                        || IsEigthColumnExclusion(PiecePosition, candidateDestinationCoordinate))
                        continue;

                    Tile candidateDestinationTile = board.GetTile(candidateDestinationCoordinate);

                    if (!candidateDestinationTile.IsTileOccupied()) {
                        legasMoves.Add(new MajorMove(board, this, candidateDestinationCoordinate));
                    } else {
                        Piece pieceAtDestination = candidateDestinationTile.GetPiece();
                        Alliance pieceAlliance = pieceAtDestination.PieceAlliance;

                        if (this.PieceAlliance != pieceAlliance) {
                            legasMoves.Add(new AttackMove(board, this, candidateDestinationCoordinate, pieceAtDestination));
                        }
                    }

                }
            }
            return ImmutableList.CreateRange(legasMoves);
        }

        private static bool IsFirstColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.FirstColumn[currentPosition] && (candidateOffset == -17 || candidateOffset == -10 ||
                                                          candidateOffset == 6 || candidateOffset == 15);
        }

        private static bool IsSecondColumnExclusion(int currentPosition, int candidateOffset) {
            return BoardUtils.SecondColumn[currentPosition] && (candidateOffset == -10 || candidateOffset == 6);
        }

        private static bool IsSeventhColumnExclusion(int currentPosition, int candidateOffset) {
            return BoardUtils.SeventhColumn[currentPosition] && (candidateOffset == -6 || candidateOffset == 10);
        }

        private static bool IsEigthColumnExclusion(int currentPosition, int candidateOffset) {
            return BoardUtils.EighthColumn[currentPosition] &&
                   (candidateOffset == -15 || candidateOffset == -6 || candidateOffset == 10 || candidateOffset == 17);
        }

    }
}