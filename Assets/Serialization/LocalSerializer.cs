using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LocalSerializer : IRoomSerializer {

    private const string basePath = "Saves";
    private const string fileExtension = ".bin";

    LevelData IRoomSerializer.LoadLevel(string levelName) {
        if (FileExists(levelName) == false) {
            UnityEngine.Debug.Log(string.Format("Level {0} could not be found. Creating a default one.", levelName));
            return new LevelData();
        }

        throw new System.NotImplementedException();
    }

    void IRoomSerializer.SaveLevel(LevelData levelData) {
        UnityEngine.Debug.Log("Saving level " + levelData.name + "...");

        SerializableLevelData serializableData = new SerializableLevelData(levelData);

        if (!Directory.Exists(basePath)) {
            Directory.CreateDirectory(basePath);
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
