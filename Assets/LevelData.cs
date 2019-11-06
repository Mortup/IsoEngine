using System;
using UnityEngine;

public class LevelData
{
    public string name;
    public string owner;
    public int width;
    public int height;
    public int[,] floorTiles;
    
    public LevelData() {
        name = "Default Level";
        owner = "Gonzalito del Flow";
        width = 3;
        height = 4;
        floorTiles = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    }
}
