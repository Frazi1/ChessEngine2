using chessengine.board;
using chessengine.board.moves;

namespace chessengine.player.AI {
    public interface IStrategy {
        Move SelectMove(Board board, Alliance.AllianceEnum alliance);
    }
}