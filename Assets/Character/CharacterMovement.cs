using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Level level;

    [SerializeField] private float speed;

    private Vector2 target;

    private void Start() {
        target = transform.position;
    }

    private void Update() {
        
        if ((target - (Vector2)transform.position).magnitude > 0.05f) {
            transform.position = transform.position + (Vector3)(target - (Vector2)transform.position).normalized * Time.deltaTime * speed;
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector2Int targetTile = level.transformer.ScreenToTile(Input.mousePosition);
            target = level.transformer.TileToWorld(targetTile);
        }
    }
}
