using UnityEngine;

namespace com.mortup.iso.resources {

    public class ResourceManager {

        public static IResource GetTile(int index) {
            string loadPath = "Sprites/Floor/" + index.ToString();

            if (Resources.Load<Sprite>(loadPath) != null) {
                return new SpriteResource(GetTileSprite(index));
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabResource(Resources.Load<GameObject>(loadPath));
            }

            Debug.LogErrorFormat("Couldn't find resource for tile {0}", index);
            return null;
        }

        public static IResource GetWall(int index, int side) {
            string loadPath = string.Format("Sprites/Wall/{0}/Wall_00{0}_{1}", index, side);

            if (Resources.Load<Sprite>(loadPath) != null) {
                return new SpriteResource(GetWallSprite(index, side));
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabResource(Resources.Load<GameObject>(loadPath));
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