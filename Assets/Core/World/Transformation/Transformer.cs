using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso {

    public class Transformer {

        private Level level;
        private Transform levelTransform;
        private Orientation orientation;

        public Transformer(Level level) {
            this.level = level;
            levelTransform = level.gameObject.transform;
            orientation = Orientation.NORTH;
        }

        public Vector2 TileToLocal(int tileX, int tileY) {
            Vector2Int rotated = RotateTile(new Vector2Int(tileX, tileY));

            float x = (rotated.x + rotated.y) * 0.5f;
            float y = (rotated.y - rotated.x) * 0.25f;
            return new Vector2(x, y);
        }

        public Vector2 TileToLocal(Vector2Int tileCoords) {
            return TileToLocal(tileCoords.x, tileCoords.y);
        }

        public Vector2 TileToWorld(int tileX, int tileY) {
            Vector2 local = TileToLocal(tileX, tileY);
            return levelTransform.TransformPoint(local);
        }

        public Vector2 TileToWorld(Vector2Int tileCoords) {
            return TileToWorld(tileCoords.x, tileCoords.y);
        }

        public Vector2Int ScreenToTile(Vector2 screenCoords) {
            Vector2 world = Camera.main.ScreenToWorldPoint(screenCoords);
            return WorldToTile(world);
        }

        public Vector2Int WorldToTile(Vector2 world) {
            Vector2 local = levelTransform.InverseTransformPoint(world);
            return LocalToTile(local);
        }

        public Vector2Int LocalToTile(Vector2 local) {
            local += new Vector2(-0.5f, 0f);

            int x = Mathf.RoundToInt(local.x - 2 * local.y);
            int y = Mathf.RoundToInt(local.x + 2 * local.y);

            return InverseRotateTile(new Vector2Int(x, y));
        }

        public Vector2 MouseTileRounded() {
            return TileToWorld(ScreenToTile(Input.mousePosition));
        }

        // Sorting Order
        public int SortingOrder(int x, int y) {
            Vector2 localPos = level.transformer.TileToLocal(x, y);
            return Mathf.RoundToInt(localPos.y * 1000 + localPos.x * 10) * -1;
        }

        // Rotation Specific
        public enum Orientation {
            NORTH = 0,
            EAST = 1,
            SOUTH = 2,
            WEST = 3,
        }

        public void SetOrientation(Orientation newOrientation) {
            Orientation oldOrientation = orientation;
            orientation = newOrientation;

            if (oldOrientation != orientation) {
                IsometricTransform[] transforms = GameObject.FindObjectsOfType<IsometricTransform>();
                foreach (IsometricTransform t in transforms) {
                    if (t.GetLevel() == level) {
                        t.UpdatePosition();
                    }
                }
            }
        }

        public void RotateClockwise(int times=1) {
            SetOrientation((Orientation)(((int)orientation + times) % 4));
        }

        public Vector2Int RotateTile(Vector2Int original) {
            switch(orientation) {
                case Orientation.NORTH:
                    return original;
                case Orientation.SOUTH:
                    return original * -1;
                case Orientation.EAST:
                    return new Vector2Int(original.y * -1, original.x);
                case Orientation.WEST:
                    return new Vector2Int(original.y, original.x * -1);
                default:
                    Debug.LogError("Unknown orientation.");
                    return Vector2Int.zero;
            }
        }

        public Vector2Int InverseRotateTile(Vector2Int original) {
            switch (orientation) {
                case Orientation.NORTH:
                    return original;
                case Orientation.SOUTH:
                    return original * -1;
                case Orientation.WEST:
                    return new Vector2Int(original.y * -1, original.x);
                case Orientation.EAST:
                    return new Vector2Int(original.y, original.x * -1);
                default:
                    Debug.LogError("Unknown orientation.");
                    return Vector2Int.zero;
            }
        }
    }

}