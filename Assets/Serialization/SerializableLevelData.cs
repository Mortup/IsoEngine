using System;

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
                floorTiles[y * width + x] = levelData.floorTiles[x, y];
            }
        }
    }

    public LevelData ToLevelData() {
        LevelData levelData = new LevelData();
        levelData.name = name;
        levelData.owner = owner;
        levelData.width = width;
        levelData.height = height;

        levelData.floorTiles = new int[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                levelData.floorTiles[x, y] = floorTiles[y * width + x];
            }
        }

        return levelData;
    }
}
