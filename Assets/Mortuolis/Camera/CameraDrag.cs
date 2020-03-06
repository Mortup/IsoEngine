using UnityEngine;
using UnityEngine.EventSystems;

namespace com.mortup.city.camera {

    public class CameraDrag : MonoBehaviour {

        Camera cam;

        bool isDragging;
        Vector2 lastMousePosition;
        Vector3 dragStartPosition;

        void Awake() {
            cam = gameObject.GetComponent<Camera>();
        }

        private void LateUpdate() {
            if (isDragging) {
                Vector2 mouseDelta = lastMousePosition - (Vector2)Input.mousePosition;
                cam.transform.position = dragStartPosition + (Vector3)(mouseDelta * 2 * cam.orthographicSize / Screen.height);

                if (!Input.GetMouseButton(1)) {
                    isDragging = false;
                }
            }
            else {
                if (Input.GetMouseButtonDown(1) && !IsOverUI()) {
                    isDragging = true;
                    lastMousePosition = Input.mousePosition;
                    dragStartPosition = cam.transform.position;
                }
            }

        }

        private bool IsOverUI() {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }

    }

}