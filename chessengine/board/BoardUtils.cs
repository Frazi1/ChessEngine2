namespace chessengine.board {
    public static class BoardUtils {
        public static readonly bool[] FirstColumn = InitColumn(0);
        public static readonly bool[] SecondColumn = InitColumn(1);
        public static readonly bool[] ThirdColumn = InitColumn(2);
        public static readonly bool[] FourthColumn = InitColumn(3);
        public static readonly bool[] FifthColumn = InitColumn(4);
        public static readonly bool[] SixthColumn = InitColumn(5);
        public static readonly bool[] SeventhColumn = InitColumn(6);
        public static readonly bool[] EighthColumn = InitColumn(7);

        public static readonly bool[] FirstRank = InitRank(0);
        public static readonly bool[] SecondRank = InitRank(1);
        public static readonly bool[] ThirdRank = InitRank(2);
        public static readonly bool[] FourthRank = InitRank(3);
        public static readonly bool[] FifthRank = InitRank(4);
        public static readonly bool[] SixthRank = InitRank(5);
        public static readonly bool[] SeventhRank = InitRank(6);
        public static readonly bool[] EigthRank = InitRank(7);


        public const int NumTiles = 64;
        public const int NumTilesPerRow = 8;

        public static bool IsValidCoordinate(int coordinate) {
            return coordinate >= 0 && coordinate < NumTiles;
        }

        private static bool[] InitColumn(int columnNumber) {
            bool[] column = new bool[NumTiles];

            do {
                column[columnNumber] = true;
                columnNumber += NumTilesPerRow;
            } while (columnNumber < NumTiles);

            return column;
        }

        private static bool[] InitRank(int rowNumber) {
            bool[] rank = new bool[NumTiles];
            for (int i = 0; i < NumTilesPerRow; i++) {
                rank[NumTilesPerRow * rowNumber + i] = true;
            }
            return rank;
        }
    }
}