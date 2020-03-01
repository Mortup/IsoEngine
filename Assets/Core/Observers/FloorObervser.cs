using UnityEngine;

using com.mortup.iso.resources;
using com.mortup.iso.world;

namespace com.mortup.iso.observers {

    public class FloorObserver {
        private Level level;
        private PrefabContainer[,] tilePrefabs;

        public FloorObserver(Level level) {
            this.level = level;
            tilePrefabs = new PrefabContainer[level.data.width, level.data.height];
            UpdateAllTiles();
            level.data.RegisterFloorObserver(this);
        }

        public void UpdateTile(int x, int y) {
            DestroyTile(x, y);

            int tileIndex = level.data.GetFloor(x, y);

            PrefabContainer tilePrefab = ResourceManager.GetTilePrefab(tileIndex);

            tilePrefab.gameObject.name = string.Format("Floor Tile [{0}, {1}]", x, y);
            tilePrefab.gameObject.transform.SetParent(level.transform);

            tilePrefab.spriteRenderer.sortingLayerName = "Floor";

            tilePrefab.isometricTransform.Init(level, IsometricTransform.ElementType.Tile);
            tilePrefab.isometricTransform.coords = new Vector3Int(x, y, 0);

            tilePrefab.orientableSprite.Init(level);

            tilePrefabs[x, y] = tilePrefab;
        }
        
        private void UpdateAllTiles() {

            for (int x = 0; x < level.data.width; x++) {
                for (int y = 0; y < level.data.height; y++) {
                    UpdateTile(x, y);
                }
            }

        }

        private void DestroyTile(int x, int y) {
            if (tilePrefabs[x,y] != null) {
                tilePrefabs[x, y].Destroy();
            }
        }

    }

}