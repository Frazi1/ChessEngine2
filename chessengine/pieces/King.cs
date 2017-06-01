using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.tiles;
using chessengine.Extensions.EnumExtensions;

namespace chessengine.pieces {
    public class King : Piece {
        private static readonly int[] CandidateMoveCoordinate = { -9, -8, -7, -1, 1, 7, 8, 9 };

        public King(int piecePosition, bool isFirstMove, Alliance.AllianceEnum pieceAlliance)
            : base(piecePosition, isFirstMove, pieceAlliance, PieceType.King) {
        }

        public override ICollection<Move> CalculateLegalMoves(Board board) {
            List<Move> legasMoves = new List<Move>();

            foreach (int candidateOffset in CandidateMoveCoordinate) {

                int candidateDestinationCoordinate = PiecePosition + candidateOffset;
                if (IsEigthColumnExclusion(PiecePosition, candidateOffset)
                    || IsFirstColumnExlusion(PiecePosition, candidateOffset))
                    continue;

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
                }
            }

            return ImmutableList.CreateRange(legasMoves);
        }

        public override Piece MovePiece(Move move) {
            return new King(move.DestinationCoordinate, false, move.MovedPiece.PieceAlliance);
        }

        private static bool IsFirstColumnExlusion(int currentPosition, int candidateOffset) {
            return BoardUtils.FirstColumn[currentPosition]
                   && (candidateOffset == -9 || candidateOffset == -1 || candidateOffset == 7);
        }

        private static bool IsEigthColumnExclusion(int currentPosition, int candidateOffset) {
            return BoardUtils.EighthColumn[currentPosition] &&
                (candidateOffset == -7 || candidateOffset == 1 || candidateOffset == 9);
        }

        //public override string ToString() {
        //    return PieceType.King.ToText();
        //}
    }
}