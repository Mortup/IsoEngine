﻿using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

namespace com.mortup.city {

    public class Launcher : MonoBehaviourPunCallbacks {

        [SerializeField] private string gameVersion = "0.1";

        [SerializeField] private InputField emailInput;
        [SerializeField] private InputField passwordInput;

        [SerializeField] private Button connectButton;
        [SerializeField] private Text progressLabel;

        public void Connect() {
            progressLabel.text = "Connecting...";
            connectButton.interactable = false;

            PhotonNetwork.GameVersion = gameVersion;

            AuthenticationValues authValues = new AuthenticationValues();

            authValues.AuthType = CustomAuthenticationType.Custom;

            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("email", emailInput.text);
            postData.Add("password", passwordInput.text);

            authValues.SetAuthPostData(postData);
            PhotonNetwork.AuthValues = authValues;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster() {
            progressLabel.text = "Successfully connected to server";

            SceneManager.LoadScene("Game");
        }

        public override void OnDisconnected(DisconnectCause cause) {
            connectButton.interactable = true;
            Debug.LogWarningFormat("OnDisconnected() with reason {0}", cause);
        }

        public override void OnCustomAuthenticationFailed(string debugMessage) {
            progressLabel.text = "Wrong email or password.";
        }
    }

}