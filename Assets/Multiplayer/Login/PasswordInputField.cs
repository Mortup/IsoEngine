using UnityEngine;
using UnityEngine.UI;

namespace com.mortup.city {

    public class PasswordInputField : MonoBehaviour {
        [SerializeField] private Launcher launcher;

        private InputField inputField;
        private bool wasFocused = false;

        private void Awake() {
            inputField = GetComponent<InputField>();
        }

        private void Update() {
            if (wasFocused && Input.GetKeyDown(KeyCode.Return)) {
                launcher.Connect();
            }

            wasFocused = inputField.isFocused;
        }
    }

}