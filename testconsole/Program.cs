using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using chessengine.board;
using System.Threading.Tasks;
using chessengine.board.moves;
using chessengine.player;

namespace testconsole {
    class Program {
        static void Main(string[] args) {
            Board board1 = Board.CreateStandardBoard();
            Move move = board1.CurrentPlayer.LegalMoves.ElementAt(0);
            MoveTransition moveTransition = board1.CurrentPlayer.MakeMove(move);
            Board board2 = moveTransition.TransitionBoard;

            bool equal = board1.GetTile(5).Piece.Equals(board2.GetTile(6).Piece);
            Console.WriteLine(equal);


            Console.ReadKey();
        }
    }
}
