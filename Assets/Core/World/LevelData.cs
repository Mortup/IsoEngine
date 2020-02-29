using System.Collections.Generic;
using UnityEngine;

using com.mortup.iso.observers;

namespace com.mortup.iso.world {

    public class LevelData {
        public int id;
        public string name;
        public string owner;
        public int width;
        public int height;

        private int[,] floorTiles;
        private int[,,] wallTiles;

        private List<FloorObserver> floorObservers;
        private List<WallObserver> wallObservers;

        public LevelData(int width, int height) {
            id = -1;
            name = "Default Level";
            owner = "Gonzalito del Flow";
            this.width = width;
            this.height = height;
            floorTiles = new int[width, height];
            wallTiles = new int[width + 1, height + 1, 2];

            floorObservers = new List<FloorObserver>();
            wallObservers = new List<WallObserver>();

            wallTiles[2, 2, 1] = 1;
            wallTiles[3, 1, 0] = 1;
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

        public bool IsFloorInBounds(Vector2Int coords) {
            if (coords.x < 0 || coords.x > width - 1)
                return false;

            if (coords.y < 0 || coords.y > height - 1)
                return false;

            return true;
        }

        public int GetWall(int x, int y, int z) {
            return wallTiles[x, y, z];
        }

        public void SetWall(int x, int y, int z, int value) {
            wallTiles[x, y, z] = value;

            foreach (WallObserver observer in wallObservers) {
                observer.UpdateWall(x, y, z);
            }
        }

        public void RegisterWallObserver(WallObserver observer) {
            wallObservers.Add(observer);
        }

        public bool IsWallInBounds(Vector3Int coords) {
            if (coords.x < 0 || coords.x > width) {
                return false;
            }

            if (coords.y < 0 || coords.y > height) {
                return false;
            }

            return coords.z == 0 || coords.z == 1;
        }
    }

}