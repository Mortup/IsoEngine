using System;

namespace com.mortup.iso.serialization.tiled {

    [Serializable]
    public class TiledJsonLayer {
        public int[] data;
        public int height;
        public int id;
        public string name;
        public float opacity;
        public string type;
        public bool visible;
        public int width;
        public int x;
        public int y;
    }
}