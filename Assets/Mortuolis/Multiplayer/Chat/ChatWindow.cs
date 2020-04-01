using UnityEngine;
using TMPro;

namespace com.mortup.city.multiplayer.chat {
    public class ChatWindow : MonoBehaviour {
        [SerializeField] private int maxMessages;
        [SerializeField] private TMP_Text chatText;

        public void AppendMessage(string sender, string message) {
            string[] oldMessages = chatText.text.Split('\n');

            if (oldMessages.Length == maxMessages) {
                for (int i = 0; i < maxMessages - 1; i++) {
                    oldMessages[i] = oldMessages[i + 1];
                }
                string newMessage = string.Format("<b>{0}:</b> {1}", sender, message);
                oldMessages[oldMessages.Length - 1] = newMessage;
                chatText.text = string.Join("\n", oldMessages);
            }
            else {
                string newMessage = string.Format("<b>{0}:</b> {1}", sender, message);

                if (oldMessages.Length > 0) {
                    newMessage = "\n" + newMessage;
                }

                chatText.text = string.Join("\n", oldMessages) + newMessage;
            }
        }
    }
}