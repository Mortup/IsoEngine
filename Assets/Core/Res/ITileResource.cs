using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.resources {

    public interface ITileResource {
        GameObject gameObject { get; }
        IsometricTransform isometricTransform { get; }
        SpriteRenderer spriteRenderer { get; }

        void Destroy();
    }

}