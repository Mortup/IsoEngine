using UnityEngine;

using com.mortup.iso.observers;
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

        // Tile Position
        private Vector2 TileToLocalNoRotation(int tileX, int tileY) {
            float x = (tileX + tileY) * 0.5f;
            float y = (tileY - tileX) * 0.25f;
            return new Vector2(x + 0.01f, y);
        }

        public Vector2 TileToLocal(int tileX, int tileY) {
            Vector2Int rotated = RotateTile(new Vector2Int(tileX, tileY));
            return TileToLocalNoRotation(rotated.x, rotated.y);
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

        private Vector2Int LocalToTileNoRotation(Vector2 local) {
            local += new Vector2(-0.5f, 0f);

            int x = Mathf.RoundToInt(local.x - 2 * local.y);
            int y = Mathf.RoundToInt(local.x + 2 * local.y);

            return new Vector2Int(x, y);
        }

        public Vector2Int LocalToTile(Vector2 local) {
            return InverseRotateTile(LocalToTileNoRotation(local));
        }

        public Vector2 MouseTileRounded() {
            return TileToWorld(ScreenToTile(Input.mousePosition));
        }

        // Wall Positions
        public Vector2 WallToLocal(int x, int y, int z) {
            Vector3Int rotatedCoords = RotateWall(new Vector3Int(x, y - 1, z));
            int rotatedX = rotatedCoords.x;
            int rotatedY = rotatedCoords.y;
            int rotatedZ = rotatedCoords.z;

            Vector2 local = TileToLocalNoRotation(rotatedX, rotatedY);
            Vector2 offset = Vector2.zero;

            if (rotatedZ == 1)
                offset += new Vector2(0.5f, 0f);

            return local + offset;
        }

        public Vector2 WallToLocal(Vector3Int wallPos) {
            return WallToLocal(wallPos.x, wallPos.y, wallPos.z);
        }

        public Vector2 WallToWorld(int x, int y, int z) {
            Vector2 local = WallToLocal(x, y, z);
            return levelTransform.TransformPoint(local);
        }

        public Vector2 WallToWorld(Vector3Int wallCoords) {
            return WallToWorld(wallCoords.x, wallCoords.y, wallCoords.z);
        }

        public Vector3Int LocalToWall(Vector2 local) {
            int gridX = Mathf.FloorToInt(local.x / 0.5f);
            int gridY = -1 * Mathf.FloorToInt(local.y / 0.25f);

            int x = Mathf.FloorToInt((gridY / 2f) + (gridX / 2f));
            int y = Mathf.FloorToInt((gridX / 2f) - (gridY / 2f));
            int z = Mathf.Abs(Mathf.Abs(gridY % 2) - Mathf.Abs(gridX % 2));

            Vector3Int unrotated = InverseRotateWall(new Vector3Int(x, y, z));
            return new Vector3Int(unrotated.x, unrotated.y + 1, unrotated.z);
        }

        public Vector3Int WorldToWall(Vector2 world) {
            Vector2 local = levelTransform.InverseTransformPoint(world);
            return LocalToWall(local);
        }

        public Vector3Int ScreenToWall(Vector2 screenPosition) {
            Vector2 world = Camera.main.ScreenToWorldPoint(screenPosition);
            return WorldToWall(world);
        }

        public Vector2 ScreenWallRounded(Vector2 screenPosition) {
            return WallToWorld(ScreenToWall(screenPosition));
        }

        // Vertex Positions
        public Vector2 VertexToLocal(int x, int y) {
            Vector2Int orientationOffset = Vector2Int.zero;
            switch (GetOrientation()) {
                case Orientation.NORTH:
                    break;
                case Orientation.EAST:
                    orientationOffset = new Vector2Int(0, -1);
                    break;
                case Orientation.SOUTH:
                    orientationOffset = new Vector2Int(-1, -1);
                    break;
                case Orientation.WEST:
                    orientationOffset = new Vector2Int(-1, 0);
                    break;
            }

            return TileToLocal(x + orientationOffset.x, y + orientationOffset.y);
        }

        public Vector2 VertexToLocal(Vector2Int vertex) {
            return VertexToLocal(vertex.x, vertex.y);
        }

        public Vector2 VertexToWorld(Vector2Int vertex) {
            Vector2 local = VertexToLocal(vertex);
            return levelTransform.TransformPoint(local);
        }

        public Vector2 VertexToWorld(int x, int y) {
            return VertexToWorld(new Vector2Int(x, y));
        }

        public Vector2Int LocalToVertex(Vector2 local) {
            Vector2Int tile = LocalToTile(local + Vector2.right * 0.5f);
            Vector2Int orientationOffset = Vector2Int.zero;

            switch (GetOrientation()) {
                case Orientation.NORTH:
                    break;
                case Orientation.EAST:
                    orientationOffset = new Vector2Int(0, 1);
                    break;
                case Orientation.SOUTH:
                    orientationOffset = new Vector2Int(1, 1);
                    break;
                case Orientation.WEST:
                    orientationOffset = new Vector2Int(1,0);
                    break;
            }

            return tile + orientationOffset;
        }

        public Vector2Int WorldToVertex(Vector2 world) {
            Vector2 local = levelTransform.InverseTransformPoint(world);
            return LocalToVertex(local);
        }

        public Vector2Int ScreenToVertex(Vector2 screenPosition) {
            Vector2 world = Camera.main.ScreenToWorldPoint(screenPosition);
            return WorldToVertex(world);
        }

        public Vector2 ScreenVertexRounded(Vector2 screenPosition) {
            return VertexToWorld(ScreenToVertex(screenPosition));
        }

        // Sorting Order
        public int TileSortingOrder(int x, int y) {
            Vector2 localPos = level.transformer.TileToLocal(x, y);
            return Mathf.RoundToInt(localPos.y * 1000 + localPos.x * 10) * -1;
        }

        public int WallSortingOrder(int x, int y, int z) {
            Vector2 localPos = level.transformer.WallToLocal(x, y, z);
            return Mathf.RoundToInt(localPos.y * 1000 + localPos.x * 10) * -1;
        }

        public int ItemSortingOrder(int x, int y) {
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
                // All IsometricTransforms should be updated regardless if they have an observer or not.
                IsometricTransform[] transforms = GameObject.FindObjectsOfType<IsometricTransform>();
                foreach (IsometricTransform t in transforms) {
                    if (t.GetLevel() == level) {
                        t.UpdatePosition();
                    }
                }

                OrientableSprite[] orientableSprites = GameObject.FindObjectsOfType<OrientableSprite>();
                foreach (OrientableSprite os in orientableSprites) {
                    if (os.GetLevel() == level) {
                        os.UpdateSprite();
                    }
                }
            }
        }

        public Orientation GetOrientation() {
            return orientation;
        }

        public void RotateClockwise(int times=1) {
            SetOrientation((Orientation)(((int)orientation + times) % 4));
        }

        // Tile Rotation
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

        // Wall Rotation
        public Vector3Int RotateWall(Vector3Int original) {
            Vector2Int tileRotated = RotateTile(new Vector2Int(original.x, original.y));
            return RotateWallInsideTile(new Vector3Int(tileRotated.x, tileRotated.y, original.z));
        }

        public Vector3Int InverseRotateWall(Vector3Int original) {
            Vector2Int tileRotated = InverseRotateTile(new Vector2Int(original.x, original.y));
            return InverseRotateWallInsideTile(new Vector3Int(tileRotated.x, tileRotated.y, original.z));
        }

        public Vector3Int RotateWallInsideTile(Vector3Int original) {

            switch (orientation) {
                case Orientation.WEST:
                    if (original.z == 0) {
                        original.z = 1;
                    }
                    else {
                        original.z = 0;
                        original.x += 1;
                    }
                    break;
                case Orientation.SOUTH:
                    if (original.z == 0) {
                        original.x += 1;
                    }
                    else {
                        original.y -= 1;
                    }
                    break;
                case Orientation.EAST:
                    if (original.z == 0) {
                        original.z = 1;
                        original.y -= 1;
                    }
                    else {
                        original.z = 0;
                    }
                    break;
            }

            return original;
        }

        public Vector3Int InverseRotateWallInsideTile(Vector3Int original) {

            switch (orientation) {
                case Orientation.EAST:
                    if (original.z == 0) {
                        original.z = 1;
                    }
                    else {
                        original.z = 0;
                        original.x += 1;
                    }
                    break;
                case Orientation.SOUTH:
                    if (original.z == 0) {
                        original.x += 1;
                    }
                    else {
                        original.y -= 1;
                    }
                    break;
                case Orientation.WEST:
                    if (original.z == 0) {
                        original.z = 1;
                        original.y -= 1;
                    }
                    else {
                        original.z = 0;
                    }
                    break;
            }

            return original;
        }
    }

}