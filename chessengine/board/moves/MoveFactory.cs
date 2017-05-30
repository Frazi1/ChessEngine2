namespace chessengine.board.moves {
    public static class MoveFactory {
        public static Move CreateMove(Board board, int currentCoordinate, int destinationCoordinate) {
            foreach (Move move in board.GetAllLegalMoves()) {
                if (move.CurrentCoordinate == currentCoordinate &&
                    move.DestinationCoordinate == destinationCoordinate) {
                    return move;
                }
            }
            return Move.NullMove;
        }
    }
}