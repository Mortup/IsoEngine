using UnityEngine;

using com.mortup.iso.resources;
using com.mortup.iso.world;

namespace com.mortup.iso.observers {

    public class ItemObserver {

        private Level level;
        PrefabContainer[,] itemPrefabs;

        public ItemObserver(Level level) {
            this.level = level;
            itemPrefabs = new PrefabContainer[level.data.width, level.data.height];
            UpdateAllItems();
            level.data.RegisterItemObserver(this);
        }

        public void UpdateItem(int x, int y) {
            DestroyTile(x, y);

            int itemIndex = level.data.GetItem(x, y);

            PrefabContainer itemPrefab = ResourceManager.GetItemPrefab(itemIndex, 0);

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
