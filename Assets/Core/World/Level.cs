using UnityEngine;

using com.mortup.iso.serialization;
using com.mortup.iso.observers;
using com.mortup.iso.world;

namespace com.mortup.iso {

    public class Level : MonoBehaviour {
        public FloorObserver floorObserver { get; private set; }
        public WallObserver wallObserver { get; private set; }
        public Transformer transformer { get; private set; }
        public LevelData data { get; private set; }

        [Header("Debug Settings")]
        [SerializeField] private string levelName;
        [SerializeField] private bool loadOnStart;

        private ILevelSerializer levelSerializer;

        private void Awake() {
            transformer = new Transformer(this);

            levelSerializer = GetComponent<ILevelSerializer>();
            if (levelSerializer == null) {
                levelSerializer = gameObject.AddComponent<NullRoomSerializer>();
            }

            if (loadOnStart) {
                LoadLevel();
            }
        }

        public void LoadLevel() {
            data = levelSerializer.LoadLevel(levelName);

            floorObserver = new FloorObserver(this);
            wallObserver = new WallObserver(this);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.S)) {
                levelSerializer.SaveLevel(data);
            }
        }

    }

}
