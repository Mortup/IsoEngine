using UnityEngine;

using com.mortup.city.persistence;
using com.mortup.iso.world;

namespace com.mortup.iso.serialization {

    public class WebSerializer : MonoBehaviour, ILevelSerializer {

        LevelData ILevelSerializer.LoadLevel(string levelName) {
            string jsonResponse = PersistentAPI.GetRoom(levelName);
    
            SerializableLevelData webLevelData = JsonUtility.FromJson<SerializableLevelData>(jsonResponse);
            LevelData levelData = webLevelData.ToLevelData();

            return levelData;
        }

        void ILevelSerializer.SaveLevel(LevelData levelData) {
            SerializableLevelData webLevelData = new SerializableLevelData(levelData);
            PersistentAPI.SaveRoom(webLevelData);
            Debug.Log("Saving room");
        }
    }

}