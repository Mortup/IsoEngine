using System.IO;
using System.Net;
using UnityEngine;

public class WebSerializer : IRoomSerializer {

    private const string baseUrl = "http://ec2-52-40-188-170.us-west-2.compute.amazonaws.com:8000/api/room/";

        LevelData IRoomSerializer.LoadLevel(string levelName) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + levelName);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        WebLevelData webLevelData = JsonUtility.FromJson<WebLevelData>(jsonResponse);
        LevelData levelData = webLevelData.ToLevelData();

        return levelData;
    }
}