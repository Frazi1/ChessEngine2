using System;
using chessengine.pieces;
using chessengine.player;

namespace chessengine.board.moves.pawn {
    public class PawnPromotion : PawnMove {
        public Move Move { get; private set; }
        public PawnPromotion(Board board, Move move)
            : base(board, move.MovedPiece, move.DestinationCoordinate) {
            Move = move;
        }

        public override Board Execute() {
            if (!CanExecute()) throw new Exception();

            Board transitionBoard = Move.Execute();
            Pawn movedPawn = (Pawn)transitionBoard.GetTile(Move.DestinationCoordinate).Piece;
            Builder builder = new Builder();

            foreach (Piece piece in transitionBoard.CurrentPlayer.Opponent.ActivePieces) {
                if (!movedPawn.Equals(piece)) {
                    builder.SetPiece(piece);
                }
            }
            foreach (Piece piece in transitionBoard.CurrentPlayer.ActivePieces) {
                builder.SetPiece(piece);
            }
            Piece prommotedPiece = movedPawn.GetPrommotion(PieceType.Queen);
            builder.SetPiece(prommotedPiece);
            builder.SetMoveMaker(transitionBoard.CurrentPlayer.PlayerAlliance);
            return builder.Build();
        }
    }
}