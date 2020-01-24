using System.IO;
using System.Net;

using UnityEngine;
using com.mortup.iso.serialization;

namespace com.mortup.iso.persistence {

    public class PersistentAPI {
        public static readonly string baseUrl = "http://ec2-54-244-217-50.us-west-2.compute.amazonaws.com:8000/api/";

        private static string authToken = "NOTOKEN";

        public static void SetToken(string token) {
            authToken = "Token " + token;
        }

        public static string GetRoom(string roomId) {
            string endPoint = baseUrl + "room/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint + roomId + "/");
            request.Headers["Authorization"] = authToken;
            Debug.Log(authToken);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string jsonResponse;
            using (var reader = new StreamReader(response.GetResponseStream())) {
                jsonResponse = reader.ReadToEnd();
            }

            return jsonResponse;
        }

        public static bool SaveRoom(SerializableLevelData webLevelData) {
            string endPoint = baseUrl + "room/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint + webLevelData.id + '/');
            request.Headers["Authorization"] = authToken;
            request.ContentType = "application/json";
            request.Method = "PUT";

            using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                streamWriter.Write(JsonUtility.ToJson(webLevelData));
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode == HttpStatusCode.OK;
        }
    }

}