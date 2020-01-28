using UnityEngine;

namespace com.mortup.iso.world {

    public class IsometricTransform : MonoBehaviour {

        [SerializeField] private Level level;

        public Vector2Int coords {
            get {
                return _coords;
            }
            set {
                _coords = value;
                UpdatePosition();
            }
        }

        private Vector2Int _coords;

        public void Init(Level level) {
            this.level = level;
        }

        public void UpdatePosition() {
            transform.localPosition = level.transformer.TileToLocal(coords);
        }

    }

}