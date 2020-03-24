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
        private int[,] items;
        private short[,] itemOrientations;

        private List<FloorObserver> floorObservers;
        private List<WallObserver> wallObservers;
        private List<ItemObserver> itemObservers;

        public LevelData(int width, int height, string name) {
            id = -1;
            this.name = name;
            owner = "Gonzalito del Flow";
            this.width = width;
            this.height = height;
            floorTiles = new int[width, height];
            wallTiles = new int[width + 1, height + 1, 2];
            items = new int[width, height];
            itemOrientations = new short[width, height];

            floorObservers = new List<FloorObserver>();
            wallObservers = new List<WallObserver>();
            itemObservers = new List<ItemObserver>();
        }

        public LevelData(int width, int height) : this(width, height, "Default Level") {}

        public int GetFloor(int x, int y) {
            return floorTiles[x, y];
        }

        public void SetFloor(int x, int y, int value) {
            if (IsFloorInBounds(new Vector2Int(x,y)) == false) {
                return;
            }

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
            if (IsWallInBounds(new Vector3Int(x,y,z)) == false) {
                return (int)FloorIndex.Empty;
            }
            return wallTiles[x, y, z];
        }

        public int GetWall(Vector3Int coords) {
            return GetWall(coords.x, coords.y, coords.z);
        }

        public void SetWall(int x, int y, int z, int value) {
            if (IsWallInBounds(new Vector3Int(x, y, z)) == false) {
                return;
            }

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

            // Edge case bottom left
            if (coords.y == 0  && coords.z == 0) {
                return false;
            }

            // Edge case bottom right
            if (coords.x == width && coords.z == 1) {
                return false;
            }

            return coords.z == 0 || coords.z == 1;
        }

        public void RegisterItemObserver(ItemObserver observer) {
            itemObservers.Add(observer);
        }

        public bool IsItemInBounds(Vector2Int coords) {
            return IsFloorInBounds(coords);
        }

        public Vector2Int GetItem (int x, int y) {
            return new Vector2Int(items[x, y], itemOrientations[x,y]);
        }

        public Vector2Int GetItem(Vector2Int coords) {
            return GetItem(coords.x, coords.y);
        }

        public void SetItem(int x, int y, int index, short orientation) {
            if (IsItemInBounds(new Vector2Int(x,y)) == false) {
                return;
            }

            items[x, y] = index;
            itemOrientations[x, y] = orientation;

            foreach (ItemObserver observer in itemObservers) {
                observer.UpdateItem(x, y);
            }
        }
    }

}