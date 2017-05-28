namespace chessengine {
    public static class Alliance {
        public enum AllianceEnum {
            Black,
            White
        }

        public static int GetDirection(AllianceEnum alliance) {
            return alliance == AllianceEnum.Black ? 1 : -1;
        }

        public static bool IsWhite(AllianceEnum alliance) {
            return alliance == AllianceEnum.White;
        }

        public static bool IsBlack(AllianceEnum alliance) {
            return alliance == AllianceEnum.Black;
        }
    }
}