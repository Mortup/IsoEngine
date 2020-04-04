using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso {

    public class NullLevel : Level {

        public static NullLevel instance {
            get {
                if (_instance == null) {
                    GameObject go = new GameObject("NullLevel");
                    _instance = go.AddComponent<NullLevel>();
                }

                return _instance;
            }
        }

        private static NullLevel _instance;

        public void Awake() {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            transformer = new Transformer(transform);
            data = new NullLevelData();
        }

        public override void LoadLevel() {
        }
    }

}