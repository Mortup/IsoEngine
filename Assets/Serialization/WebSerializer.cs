using System.IO;

using UnityEngine;

using com.mortup.city.persistence;

namespace com.mortup.city.serialization {

    public class WebSerializer : IRoomSerializer {

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