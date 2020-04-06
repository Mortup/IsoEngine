using UnityEngine;

using com.mortup.iso.serialization;
using com.mortup.iso.observers;

namespace com.mortup.iso {

    public class DefaultLevel : Level {        

        [Header("Debug Settings")]
        [SerializeField] private string levelName;
        [SerializeField] private bool loadOnStart;

        private ILevelSerializer levelSerializer;

        private FloorObserver floorObserver;
        private WallObserver wallObserver;
        private ItemObserver itemObserver;

        private void Awake() {
            transformer = new Transformer(transform);

            levelSerializer = GetComponent<ILevelSerializer>();
            if (levelSerializer == null) {
                levelSerializer = gameObject.AddComponent<NullRoomSerializer>();
            }

            foreach(IsoMonoBehaviour isoMonoBehaviour in GetComponents<IsoMonoBehaviour>()) {
                isoMonoBehaviour.OnInit(this);
            }

            if (loadOnStart) {
                LoadLevel();
            }

            floorObserver = GetComponent<FloorObserver>();
            wallObserver = GetComponent<WallObserver>();
            itemObserver = GetComponent<ItemObserver>();
        }

        public override void LoadLevel() {
            data = levelSerializer.LoadLevel(levelName);

            foreach (IsoMonoBehaviour isoMonoBehaviour in GetComponents<IsoMonoBehaviour>()) {
                isoMonoBehaviour.OnLevelLoad(this);
            }

        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.S)) {
                levelSerializer.SaveLevel(data);
            }
        }

        public override GameObject GetFloorGameObject(Vector2Int coords) {
            if (floorObserver == null) {
                Debug.LogWarning("Floor GameObjects cannot be retrieved without a FloorObserver.");
                return null;
            }

            return floorObserver.GetFloorGameObject(coords);
        }

        public override GameObject GetWallGameObject(Vector3Int coords) {
            if (wallObserver == null) {
                Debug.LogWarning("Wall GameObjects cannot be retrieved without a WallObserver.");
                return null;
            }

            return wallObserver.GetWallGameObject(coords);
        }

        public override GameObject GetItemGameObject(Vector2Int coords) {
            if (itemObserver == null) {
                Debug.LogWarning("Item GameObjects cannot be retrieved without a ItemObserver.");
                return null;
            }

            return itemObserver.GetItemGameObject(coords);
        }

    }

}
