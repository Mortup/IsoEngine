using UnityEngine;

using com.mortup.iso.persistence;

namespace com.mortup.iso.serialization {

    public class WebSerializer : MonoBehaviour, IRoomSerializer {

        LevelData IRoomSerializer.LoadLevel(string levelName) {
            string jsonResponse = PersistentAPI.GetRoom(levelName);
    
            SerializableLevelData webLevelData = JsonUtility.FromJson<SerializableLevelData>(jsonResponse);
            LevelData levelData = webLevelData.ToLevelData();

            return levelData;
        }

        void IRoomSerializer.SaveLevel(LevelData levelData) {
            SerializableLevelData webLevelData = new SerializableLevelData(levelData);
            PersistentAPI.SaveRoom(webLevelData);
            Debug.Log("Saving room");
        }
    }

}