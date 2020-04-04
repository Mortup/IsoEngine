using UnityEngine;

using com.mortup.iso.serialization;
using com.mortup.iso.observers;
using com.mortup.iso.world;

namespace com.mortup.iso {

    public class DefaultLevel : Level {        

        [Header("Debug Settings")]
        [SerializeField] private string levelName;
        [SerializeField] private bool loadOnStart;

        private ILevelSerializer levelSerializer;

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

    }

}
