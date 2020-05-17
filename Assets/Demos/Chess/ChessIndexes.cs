namespace com.mortup.iso.demo.chess {

    // I hope to be able to delete this and store data on prefabs later.
    public enum ChessIndex {
        EMPTY = 0,
        BLACK_KING = 1,
        BLACK_QUEEN = 2,
        BLACK_BISHOP = 3,
        BLACK_KNIGHT = 4,
        BLACK_TOWER = 5,
        BLACK_PAWN = 6,
        WHITE_KING = 7,
        WHITE_QUEEN = 8,
        WHITE_BISHOP = 9,
        WHITE_KNIGHT = 10,
        WHITE_TOWER = 11,
        WHITE_PAWN = 12
    }

    static class ChessIndexMethods {
        public static bool IsEmpty(this ChessIndex ci) {
            return ci == ChessIndex.EMPTY;
        }

        public static bool IsWhite(this ChessIndex ci) {
            return (int)ci > 6;
        }

        public static bool IsBlack(this ChessIndex ci) {
            return (int)ci < 7 && (int)ci != 0;
        }

        public static bool IsSameColor(this ChessIndex ci1, ChessIndex ci2) {
            if (ci1.IsBlack() && ci2.IsBlack())
                return true;

            if (ci1.IsWhite() && ci2.IsWhite())
                return true;

            if (ci1.IsEmpty() && ci2.IsEmpty())
                return true;

            return false;
        }
    }
}