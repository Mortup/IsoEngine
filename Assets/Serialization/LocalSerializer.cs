using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

namespace com.mortup.iso.serialization {
    public class LocalSerializer : MonoBehaviour, IRoomSerializer {

        private const string basePath = "Saves";
        private const string fileExtension = ".bin";

        LevelData IRoomSerializer.LoadLevel(string levelName) {
            if (FileExists(levelName) == false) {
                Debug.LogError(string.Format("Level {0} could not be found. Creating a default one.", levelName));
                LevelData levelData = new LevelData(10, 10);
                levelData.name = levelName;

                return levelData;
            }

            FileStream saveFile = File.Open(FullSavePath(levelName), FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            SerializableLevelData serializableData = (SerializableLevelData)formatter.Deserialize(saveFile);

            return serializableData.ToLevelData();
        }

        void IRoomSerializer.SaveLevel(LevelData levelData) {
            Debug.Log("Saving level " + levelData.name + "...");

            SerializableLevelData serializableData = new SerializableLevelData(levelData);

            if (!Directory.Exists(basePath)) {
                Directory.CreateDirectory(basePath);
                Debug.Log("Creating saving directory...");
            }

            FileStream saveFile = File.Create(FullSavePath(levelData.name));
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(saveFile, serializableData);
            saveFile.Close();
        }

        private bool FileExists(string fileName) {
            return File.Exists(FullSavePath(fileName));
        }

        private string FullSavePath(string fileName) {
            return Path.Combine(basePath, fileName + fileExtension);
        }
    }
}