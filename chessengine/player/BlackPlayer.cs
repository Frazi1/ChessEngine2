using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.moves.castle;
using chessengine.board.tiles;
using chessengine.pieces;

namespace chessengine.player {
    public class BlackPlayer : Player {
        public override ICollection<Piece> ActivePieces {
            get { return Board.BlackPieces; }
        }

        public override Alliance.AllianceEnum PlayerAlliance {
            get { return Alliance.AllianceEnum.Black; }
        }

        public override Player Opponent {
            get { return Board.WhitePlayer; }
        }

        public BlackPlayer(Board board,
            ICollection<Move> whiteStandardLegalMoves,
            ICollection<Move> blackStandardLegalMoves)
            : base(board, blackStandardLegalMoves, whiteStandardLegalMoves) {
        }

        protected override King GetKing() {
            return Board
                .BlackPieces
                .FirstOrDefault(p => p.PieceType == PieceType.King) as King;
        }

        protected override ICollection<Move> CalcucateKingCastles(ICollection<Move> playerLegalMoves,
            ICollection<Move> opponentLegalMoves) {
            List<Move> kingCastles = new List<Move>();
            if (!PlayerKing.IsFirstMove || IsInCheck) return ImmutableList.CreateRange(kingCastles);
            if (!Board.GetTile(5).IsTileOccupied && !Board.GetTile(6).IsTileOccupied) {
                Tile rookTile = Board.GetTile(7);
                if (rookTile.IsTileOccupied && rookTile.Piece.IsFirstMove) {
                    if (CalculateAttacksOnTile(5, opponentLegalMoves).Count == 0
                        && CalculateAttacksOnTile(6, opponentLegalMoves).Count == 0
                        && rookTile.Piece.PieceType == PieceType.Rook) {
                        kingCastles.Add(new KingSideCastleMove(
                            Board,
                            PlayerKing,
                            6,
                            (Rook)rookTile.Piece,
                            rookTile.Coordinate,
                            5));
                    }
                }
            }
            if (Board.GetTile(1).IsTileOccupied || Board.GetTile(2).IsTileOccupied || Board.GetTile(3).IsTileOccupied)
                return ImmutableList.CreateRange(kingCastles);
            {
                Tile rookTile = Board.GetTile(0);
                if (!rookTile.IsTileOccupied || !rookTile.Piece.IsFirstMove)
                    return ImmutableList.CreateRange(kingCastles);
                if (CalculateAttacksOnTile(1, opponentLegalMoves).Count == 0
                    && CalculateAttacksOnTile(2, opponentLegalMoves).Count == 0
                    && CalculateAttacksOnTile(3, opponentLegalMoves).Count == 0
                    && rookTile.Piece.PieceType == PieceType.Rook) {
                    kingCastles.Add(new QueenSideCastleMove(
                        Board,
                        PlayerKing,
                        2,
                        (Rook)rookTile.Piece,
                        rookTile.Coordinate,
                        3));
                }
            }

            return ImmutableList.CreateRange(kingCastles);
        }
    }
}