using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso {

    public abstract class Level : MonoBehaviour {

        public Transformer transformer { get; protected set; }
        public ILevelData data { get; protected set; }

        public abstract void LoadLevel();

    }

}