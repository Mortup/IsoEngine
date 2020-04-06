using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso {

    public abstract class Level : MonoBehaviour {

        public Transformer transformer { get; protected set; }
        public ILevelData data { get; protected set; }

        public abstract void LoadLevel();

        public abstract GameObject GetFloorGameObject(Vector2Int coords);
        public GameObject GetFloorGameObject(int x, int y) {
            return GetFloorGameObject(new Vector2Int(x, y));
        }

        public abstract GameObject GetWallGameObject(Vector3Int coords);
        public GameObject GetWallGameObject(int x, int y, int z) {
            return GetWallGameObject(new Vector3Int(x, y, z));
        }

        public abstract GameObject GetItemGameObject(Vector2Int coords);
        public GameObject GetItemGameObject(int x, int y) {
            return GetItemGameObject(new Vector2Int(x, y));
        }

    }

}