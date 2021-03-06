﻿using System.Threading.Tasks;
using chessengine.board;
using chessengine.board.moves;

namespace chessengine.player.AI {
    public interface IStrategy {
        Move SelectMove(Board board, Player player);
        Move SelectMoveParallel(Board board, Player player);
    }
}