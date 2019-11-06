using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void GoToMapLoader() {
        SceneManager.LoadScene("MapSelection");
    }
}
