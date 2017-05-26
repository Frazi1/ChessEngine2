namespace chessengine.board {
    public static class BoardUtils {
        public static readonly bool[] FirstColumn = InitColumn(0);
        public static readonly bool[] SecondColumn = InitColumn(1);
        public static readonly bool[] SeventhColumn = InitColumn(6);
        public static readonly bool[] EighthColumn = InitColumn(7);

        public static readonly int NumTiles = 64;
        public static readonly int NumTilesPerRow = 8;

        public static bool IsValidCoordinate(int coordinate) {
            return coordinate >= 0 && coordinate < 64;
        }

        private static bool[] InitColumn(int columnNumber) {
            bool[] column = new bool[64];

            do {
                column[columnNumber] = true;
                columnNumber += 8;
            } while (columnNumber < 64);

            return column;
        }
    }
}