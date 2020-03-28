using System.Collections.Generic;

using UnityEngine;

namespace com.mortup.iso.world {

    public class IsometricTransform : MonoBehaviour {

        private static List<IsometricTransform> instances;

        public static List<IsometricTransform> GetInstances() { return instances; }

        [SerializeField] private Level level;
        [SerializeField] private Vector3Int coordinates;
        [SerializeField] private ElementType elementType;

        private SpriteRenderer spriteRenderer;

        public Vector3Int coords {
            get {
                return coordinates;
            }
            set {
                coordinates = value;
                UpdatePosition();
            }
        }

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Level level, ElementType elementType) {
            if (instances == null) {
                instances = new List<IsometricTransform>();
            }
            instances.Add(this);

            this.level = level;
            this.elementType = elementType;
            UpdatePosition();
        }

        public Level GetLevel() {
            return level;
        }

        public void UpdatePosition() {
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

            if (spriteRenderer != null) {
                spriteRenderer.sortingOrder = sortingOrder;
            }
        }

        private void OnValidate() {
            if (level != null) {
                UpdatePosition();
            }
        }

        private void OnDestroy() {
            if (instances != null) {
                instances.Remove(this);
            }
        }

        public enum ElementType {
            Tile,
            Wall,
            Item
        }
    }

}