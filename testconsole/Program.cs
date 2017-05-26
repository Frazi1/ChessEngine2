using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using chessengine.board;

namespace testconsole {
    class Program {
        static void Main(string[] args) {
           Tile t = Tile.CreateTile(10,null);
            var d = Tile.CreateAllPossibleEmptyTiles();
            Console.ReadLine();
        }
    }
}
