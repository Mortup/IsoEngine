using UnityEngine;

using com.mortup.iso.resources;
using com.mortup.iso.world;

namespace com.mortup.iso.observers {

    public class FloorObserver {
        private Level level;
        private IResource[,] tileResources;

        public FloorObserver(Level level) {
            this.level = level;
            tileResources = new IResource[level.data.width, level.data.height];
            UpdateAllTiles();
            level.data.RegisterFloorObserver(this);
        }

        public void UpdateTile(int x, int y) {
            DestroyTile(x, y);

            int tileIndex = level.data.GetFloor(x, y);

            IResource tileRes = ResourceManager.GetTile(tileIndex);

            tileRes.gameObject.name = string.Format("Floor Tile [{0}, {1}]", x, y);
            tileRes.gameObject.transform.SetParent(level.transform);

            tileRes.spriteRenderer.sortingLayerName = "Floor";

            tileRes.isometricTransform.Init(level);
            tileRes.isometricTransform.coords = new Vector2Int(x, y);

            tileResources[x, y] = tileRes;
        }

        private void UpdateAllTiles() {

            for (int x = 0; x < level.data.width; x++) {
                for (int y = 0; y < level.data.height; y++) {
                    UpdateTile(x, y);
                }
            }

        }

        private void DestroyTile(int x, int y) {
            if (tileResources[x,y] != null) {
                tileResources[x, y].Destroy();
            }
        }

    }

}