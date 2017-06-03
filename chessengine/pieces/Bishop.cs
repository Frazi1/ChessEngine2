using System.Collections.Generic;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.tiles;

namespace chessengine.pieces {
    public class Bishop : Piece {
        private static readonly int[] CandidateMoveVectorCoordinates = { -9, -7, 7, 9 };

        public Bishop(int piecePosition, bool isFirstMove, Alliance.AllianceEnum pieceAlliance)
            : base(piecePosition, isFirstMove, pieceAlliance, PieceType.Bishop) {
        }

        public override ICollection<Move> CalculateLegalMoves(Board board) {
            List<Move> legasMoves = new List<Move>();
            foreach (int coordinateOffset in CandidateMoveVectorCoordinates) {
                int candidateDestinationCoordinate = PiecePosition;

                while (BoardUtils.IsValidCoordinate(candidateDestinationCoordinate)) {
                    if (IsFirstColumnExlusion(candidateDestinationCoordinate, coordinateOffset)
                        || IsEightsColumnExlusion(candidateDestinationCoordinate, coordinateOffset))
                        break;
                    candidateDestinationCoordinate += coordinateOffset;

                    if (!BoardUtils.IsValidCoordinate(candidateDestinationCoordinate)) continue;



                    Tile candidateDestinationTile = board.GetTile(candidateDestinationCoordinate);

                    if (!candidateDestinationTile.IsTileOccupied) {
                        legasMoves.Add(new MajorMove(board, this, candidateDestinationCoordinate));
                    } else {
                        Piece pieceAtDestination = candidateDestinationTile.Piece;
                        Alliance.AllianceEnum pieceAlliance = pieceAtDestination.PieceAlliance;

                        if (PieceAlliance != pieceAlliance) {
                            legasMoves.Add(new AttackMove(board, this, candidateDestinationCoordinate,
                                pieceAtDestination));
                        }
                        break;
                    }
                }
            }
            return legasMoves;
        }

        public override Piece MovePiece(Move move) {
            return new Bishop(move.DestinationCoordinate, false, move.MovedPiece.PieceAlliance);
        }

        private static bool IsFirstColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.FirstColumn[currentPosition] && (candidateOffset == -9 || candidateOffset == 7);
        }

        private static bool IsEightsColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.EighthColumn[currentPosition] && (candidateOffset == -7 || candidateOffset == 9);
        }

        //public override string ToString() {
        //    return PieceType.Bishop.ToText();
        //}
    }
}