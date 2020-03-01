using UnityEngine;

using com.mortup.iso.observers;

namespace com.mortup.iso.resources {

    public class ResourceManager {

        private const string prefabPath = "DefaultPrefabs/";

        public static PrefabContainer GetTilePrefab(int index) {
            string loadPath = "Sprites/Floor/" + index.ToString();

            if (Resources.Load<Sprite>(loadPath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(Resources.Load<GameObject>(prefabPath + "Tile"));
                prefabContainer.spriteRenderer.sprite = GetTileSprite(index);
                return prefabContainer;
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(loadPath));
            }

            Debug.LogErrorFormat("Couldn't find resource for tile {0}", index);
            return null;
        }

        public static PrefabContainer GetWallPrefab(int index, int side) {
            string loadPath = string.Format("Sprites/Wall/{0}/Wall_00{0}_{1}", index, side);

            if (Resources.Load<Sprite>(loadPath) != null) {
                PrefabContainer prefabContainer = new PrefabContainer(Resources.Load<GameObject>(prefabPath + "Wall"));
                prefabContainer.spriteRenderer.sprite = GetWallSprite(index, side);
                return prefabContainer;
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabContainer(Resources.Load<GameObject>(loadPath));
            }

            Debug.LogErrorFormat("Couldn't find resource for wall {0} with path {1}", index, loadPath);
            return null;
        }

        public static Sprite GetTileSprite(int index) {
            string loadPath = "Sprites/Floor/" + index.ToString();

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
            string loadPath = string.Format("Sprites/Wall/{0}/Wall_00{0}_{1}", index, side);

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