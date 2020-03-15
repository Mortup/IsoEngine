using UnityEngine;
using UnityEngine.UI;

public class ModeHighLighter : MonoBehaviour
{
    [SerializeField] private CursorMode cursorMode;
    [SerializeField] private int modeToHighlight;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Update() {
        image.color = cursorMode.GetCurrentMode() == modeToHighlight ? Color.white : Color.clear;
    }
}
