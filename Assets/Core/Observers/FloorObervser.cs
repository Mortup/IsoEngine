using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.observers {

    public class FloorObserver {
        private Level level;
        private GameObject[,] gameobjects;
        private SpriteRenderer[,] spriterenderers;
        private IsometricTransform[,] isometricTransforms;

        public FloorObserver(Level level) {
            this.level = level;

            gameobjects = new GameObject[level.data.width, level.data.height];
            spriterenderers = new SpriteRenderer[level.data.width, level.data.height];
            isometricTransforms = new IsometricTransform[level.data.width, level.data.height];

            for (int x = 0; x < level.data.width; x++) {
                for (int y = 0; y < level.data.height; y++) {
                    GameObject go = new GameObject(string.Format("Floor Tile [{0}, {1}]", x, y));
                    go.transform.SetParent(level.transform);

                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.sortingLayerName = "Floor";

                    IsometricTransform isoTrans = go.AddComponent<IsometricTransform>();
                    isoTrans.Init(level);
                    isoTrans.coords = new Vector2Int(x, y);

                    gameobjects[x, y] = go;
                    spriterenderers[x, y] = sr;
                    isometricTransforms[x, y] = isoTrans;
                }
            }

            UpdateAllTiles();

            level.data.RegisterFloorObserver(this);
        }

        private void UpdateAllTiles() {

            for (int x = 0; x < level.data.width; x++) {
                for (int y = 0; y < level.data.height; y++) {
                    UpdateTile(x, y);
                }
            }

        }

        public void UpdateTile(int x, int y) {
            int tileIndex = level.data.GetFloor(x, y);
            spriterenderers[x, y].sprite = Resources.Load<Sprite>("Sprites/Floor/" + tileIndex);
            isometricTransforms[x, y].UpdatePosition();
        }

        public void NotifyOrientationChanged() {
            UpdateAllTiles(); // TODO: Replace this for only a position update.
        }

    }

}