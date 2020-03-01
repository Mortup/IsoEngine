using UnityEngine;

using com.mortup.iso.observers;

namespace com.mortup.iso.resources {

    public class ResourceManager {

        private const string prefabPath = "DefaultPrefabs/{0}";
        private const string tilePath = "Sprites/Floor/{0}";
        private const string wallSpritePath = "Sprites/Wall/{0}/Wall_00{0}_{1}";

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
            string loadPath = string.Format(wallSpritePath, index, side);

            if (Resources.Load<Sprite>(loadPath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(Resources.Load<GameObject>(string.Format(prefabPath, "Wall")));
                prefabContainer.spriteRenderer.sprite = GetWallSprite(index, side);

                BiOrientableSprite bos = prefabContainer.gameObject.GetComponent<BiOrientableSprite>();
                bos.westEastSprite = GetWallSprite(index, 0);
                bos.northSouthSprite = GetWallSprite(index, 1);
                bos.invertedSide = side == 0 ? false : true;

                return prefabContainer;
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(loadPath));
            }

            Debug.LogErrorFormat("Couldn't find resource for wall {0} with path {1}", index, loadPath);
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