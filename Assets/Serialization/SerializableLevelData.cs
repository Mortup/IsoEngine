﻿using System;

[Serializable]
public class SerializableLevelData
{
    public string name;
    public string owner;
    public int width;
    public int height;
    public int[] floorTiles;

    public SerializableLevelData(LevelData levelData) {
        name = levelData.name;
        owner = levelData.owner;
        width = levelData.width;
        height = levelData.height;

        floorTiles = new int[width * height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                floorTiles[y * width + x] = levelData.GetFloor(x, y);
            }
        }
    }

    public LevelData ToLevelData() {
        LevelData levelData = new LevelData(width, height);
        levelData.name = name;
        levelData.owner = owner;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                levelData.SetFloor(x, y, floorTiles[y * width + x]);
            }
        }

        return levelData;
    }
}
