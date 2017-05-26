using System.Collections.Generic;
using chessengine.board;

namespace chessengine.pieces {
    public class Queen : Piece {
        private static readonly int[] CandidateMoveVectorCoordinates = { -9, -8, -7, -1, 1, 7, 8, 9 };
        public Queen(int piecePosition, Alliance pieceAlliance)
            : base(piecePosition, pieceAlliance) {
        }

        public override ICollection<Move> CalculateLegalMoves(Board board) {
            List<Move> legasMoves = new List<Move>();
            foreach (int candidateCoordinateOffset in CandidateMoveVectorCoordinates) {
                int candidateDestinationCoordinate = PiecePosition;

                while (BoardUtils.IsValidCoordinate(candidateDestinationCoordinate)) {
                    candidateDestinationCoordinate += candidateCoordinateOffset;

                    if (IsFirstColumnExlusion(this.PiecePosition, candidateDestinationCoordinate)
                        || IsEigthsColumnExlusion(PiecePosition, candidateDestinationCoordinate))
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
                        break;
                    }
                }

            }
            return legasMoves;
        }

        private static bool IsFirstColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.FirstColumn[currentPosition] &&
                (candidateOffset == -9 || candidateOffset == 7 || candidateOffset == -1);
        }

        private static bool IsEigthsColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.FirstColumn[currentPosition] &&
                (candidateOffset == 1 || candidateOffset == -7 || candidateOffset == 9);
        }
    }
}