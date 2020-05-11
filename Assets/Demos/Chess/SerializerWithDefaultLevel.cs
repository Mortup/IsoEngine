using UnityEngine;

using com.mortup.iso.serialization;
using com.mortup.iso.world;

namespace com.mortup.iso.demo.chess {
    
    public class SerializerWithDefaultLevel : MonoBehaviour, ILevelSerializer {
        private const int chessBoardSize = 8; // No te olvides que la clase carga un nivel default, no lo hace proceduralmente.

        public ILevelData LoadLevel(string levelName) {
            LevelData data = new LevelData(chessBoardSize, chessBoardSize);
            return data;
        }

        public void SaveLevel(ILevelData levelData) {
            throw new System.NotImplementedException();
        }

    }
}