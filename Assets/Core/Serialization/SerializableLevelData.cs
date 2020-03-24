using System;
using UnityEngine;

using com.mortup.iso.world;

namespace com.mortup.iso.serialization {

    [Serializable]
    public class SerializableLevelData {
        public int id;
        public string name;
        public string owner;
        public int width;
        public int height;
        public int[] floorTiles;
        public int[] wallTiles;
        public int[] itemTiles;
        public int[] itemOrientations;

        public SerializableLevelData(LevelData levelData) {
            id = levelData.id;
            name = levelData.name;
            owner = levelData.owner;
            width = levelData.width;
            height = levelData.height;

            floorTiles = new int[width * height];
            itemTiles = new int[width * height];
            itemOrientations = new int[width * height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    floorTiles[y * width + x] = levelData.GetFloor(x, y);

                    Vector2Int itemData = levelData.GetItem(x, y);
                    itemTiles[y * width + x] = itemData[0];
                    itemOrientations[y * width + x] = itemData[1];
                }
            }

            wallTiles = new int[(width + 1) * (height + 1) * 2];
            for (int x = 0; x < width + 1; x++) {
                for (int y = 0; y < height + 1; y++) {
                    for (int z = 0; z < 2; z++) {
                        wallTiles[(z * (width + 1) * (height + 1)) + (y * (width + 1)) + x] = levelData.GetWall(x, y, z);
                    }
                }
            }
            
        }

        public LevelData ToLevelData() {
            LevelData levelData = new LevelData(width, height);
            levelData.name = name;
            levelData.owner = owner;
            levelData.id = id;

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    levelData.SetFloor(x, y, floorTiles[y * width + x]);
                    levelData.SetItem(x, y, itemTiles[y * width + x], (short)itemOrientations[y * width + x]);
                }
            }

            for (int x = 0; x < width + 1; x++) {
                for (int y = 0; y < height + 1; y++) {
                    for (int z = 0; z < 2; z++) {
                        levelData.SetWall(x, y, z, wallTiles[(z * (width + 1) * (height + 1)) + (y * (width + 1)) + x]);
                    }
                }
            }

            return levelData;
        }
    }

}