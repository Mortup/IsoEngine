using System.IO;

using UnityEngine;

using com.mortup.iso.observers;

namespace com.mortup.iso.resources {

    public class ResourceManager {

        private static string prefabPath {
            get {
                return Path.Combine(Settings.basePath, "DefaultPrefabs/{0}");
            }
        }
        private static string tilePath {
            get {
                return Path.Combine(Settings.basePath, "Sprites/Floor/{0}");
            }
        }
        private static string wallPrefabPath {
            get {
                return Path.Combine(Settings.basePath, "Sprites/Wall/{0}/Wall_00{0}");
            }
        }
        private static string wallSpritePath {
            get {
                return Path.Combine(Settings.basePath, "Sprites/Wall/{0}/Wall_00{0}_{1}");
            }
        }

        public static PrefabContainer GetTilePrefab(int index) {
            string loadPath = string.Format(tilePath, index);

            if (Resources.Load<Sprite>(loadPath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(Resources.Load<GameObject>(string.Format(prefabPath, "Tile")));
                prefabContainer.spriteRenderer.sprite = GetTileSprite(index);
                SingleOrientableSprite sos = (SingleOrientableSprite)prefabContainer.orientableSprite;
                sos.sprite = GetTileSprite(index);
                return prefabContainer;
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(loadPath));
            }

            Debug.LogErrorFormat("Couldn't find resource for tile {0}", index);
            return null;
        }

        public static PrefabContainer GetWallPrefab(int index, int side) {
            string currentWallPrefabPath = string.Format(wallPrefabPath, index);
            string currentWallSpritePath = string.Format(wallSpritePath, index, side);

            if (Resources.Load<GameObject>(wallPrefabPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(currentWallSpritePath));
            }
            if (Resources.Load<Sprite>(currentWallSpritePath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(Resources.Load<GameObject>(string.Format(prefabPath, "Wall")));
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

        public static Sprite GetTileSprite(int index) {
            string loadPath = string.Format(tilePath, index);

            Sprite sprite = Resources.Load<Sprite>(loadPath);
            if (sprite != null) {
                return sprite;
            }

            GameObject go = Resources.Load<GameObject>(loadPath);
            if (go != null && go.GetComponent<SpriteRenderer>() != null) {
                return go.GetComponent<SpriteRenderer>().sprite;
            }

            Debug.LogErrorFormat("Couldn't find sprite for tile {0}", index);
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

            Debug.LogErrorFormat("Couldn't find sprite for wall {0}", index);
            return null;
        }

    }

}