using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using com.mortup.iso.persistence;

namespace com.mortup.city {

    public class Launcher : MonoBehaviourPunCallbacks {

        [SerializeField] private string gameVersion = "0.1";

        [SerializeField] private InputField emailInput;
        [SerializeField] private InputField passwordInput;

        [SerializeField] private Button connectButton;
        [SerializeField] private Text progressLabel;

        [Header("Quick login settings")]
        [SerializeField] private bool connectOnStart;
        [SerializeField] private string defaultEmail;
        [SerializeField] private string defaultPassword;

        private void Start() {
            //Esto mata el launcher
            if (connectOnStart && PhotonNetwork.IsConnected == false) {
                Connect();
            }
        }

        public void Connect() {
            if (emailInput != null && passwordInput != null) {
                Connect(emailInput.text, passwordInput.text);
            }
            else {
                Connect(defaultEmail, defaultPassword);
            }
        }

        public void Connect(string email, string password) {
            ShowProgressMessage("Connecting...");
            SetConnectButtonState(false);

            PhotonNetwork.GameVersion = gameVersion;

            AuthenticationValues authValues = new AuthenticationValues();

            authValues.AuthType = CustomAuthenticationType.Custom;

            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("email", email);
            postData.Add("password", password);

            authValues.SetAuthPostData(postData);
            PhotonNetwork.AuthValues = authValues;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster() {
            ShowProgressMessage("Successfully connected to server");
            Debug.LogFormat("Welcome back {0}!", PhotonNetwork.NickName);

            if (SceneManager.GetActiveScene().name != "Game") {
                SceneManager.LoadScene("Game");
            }
        }

        public override void OnDisconnected(DisconnectCause cause) {
            SetConnectButtonState(true);
            Debug.LogWarningFormat("OnDisconnected() with reason {0}", cause);
        }

        public override void OnCustomAuthenticationResponse(Dictionary<string, object> data) {
            PersistentAPI.SetToken((string)data["token"]);
        }

        public override void OnCustomAuthenticationFailed(string debugMessage) {
            ShowProgressMessage("Wrong email or password.");
            ShowProgressMessage(string.Format("Authentication error: {0}", debugMessage));
            //TODO: Might also be that the server couldn't be reached.
        }

        public void ShowProgressMessage(string message) {
            if (progressLabel != null) {
                progressLabel.text = message;
            }
            else {
                Debug.LogFormat("Auto Login: {0}", message);
            }
        }

        public void SetConnectButtonState(bool state) {
            if (connectButton != null) {
                connectButton.interactable = state;
            }
        }
    }

}