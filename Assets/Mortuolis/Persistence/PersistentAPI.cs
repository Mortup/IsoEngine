using System.IO;
using System.Net;

using UnityEngine;
using com.mortup.iso.serialization;

namespace com.mortup.city.persistence {

    public class PersistentAPI {
        public static readonly string baseUrl = "http://54.149.82.204:8000/api/";

        private static string authToken = "NOTOKEN";

        public static void SetToken(string token) {
            authToken = "Token " + token;
        }

        public static bool PingServer() {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + "ping/");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();


                return response.StatusCode == HttpStatusCode.OK;
            }
            catch {
                return false;
            }
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