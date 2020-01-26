using System.IO;
using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.serialization.tiled {

    public class TiledSerializer : MonoBehaviour, ILevelSerializer {

        private string basePath = "Saves"; // TODO: Path manager should take care of this.
        private string fileExtension = ".json";

        // TODO: To save we should create a dictionary of leveldata:tiledjsonleveldata

        public LevelData LoadLevel(string levelName) {
            TextAsset levelJson = Resources.Load<TextAsset>(Path.Combine(basePath, levelName));
            if (levelJson == null) {
                Debug.LogError(string.Format("Level {0} could not be found. Creating a default one.", levelName));
                LevelData levelData = new LevelData(10, 10);
                levelData.name = levelName;

                return levelData;
            }

            TiledJsonLevelData tiledData = JsonUtility.FromJson<TiledJsonLevelData>(levelJson.text);
            return tiledData.ToLevelData();
        }

        public void SaveLevel(LevelData levelData) {
            throw new System.NotImplementedException("Tiled Serializer does not support saving yet.");
        }
    }

}