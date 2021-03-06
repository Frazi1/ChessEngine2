﻿using System.Collections.Generic;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.tiles;

namespace chessengine.pieces {
    public class Rook : Piece {

        private static readonly int[] CandidateMoveVectorCoordinates = { -8, -1, 1, 8 };

        public Rook(int piecePosition, bool isFirstMove, Alliance.AllianceEnum pieceAlliance)
            : base(piecePosition, isFirstMove, pieceAlliance, PieceType.Rook) {
        }

        public override ICollection<Move> CalculateLegalMoves(Board board) {
            List<Move> legasMoves = new List<Move>();
            foreach (int candidateOffset in CandidateMoveVectorCoordinates) {
                int candidateDestinationCoordinate = PiecePosition;

                while (BoardUtils.IsValidCoordinate(candidateDestinationCoordinate)) {

                    if (IsFirstColumnExlusion(candidateDestinationCoordinate, candidateOffset)
                        || IsEigthsColumnExlusion(candidateDestinationCoordinate, candidateOffset))
                        break;
                    candidateDestinationCoordinate += candidateOffset;

                    if (!BoardUtils.IsValidCoordinate(candidateDestinationCoordinate)) continue;


                    Tile candidateDestinationTile = board.GetTile(candidateDestinationCoordinate);

                    if (!candidateDestinationTile.IsTileOccupied) {
                        legasMoves.Add(new MajorMove(board, this, candidateDestinationCoordinate));
                    } else {
                        Piece pieceAtDestination = candidateDestinationTile.Piece;
                        Alliance.AllianceEnum pieceAlliance = pieceAtDestination.PieceAlliance;

                        if (PieceAlliance != pieceAlliance) {
                            legasMoves.Add(new AttackMove(board, this, candidateDestinationCoordinate, pieceAtDestination));
                        }
                        break;
                    }
                }

            }
            return legasMoves;
        }

        public override Piece MovePiece(Move move) {
            return new Rook(move.DestinationCoordinate,false, move.MovedPiece.PieceAlliance);
        }
        
        private static bool IsFirstColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.FirstColumn[currentPosition] && candidateOffset == -1;
        }

        private static bool IsEigthsColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.EighthColumn[currentPosition] && candidateOffset == 1;
        }
        
        //public override string ToString() {
        //    return PieceType.Rook.ToText();
        //}
    }
}