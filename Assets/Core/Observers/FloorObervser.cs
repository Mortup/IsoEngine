using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.observers {

    public class FloorObserver {
        private Level level;
        private GameObject[,] gameobjects;
        private SpriteRenderer[,] spriterenderers;

        public FloorObserver(Level level) {
            this.level = level;

            gameobjects = new GameObject[level.data.width, level.data.height];
            spriterenderers = new SpriteRenderer[level.data.width, level.data.height];

            for (int x = 0; x < level.data.width; x++) {
                for (int y = 0; y < level.data.height; y++) {
                    GameObject go = new GameObject(string.Format("Floor Tile [{0}, {1}]", x, y));
                    go.transform.SetParent(level.transform);
                    go.transform.localPosition = level.transformer.TileToLocal(x, y);

                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.sortingLayerName = "Floor";
                    sr.sortingOrder = SortingOrder(x, y);

                    gameobjects[x, y] = go;
                    spriterenderers[x, y] = sr;
                }
            }

            UpdateAllTiles();

            level.data.RegisterFloorObserver(this);
        }

        public int SortingOrder(int x, int y) {
            Vector2 localPos = level.transformer.TileToLocal(x, y);
            return Mathf.RoundToInt(localPos.y * 1000 + localPos.x * 10) * -1;
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
            spriterenderers[x,y].sortingOrder = SortingOrder(x, y);
            gameobjects[x,y].transform.localPosition = level.transformer.TileToLocal(x, y);
        }

        public void NotifyOrientationChanged() {
            UpdateAllTiles(); // TODO: Replace this for only a position update.
        }

    }

}