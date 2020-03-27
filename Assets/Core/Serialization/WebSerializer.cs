using UnityEngine;

using com.mortup.city.persistence;
using com.mortup.iso.world;

namespace com.mortup.iso.serialization {

    public class WebSerializer : MonoBehaviour, ILevelSerializer {

        [SerializeField] private bool forceCreateNewLevelOnLoad;

        LevelData ILevelSerializer.LoadLevel(string levelName) {
            if (forceCreateNewLevelOnLoad) {
                LevelData newLevelData = new LevelData(10, 10, levelName);
                newLevelData.owner = "1";
                newLevelData.id = 1;
                return newLevelData;
            }

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