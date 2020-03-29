using UnityEngine;

using com.mortup.iso.resources;
using com.mortup.iso.world;

namespace com.mortup.iso.observers {

    public class ItemObserver :IsoMonoBehaviour {

        private Level level;
        PrefabContainer[,] itemPrefabs;

        public override void OnLevelLoad(Level level) {
            this.level = level;
            itemPrefabs = new PrefabContainer[level.data.width, level.data.height];
            UpdateAllItems();
            level.data.RegisterItemObserver(this);
        }

        public void UpdateItem(int x, int y) {
            DestroyTile(x, y);

            Vector2Int itemData = level.data.GetItem(x, y);
            int itemIndex = itemData[0];
            int itemOrientation = itemData[1];

            PrefabContainer itemPrefab = ResourceManager.GetItemPrefab(itemIndex, itemOrientation);

            itemPrefab.gameObject.name = string.Format("Item Tile [{0}, {1}]", x, y);
            itemPrefab.gameObject.transform.SetParent(level.transform);

            itemPrefab.spriteRenderer.sortingLayerName = "Items";

            itemPrefab.isometricTransform.Init(level, IsometricTransform.ElementType.Item);
            itemPrefab.isometricTransform.coords = new Vector3Int(x, y, 0);

            itemPrefab.orientableSprite.Init(level);

            itemPrefabs[x, y] = itemPrefab;
        }

        private void UpdateAllItems() {

            for (int x = 0; x < level.data.width; x++) {
                for (int y = 0; y < level.data.height; y++) {
                    UpdateItem(x, y);
                }
            }

        }

        private void DestroyTile(int x, int y) {
            if (itemPrefabs[x, y] != null) {
                itemPrefabs[x, y].Destroy();
            }
        }
    }

}
