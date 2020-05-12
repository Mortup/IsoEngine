using System.IO;

using UnityEngine;

using com.mortup.iso.observers;

namespace com.mortup.iso.resources {

    public class ResourceManager {

        private static string prefabPath {
            get {
                return Path.Combine(Settings.basePath, "DefaultPrefabs", "{0}");
            }
        }
        private static string floorPath {
            get {
                return Path.Combine(Settings.basePath, "Sprites", "Floor", "{0}");
            }
        }
        private static string wallPrefabPath {
            get {
                return Path.Combine(Settings.basePath, "Sprites", "Wall", "{0}", "Wall_00{0}");
            }
        }
        private static string wallSpritePath {
            get {
                return Path.Combine(Settings.basePath, "Sprites", "Wall", "{0}", "Wall_00{0}_{1}");
            }
        }
        private static string itemFolderPath {
            get {
                return Path.Combine(Settings.basePath, "Sprites", "Item", "{0}");
            }
        }
        private static string itemPrefabPath {
            get {
                return Path.Combine(itemFolderPath, "Item_00{0}");
            }
        }
        private static string itemSpritePath {
            get {
                return Path.Combine(itemFolderPath, "Item_00{0}_{1}");
            }
        }

        public static PrefabContainer GetFloorPrefab(int index) {
            string loadPath = string.Format(floorPath, index);

            if (Resources.Load<Sprite>(loadPath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(GetBaseFloorPrefab());
                prefabContainer.spriteRenderer.sprite = GetFloorSprite(index);
                SingleOrientableSprite sos = (SingleOrientableSprite)prefabContainer.orientableSprite;
                sos.sprite = GetFloorSprite(index);
                return prefabContainer;
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(loadPath));
            }

            Debug.LogErrorFormat("Couldn't find resource for tile {0} with path {1}", index, loadPath);
            return null;
        }

        public static PrefabContainer GetWallPrefab(int index, int side) {
            string currentWallPrefabPath = string.Format(wallPrefabPath, index);
            string currentWallSpritePath = string.Format(wallSpritePath, index, side);

            if (Resources.Load<GameObject>(wallPrefabPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(currentWallSpritePath));
            }
            if (Resources.Load<Sprite>(currentWallSpritePath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(GetBaseWallPrefab());
                prefabContainer.spriteRenderer.sprite = GetWallSprite(index, side);

                WallOrientableSprite wallOrientableSprite = prefabContainer.gameObject.GetComponent<WallOrientableSprite>();
                wallOrientableSprite.westEastSprite = GetWallSprite(index, 0);
                wallOrientableSprite.northSouthSprite = GetWallSprite(index, 1);
                wallOrientableSprite.invertedSide = side == 0 ? false : true;

                return prefabContainer;
            }

            Debug.LogErrorFormat("Couldn't find resource for wall {0} with path {1}", index, currentWallSpritePath);
            return null;
        }

        public static PrefabContainer GetItemPrefab(int index, int side) {
            string currentItemPrefabPath = string.Format(itemPrefabPath, index);
            string currentItemSpritePath = string.Format(itemSpritePath, index, side);

            if (Resources.Load<GameObject>(itemPrefabPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(currentItemSpritePath));
            }
            if (Resources.Load<Sprite>(currentItemSpritePath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(GetBaseItemPrefab());
                prefabContainer.spriteRenderer.sprite = GetItemSprite(index, side);

                RegularOrientableSprite itemOrientableSprite = prefabContainer.gameObject.GetComponent<RegularOrientableSprite>();
                itemOrientableSprite.localOrientation = (Transformer.Orientation)side;
                int spriteCount = GetItemSpriteCount(index);
                itemOrientableSprite.northSprite = GetItemSprite(index, 0);

                if (spriteCount == 4) {
                    itemOrientableSprite.westSprite = GetItemSprite(index, 1);
                    itemOrientableSprite.southSprite = GetItemSprite(index, 2);
                    itemOrientableSprite.eastSprite = GetItemSprite(index, 3);
                }
                else if (spriteCount == 2) {
                    itemOrientableSprite.westSprite = GetItemSprite(index, 1);
                    itemOrientableSprite.southSprite = itemOrientableSprite.northSprite;
                    itemOrientableSprite.eastSprite = itemOrientableSprite.westSprite;
                }
                else {
                    itemOrientableSprite.westSprite = itemOrientableSprite.northSprite;
                    itemOrientableSprite.southSprite = itemOrientableSprite.northSprite;
                    itemOrientableSprite.eastSprite = itemOrientableSprite.northSprite;
                }
                return prefabContainer;
            }

            Debug.LogErrorFormat("Couldn't find resource for item {0} with path {1}", index, currentItemSpritePath);
            return null;
        }

        public static Sprite GetFloorSprite(int index) {
            string loadPath = string.Format(floorPath, index);

            Sprite sprite = Resources.Load<Sprite>(loadPath);
            if (sprite != null) {
                return sprite;
            }

            GameObject go = Resources.Load<GameObject>(loadPath);
            if (go != null && go.GetComponent<SpriteRenderer>() != null) {
                return go.GetComponent<SpriteRenderer>().sprite;
            }

            Debug.LogErrorFormat("Couldn't find sprite for tile {0} with path {1}", index, loadPath);
            return null;
        }

        public static Sprite GetWallSprite(int index, int side) {
            string loadPath = string.Format(wallSpritePath, index, side);

            Sprite sprite = Resources.Load<Sprite>(loadPath);
            if (sprite != null) {
                return sprite;
            }

            GameObject go = Resources.Load<GameObject>(loadPath);
            if (go != null && go.GetComponent<SpriteRenderer>() != null) {
                return go.GetComponent<SpriteRenderer>().sprite;
            }

            Debug.LogErrorFormat("Couldn't find sprite for wall {0} with path {1}", index, loadPath);
            return null;
        }

        public static Sprite GetItemSprite(int index, int side) {
            string loadPath = string.Format(itemSpritePath, index, side);

            Sprite sprite = Resources.Load<Sprite>(loadPath);
            if (sprite != null) {
                return sprite;
            }

            GameObject go = Resources.Load<GameObject>(loadPath);
            if (go != null && go.GetComponent<SpriteRenderer>() != null) {
                return go.GetComponent<SpriteRenderer>().sprite;
            }

            Debug.LogErrorFormat("Couldn't find sprite for item {0} with path {1}", index, loadPath);
            return null;
        }

        public static int GetItemSpriteCount(int index) {
            string itemPath = string.Format(itemFolderPath, index);            
            return Resources.LoadAll<Sprite>(itemPath).Length;
        }

        public static GameObject GetBaseFloorPrefab() {
            string path = string.Format(prefabPath, "Tile");
            GameObject baseFloor = Resources.Load<GameObject>(path);
            
            if (baseFloor == null) {
                Debug.LogErrorFormat("Couldn't find a base prefab for floor tiles at {0}.", path);
            }

            return baseFloor;
        }

        public static GameObject GetBaseWallPrefab() {
            string path = string.Format(prefabPath, "Wall");
            GameObject baseWall = Resources.Load<GameObject>(path);

            if (baseWall == null) {
                Debug.LogErrorFormat("Couldn't find a base prefab for wall tiles at {0}.", path);
            }

            return baseWall;
        }

        public static GameObject GetBaseItemPrefab() {
            string path = string.Format(prefabPath, "Item");
            GameObject baseItem = Resources.Load<GameObject>(path);

            if (baseItem == null) {
                Debug.LogErrorFormat("Couldn't find a base prefab for item tiles at {0}.", path);
            }

            return baseItem;
        }

    }

}