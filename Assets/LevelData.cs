using System.Collections.Generic;

public class LevelData
{
    public int id;
    public string name;
    public string owner;
    public int width;
    public int height;

    private int[,] floorTiles;

    private List<FloorObserver> floorObservers;

    public LevelData(int width, int height) {
        id = -1;
        name = "Default Level";
        owner = "Gonzalito del Flow";
        this.width = width;
        this.height = height;
        floorTiles = new int[width, height];

        floorObservers = new List<FloorObserver>();
    }

    public int GetFloor(int x, int y) {
        return floorTiles[x, y];
    }

    public void SetFloor(int x, int y, int value) {
        floorTiles[x, y] = value;

        foreach (FloorObserver observer in floorObservers) {
            observer.UpdateTile(x, y);
        }
    }

    public void RegisterFloorObserver(FloorObserver observer) {
        floorObservers.Add(observer);
    }
}
