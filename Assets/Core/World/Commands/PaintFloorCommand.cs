using UnityEngine;

namespace com.mortup.iso.world.commands {

    public class PaintFloorCommand : IWorldCommand {

        private Level level;
        private Vector2Int position;
        private int floorType;

        public PaintFloorCommand(Level level, Vector2Int position, int floorType) {
            this.level = level;
            this.position = position;
            this.floorType = floorType;
        }

        public IWorldCommand Excecute() {
            if (floorType == (int)FloorIndex.Empty) {
                Debug.LogWarning("Can't use paint floor commands to remove tiles.");
                return new NullCommand();
            }

            int initialType = level.data.GetFloor(position.x, position.y);
            if (initialType == (int)FloorIndex.Empty) {
                Debug.LogWarning("Can't use paint floor commands to build tiles.");
                return new NullCommand();
            }

            level.data.SetFloor(position.x, position.y, floorType);
            return new PaintFloorCommand(level, position, initialType);
        }
    }

}