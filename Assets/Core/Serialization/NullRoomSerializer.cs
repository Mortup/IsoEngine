using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.serialization {
    public class NullRoomSerializer : MonoBehaviour, ILevelSerializer {

        public ILevelData LoadLevel(string levelName) {
            return new LevelData(10, 10);
        }

        public void SaveLevel(ILevelData levelData) {
            Debug.LogWarning("Saving data on a NullRoomSerializer won't do anything.");
        }

    }

}