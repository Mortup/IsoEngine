using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace com.mortup.city.multiplayer.chat {

    public class ChatInputField : MonoBehaviour {
        [SerializeField] private ChatInterface chatInterface;

        private TMP_InputField inputField;
        private bool wasFocused = false;

        private void Awake() {
            inputField = GetComponent<TMP_InputField>();
        }

        private void Update() {
            if (wasFocused && Input.GetKeyDown(KeyCode.Return)) {
                chatInterface.SendChatMessage(inputField.text);
                inputField.text = "";
                inputField.ActivateInputField();
            }

            wasFocused = inputField.isFocused;
        }
    }

}