using UnityEngine;

namespace com.mortup.iso {

    public abstract class IsoMonoBehaviour : MonoBehaviour {
        public virtual void OnInit(Level level) { }
        public virtual void OnLevelLoad(Level level) { }
    }

}