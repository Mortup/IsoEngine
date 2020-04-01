using UnityEngine;

using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;

namespace com.mortup.city.multiplayer.chat {

    public class ChatInterface : MonoBehaviourPunCallbacks, IChatClientListener {

        [SerializeField] private ChatWindow chatWindow;

        private const string chatVersion = "v0.1";

        private ChatClient chatClient;

        public void DebugReturn(DebugLevel level, string message) {
            Debug.LogFormat("Debug Return: {0}", message);
        }

        public void OnChatStateChange(ChatState state) {
            if (state == ChatState.ConnectedToFrontEnd) {
                OnConnectedToFrontEnd();
            }

            Debug.Log("New state: " + state.ToString());
        }

        public void OnDisconnected() {
            Debug.Log("OnDisconnected from chat.");
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages) {
            for (int i = 0; i < senders.Length; i++) {
                chatWindow.AppendMessage(senders[i], messages[i].ToString());
            }
        }

        public void OnPrivateMessage(string sender, object message, string channelName) {
            throw new System.NotImplementedException();
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message) {
            throw new System.NotImplementedException();
        }

        public void OnSubscribed(string[] channels, bool[] results) {
            for (int i = 0; i < channels.Length; i++) {
                Debug.LogFormat("Subscribed to channel {0}", channels[i]);
            }
        }

        public void OnUnsubscribed(string[] channels) {
            throw new System.NotImplementedException();
        }

        public void OnUserSubscribed(string channel, string user) {
            throw new System.NotImplementedException();
        }

        public void OnUserUnsubscribed(string channel, string user) {
            throw new System.NotImplementedException();
        }

        public override void OnConnectedToMaster() {
            Debug.Log("Connecting to chat...");
            base.OnConnectedToMaster();

            chatClient = new ChatClient(this);
            chatClient.ChatRegion = "US";
            chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, chatVersion, new AuthenticationValues(PhotonNetwork.NickName));
        }

        void Update() {
            if (chatClient != null) {
                chatClient.Service();
            }
        }

        public void SendChatMessage(string message) {
            chatClient.PublishMessage("Global", message);
        }

        private void OnConnectedToFrontEnd() {
            chatClient.Subscribe(new string[] { "Global", "Despacito" });
        }
    }

}