using System;

using com.mortup.iso.world;

namespace com.mortup.iso.serialization.tiled {

    [Serializable]
    public class TiledJsonLevelData {
        public int height;
        public bool infinite;
        public TiledJsonLayer[] layers;
        public int nextlayerid;
        public int nextobjectid;
        public string orientation;
        public string renderorder;
        public string tiledversion;
        public int tileheight;
        public TiledTilesetData[] tilesets;
        public int tilewidth;
        public string type;
        public string version;
        public int width;

        public TiledJsonLevelData(ILevelData modified, TiledJsonLevelData original) {
            throw new System.NotImplementedException("Tiled Json Data can't merge levels yet.");
        }

        public ILevelData ToLevelData() {
            ILevelData levelData = new LevelData(width, height);

            TiledJsonLayer floorLayer = GetFloorLayer();
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    levelData.SetFloor(x, y, floorLayer.data[(height - 1 - y) * width + x]);
                }
            }

            return levelData;
        }

        private TiledJsonLayer GetFloorLayer() {
            return layers[0]; // TODO: Sholud check for floor on the name.
        }
    }

}