using UnityEngine;

namespace com.mortup.iso.resources {

    public class ResourceManager {

        public static ITileResource GetTile(int index) {
            string loadPath = "Sprites/Floor/" + index.ToString();

            if (Resources.Load<Sprite>(loadPath) != null) {
                return new SpriteTileResource(GetTileSprite(index));
            }
            if (Resources.Load<GameObject>(loadPath) != null) {
                return new PrefabTileResource(Resources.Load<GameObject>(loadPath));
            }

            Debug.LogErrorFormat("Couldn't find resource for tile {0}", index);
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

    }

}