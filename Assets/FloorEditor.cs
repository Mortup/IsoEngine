using UnityEngine;

public class FloorEditor : MonoBehaviour
{
    [SerializeField] private Level level;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2Int coords = level.transformer.ScreenToTile(Input.mousePosition);
            level.data.SetFloor(coords.x, coords.y, 1);
        }
    }
}
