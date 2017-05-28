using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.board;

namespace testconsole {
    class Program {
        static void Main(string[] args) {
            Board board = Board.CreateStandardBoard();

            Console.Write(board.ToString());

            Console.ReadKey();
        }
    }
}
