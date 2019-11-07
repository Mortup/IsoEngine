using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEditor : MonoBehaviour
{
    [SerializeField] private Level level;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Changing");
            Vector2Int coords = level.transformer.ScreenToTile(Input.mousePosition);
            level.data.SetFloor(coords.x, coords.y, 1);
        }
    }
}
