using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    public static string roomName = "TheHouseOfMortup";

    public InputField inputField;

    public void OnClick() {
        roomName = inputField.text;
        SceneManager.LoadScene("Game");
    }
}
