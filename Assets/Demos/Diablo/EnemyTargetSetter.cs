using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.demo.diablo {

    public class EnemyTargetSetter : MonoBehaviour {

        [SerializeField] private Texture2D defaultCursor;
        [SerializeField] private Texture2D attackCursor;

        private void OnMouseDown() {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<JoystickPlayerMovement>().SetEnemyTarget(transform);
        }

        private void OnMouseOver() {
            Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);
        }

        private void OnMouseExit() {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }

    }

}