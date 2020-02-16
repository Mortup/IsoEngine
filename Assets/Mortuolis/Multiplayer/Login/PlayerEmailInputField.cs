using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace com.mortup.city.multiplayer.login {

    [RequireComponent (typeof(InputField)) ]
    public class PlayerEmailInputField : MonoBehaviour {

        [SerializeField] private InputField passwordField;

        public const string playerEmailKey = "PlayerEmail";
        private InputField inputField;

        private void Awake() {
            inputField = GetComponent<InputField>();
        }

        private void Start() {
            string defaultEmail = string.Empty;

            if (PlayerPrefs.HasKey(playerEmailKey)) {
                defaultEmail = PlayerPrefs.GetString(playerEmailKey);
                inputField.text = defaultEmail;
            }
        }

        private void Update() {
            if (inputField.isFocused && Input.GetKeyDown(KeyCode.Tab)) {
                passwordField.ActivateInputField();
            }
        }

        public void SetPlayerEmail(string value) {
            if (string.IsNullOrEmpty(value)) {
                Debug.LogError("Player Email is null or empty");
                return;
            }

            PlayerPrefs.SetString(playerEmailKey, value);
        }

    }

}