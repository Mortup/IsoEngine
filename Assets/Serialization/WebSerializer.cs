using System.IO;
using System.Net;
using UnityEngine;

public class WebSerializer : IRoomSerializer {

    private const string baseUrl = "http://ec2-54-202-20-52.us-west-2.compute.amazonaws.com:8000/api/room/";

    LevelData IRoomSerializer.LoadLevel(string levelName) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + levelName);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        string jsonResponse;
        using (var reader = new StreamReader(response.GetResponseStream())) {
            jsonResponse = reader.ReadToEnd();
        }

        SerializableLevelData webLevelData = JsonUtility.FromJson<SerializableLevelData>(jsonResponse);
        LevelData levelData = webLevelData.ToLevelData();

        return levelData;
    }

    void IRoomSerializer.SaveLevel(LevelData levelData) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + levelData.id + '/');
        request.ContentType = "application/json";
        request.Method = "PUT";

        SerializableLevelData webLevelData = new SerializableLevelData(levelData);

        using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
            streamWriter.Write(JsonUtility.ToJson(webLevelData));
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Debug.Log("Save response: " + response.StatusCode.ToString());
    }
}