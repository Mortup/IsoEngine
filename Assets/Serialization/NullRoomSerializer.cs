using UnityEngine;

namespace com.mortup.iso.serialization {
    public class NullRoomSerializer : MonoBehaviour, ILevelSerializer {

        public LevelData LoadLevel(string levelName) {
            return new LevelData(10, 10);
        }

        public void SaveLevel(LevelData levelData) {
            Debug.LogWarning("Saving data on a NullRoomSerializer won't do anything.");
        }

    }

}