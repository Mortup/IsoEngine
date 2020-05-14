using UnityEngine;

using com.mortup.iso.observers;

namespace com.mortup.iso.world {

    public interface ILevelData {

        int id { get; set; }
        string name { get; set; }
        string owner { get; set; }
        int width { get; set; }
        int height { get; set; }

        int GetFloor(int x, int y);

        void SetFloor(int x, int y, int value);

        void RegisterFloorObserver(FloorObserver observer);

        bool IsFloorInBounds(Vector2Int coords);

        int GetWall(int x, int y, int z);

        int GetWall(Vector3Int coords);

        void SetWall(int x, int y, int z, int value);

        void RegisterWallObserver(WallObserver observer);

        bool IsWallInBounds(Vector3Int coords);

        void RegisterItemObserver(ItemObserver observer);

        bool IsItemInBounds(Vector2Int coords);

        /// <summary>
        /// Returns the item index on x and the item orientation on y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        Vector2Int GetItem(int x, int y);

        Vector2Int GetItem(Vector2Int coords);

        void SetItem(int x, int y, int index, short orientation);

    }

}