using UnityEngine;
using UnityEngine.UI;

namespace com.mortup.city.multiplayer.login {

    public class PasswordInputField : MonoBehaviour {
        [SerializeField] private Launcher launcher;

        private InputField inputField;
        private bool wasFocused = false;

        private void Awake() {
            inputField = GetComponent<InputField>();
        }

        private void Start() {
            if (PlayerPrefs.HasKey(PlayerEmailInputField.playerEmailKey)) {
                inputField.ActivateInputField();
            }
        }

        private void Update() {
            if (wasFocused && Input.GetKeyDown(KeyCode.Return)) {
                launcher.Connect();
            }

            wasFocused = inputField.isFocused;
        }
    }

}