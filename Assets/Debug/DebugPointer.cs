using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso;

public class DebugPointer : MonoBehaviour
{
    [SerializeField] private Level level;

    private void Update() {
        transform.position = level.transformer.TileToWorld(level.transformer.ScreenToTile(Input.mousePosition));
    }
}
