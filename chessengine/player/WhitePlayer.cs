using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using chessengine.board;
using chessengine.board.moves;
using chessengine.board.moves.castle;
using chessengine.board.tiles;
using chessengine.pieces;

namespace chessengine.player {
    public class WhitePlayer : Player {
        public override ICollection<Piece> ActivePieces {
            get { return Board.WhitePieces; }
        }

        public override Alliance.AllianceEnum PlayerAlliance {
            get { return Alliance.AllianceEnum.White; }
        }

        public override Player Opponent { get { return Board.BlackPlayer; } }

        public WhitePlayer(Board board,
            ICollection<Move> whiteStandardLegalMoves,
            ICollection<Move> blackStandardLegalMoves)
            : base(board, whiteStandardLegalMoves, blackStandardLegalMoves) {
        }

        protected override King GetKing() {
            return Board
                .WhitePieces
                .FirstOrDefault(p => p.PieceType == PieceType.King) as King;
        }

        protected override ICollection<Move> CalcucateKingCastles(ICollection<Move> playerLegalMoves,
            ICollection<Move> opponentLegalMoves) {
            List<Move> kingCastles = new List<Move>();
            if (!PlayerKing.IsFirstMove || IsInCheck) return ImmutableList.CreateRange(kingCastles);
            if (!Board.GetTile(61).IsTileOccupied && !Board.GetTile(62).IsTileOccupied) {
                Tile rookTile = Board.GetTile(63);
                if (rookTile.IsTileOccupied && rookTile.Piece.IsFirstMove) {
                    if (CalculateAttacksOnTile(61, opponentLegalMoves).Count == 0
                        && CalculateAttacksOnTile(62, opponentLegalMoves).Count == 0
                        && rookTile.Piece.PieceType == PieceType.Rook) {
                        kingCastles.Add(new KingSideCastleMove(
                            Board,
                            PlayerKing,
                            62,
                            (Rook) rookTile.Piece,
                            rookTile.Coordinate,
                            61));
                    }
                }
            }
            if (Board.GetTile(59).IsTileOccupied || Board.GetTile(58).IsTileOccupied ||
                Board.GetTile(57).IsTileOccupied) return ImmutableList.CreateRange(kingCastles);
            {
                Tile rookTile = Board.GetTile(56);
                if (!rookTile.IsTileOccupied || !rookTile.Piece.IsFirstMove)
                    return ImmutableList.CreateRange(kingCastles);
                if (CalculateAttacksOnTile(59, opponentLegalMoves).Count == 0
                    && CalculateAttacksOnTile(58, opponentLegalMoves).Count == 0
                    && CalculateAttacksOnTile(57, opponentLegalMoves).Count == 0
                    && rookTile.Piece.PieceType == PieceType.Rook) {
                    kingCastles.Add(new QueenSideCastleMove(
                        Board,
                        PlayerKing,
                        58,
                        (Rook)rookTile.Piece,
                        rookTile.Coordinate,
                        59));
                }
            }

            return ImmutableList.CreateRange(kingCastles);
        }
    }
}