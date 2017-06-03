using chessengine.board;

namespace chessengine.player.AI {
    public  interface IBoardEvaluator {
        int Evaluate(Board board, Player player);
    }
}