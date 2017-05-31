namespace chessengine {
    public static class Alliance {
        public enum AllianceEnum {
            Black,
            White
        }

        public static int GetDirection(AllianceEnum alliance) {
            return alliance == AllianceEnum.Black ? 1 : -1;
        }

        public static int GetOppositeDirection(AllianceEnum alliance) {
            return GetDirection(alliance) * -1;
        }
    }
}