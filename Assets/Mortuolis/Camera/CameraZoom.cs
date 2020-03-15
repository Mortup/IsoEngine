using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace com.mortup.city.camera {

    public class CameraZoom : MonoBehaviour {

        [Range(0, 2)]
        [SerializeField] private int scaleFactor;

        private PixelPerfectCamera cam;
        Vector2Int initialResolution;

        private void Awake() {
            cam = GetComponent<PixelPerfectCamera>();
        }

        private void Start() {
            initialResolution = new Vector2Int(cam.refResolutionX, cam.refResolutionY);
            UpdateScale();
        }

        private void Update() {
            int scroll = Mathf.Clamp(Mathf.RoundToInt(Input.mouseScrollDelta.y), -1, 1);
            if (scroll == 0)
                return;

            scaleFactor = Mathf.Clamp(scaleFactor + scroll, 0, 2);
            UpdateScale();
        }

        private void UpdateScale() {
            int divider = Mathf.RoundToInt(Mathf.Pow(2, scaleFactor));
            Vector2Int newResolution = new Vector2Int(initialResolution.x / divider, initialResolution.y / divider);
            cam.refResolutionX = newResolution.x;
            cam.refResolutionY = newResolution.y;
        }
    }

}