using UnityEngine;

using com.mortup.iso.serialization;
using com.mortup.iso.world;

namespace com.mortup.iso.demo.chess {
    
    public class SerializerWithDefaultLevel : MonoBehaviour, ILevelSerializer {
        private const int chessBoardSize = 8; // No te olvides que la clase carga un nivel default, no lo hace proceduralmente.

        public ILevelData LoadLevel(string levelName) {
            LevelData data = new LevelData(chessBoardSize, chessBoardSize);
            data.SetItem(0, 0, (int)ChessIndex.BLACK_TOWER, 0);
            data.SetItem(1, 0, (int)ChessIndex.BLACK_KNIGHT, 0);
            data.SetItem(2, 0, (int)ChessIndex.BLACK_BISHOP, 0);
            data.SetItem(3, 0, (int)ChessIndex.BLACK_KING, 0);
            data.SetItem(4, 0, (int)ChessIndex.BLACK_QUEEN, 0);
            data.SetItem(5, 0, (int)ChessIndex.BLACK_BISHOP, 0);
            data.SetItem(6, 0, (int)ChessIndex.BLACK_KNIGHT, 0);
            data.SetItem(7, 0, (int)ChessIndex.BLACK_TOWER, 0);
            data.SetItem(0, 1, (int)ChessIndex.BLACK_PAWN, 0);
            data.SetItem(1, 1, (int)ChessIndex.BLACK_PAWN, 0);
            data.SetItem(2, 1, (int)ChessIndex.BLACK_PAWN, 0);
            data.SetItem(3, 1, (int)ChessIndex.BLACK_PAWN, 0);
            data.SetItem(4, 1, (int)ChessIndex.BLACK_PAWN, 0);
            data.SetItem(5, 1, (int)ChessIndex.BLACK_PAWN, 0);
            data.SetItem(6, 1, (int)ChessIndex.BLACK_PAWN, 0);
            data.SetItem(7, 1, (int)ChessIndex.BLACK_PAWN, 0);

            data.SetItem(0, 7, (int)ChessIndex.WHITE_TOWER, 0);
            data.SetItem(1, 7, (int)ChessIndex.WHITE_KNIGHT, 0);
            data.SetItem(2, 7, (int)ChessIndex.WHITE_BISHOP, 0);
            data.SetItem(3, 7, (int)ChessIndex.WHITE_KING, 0);
            data.SetItem(4, 7, (int)ChessIndex.WHITE_QUEEN, 0);
            data.SetItem(5, 7, (int)ChessIndex.WHITE_BISHOP, 0);
            data.SetItem(6, 7, (int)ChessIndex.WHITE_KNIGHT, 0);
            data.SetItem(7, 7, (int)ChessIndex.WHITE_TOWER, 0);
            data.SetItem(0, 6, (int)ChessIndex.WHITE_PAWN, 0);
            data.SetItem(1, 6, (int)ChessIndex.WHITE_PAWN, 0);
            data.SetItem(2, 6, (int)ChessIndex.WHITE_PAWN, 0);
            data.SetItem(3, 6, (int)ChessIndex.WHITE_PAWN, 0);
            data.SetItem(4, 6, (int)ChessIndex.WHITE_PAWN, 0);
            data.SetItem(5, 6, (int)ChessIndex.WHITE_PAWN, 0);
            data.SetItem(6, 6, (int)ChessIndex.WHITE_PAWN, 0);
            data.SetItem(7, 6, (int)ChessIndex.WHITE_PAWN, 0);

            for (int x = 0; x < chessBoardSize; x++) {
                for (int y = 0; y < chessBoardSize; y++) {
                    data.SetFloor(x, y, (x + y) % 2 == 0 ? 0 : 1);
                }
            }

            return data;
        }

        public void SaveLevel(ILevelData levelData) {
            throw new System.NotImplementedException();
        }

    }
}