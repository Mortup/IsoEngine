using UnityEngine;

namespace com.mortup.iso.world {

    public class IsometricTransform : MonoBehaviour {

        [SerializeField] private Level level;
        [SerializeField] private Vector2Int coordinates;

        private bool initialized = false;

        public Vector2Int coords {
            get {
                return coordinates;
            }
            set {
                coordinates = value;
                UpdatePosition();
            }
        }


        public void Init(Level level) {
            this.level = level;
            initialized = true;
        }

        public void UpdatePosition() {
            transform.localPosition = level.transformer.TileToLocal(coords);
        }

        private void OnValidate() {
            if (initialized) {
                UpdatePosition();
            }
        }

    }

}