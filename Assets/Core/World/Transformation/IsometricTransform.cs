using UnityEngine;

namespace com.mortup.iso.world {

    public class IsometricTransform : MonoBehaviour {

        [SerializeField] private Level level;
        [SerializeField] private Vector3Int coordinates;
        [SerializeField] private ElementType elementType;

        public Vector3Int coords {
            get {
                return coordinates;
            }
            set {
                coordinates = value;
                UpdatePosition();
            }
        }

        public void Init(Level level, ElementType elementType) {
            this.level = level;
            this.elementType = elementType;
            UpdatePosition();
        }

        public Level GetLevel() {
            return level;
        }

        public void UpdatePosition() {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            int sortingOrder = -1;

            switch (elementType) {
                case ElementType.Tile:
                    transform.position = level.transformer.TileToWorld(new Vector2Int(coords.x, coords.y));
                    sortingOrder = level.transformer.TileSortingOrder(coords.x, coords.y);
                    break;
                case ElementType.Wall:
                    transform.position = level.transformer.WallToWorld(coords);
                    sortingOrder = level.transformer.WallSortingOrder(coords.x, coords.y, coords.z);
                    break;
                case ElementType.Item:
                    transform.position = level.transformer.TileToWorld(new Vector2Int(coords.x, coords.y));
                    sortingOrder = level.transformer.ItemSortingOrder(coords.x, coords.y);
                    break;
                default:
                    Debug.LogError("Unknown element type.");
                    break;
            }

            if (sr != null) {
                sr.sortingOrder = sortingOrder;
            }
        }

        private void OnValidate() {
            if (level != null) {
                UpdatePosition();
            }
        }

        public enum ElementType {
            Tile,
            Wall,
            Item
        }
    }

}