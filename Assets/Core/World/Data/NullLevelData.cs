using UnityEngine;

using com.mortup.iso.observers;

namespace com.mortup.iso.world {

    public class NullLevelData : ILevelData {
        public int id { get => 0; set { } }
        public string name { get => "NullLevel"; set { } }
        public string owner { get => "NullOwner"; set { } }
        public int width { get => 0; set { } }
        public int height { get => 0; set { } }

        public NullLevelData() {
        }

        public int GetFloor(int x, int y) {
            return 0;
        }

        public Vector2Int GetItem(int x, int y) {
            return Vector2Int.zero;
        }

        public Vector2Int GetItem(Vector2Int coords) {
            return Vector2Int.zero;
        }

        public int GetWall(int x, int y, int z) {
            return 0;
        }

        public int GetWall(Vector3Int coords) {
            return 0;
        }

        public bool IsFloorInBounds(Vector2Int coords) {
            return true;
        }

        public bool IsItemInBounds(Vector2Int coords) {
            return true;
        }

        public bool IsWallInBounds(Vector3Int coords) {
            return true;
        }

        public void RegisterFloorObserver(FloorObserver observer) {
        }

        public void RegisterItemObserver(ItemObserver observer) {
        }

        public void RegisterWallObserver(WallObserver observer) {
        }

        public void SetFloor(int x, int y, int value) {
        }

        public void SetItem(int x, int y, int index, short orientation) {
        }

        public void SetWall(int x, int y, int z, int value) {
        }
    }

}