using UnityEngine;

namespace com.mortup.iso.world {

    public class IsometricTransform : MonoBehaviour {

        [SerializeField] private Level level;
        [SerializeField] private Vector2Int coordinates;

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
        }

        public void UpdatePosition() {
            transform.position = level.transformer.TileToWorld(coords);

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null) {
                sr.sortingOrder = level.transformer.SortingOrder(coords.x, coords.y);
            }
        }

        private void OnValidate() {
            if (level != null) {
                UpdatePosition();
            }
        }
    }

}